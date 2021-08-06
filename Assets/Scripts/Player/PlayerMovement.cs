using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed = 10;
    private float moveInputDirection;
    [SerializeField]
    private float jumpforce = 16f;
    private float jumpTimer;
    private float turnTimer;
    private float wallJumpTimer;
    private float jumpTimerSet = 0.15f;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float wallJumpForce;
    public float turnTimerSet = 0.1f;
    private float knockbackStartTime;
    [SerializeField]
    private float knockbackDuration;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    [SerializeField]
    private Vector2 knockbackSpeed;

    private Animator anim;

    private bool isRunning;
    private bool canNormalJump;
    private bool canWallJump;
    private bool isGrounded = true;
    private bool isFaceRight = true;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isAttemptingToJump;
    private bool checkJumpMultiplier;
    private bool canMove;
    private bool canFlip;
    private bool hasWallJumped;
    private bool knockback;

    public LayerMask whatIsGround;

    public int amountOfJump = 1;
    private int amountOfJumpLeft;
    private int facingDirection = 1;
    private int lastWallJumpDirection;
    public int totalEnemyDestroyed = 0;

    private PlayerCombatController PCC;

    public Transform groundCheck;
    public Transform wallCheck;

    // Start is called before the first frame update
    void Start()
    {
        PCC = GetComponent<PlayerCombatController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpLeft = amountOfJump;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();

    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        checkMovementDirection();
        UpdateAnimation();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
        Debug.Log(totalEnemyDestroyed);
        CheckKnockback();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        checkSurroundings();

    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
            PCC.setCombatEnabled(false);
        }
        else
        {
            PCC.setCombatEnabled(true);
        }

    }

    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }
    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0.01f)
        {
            amountOfJumpLeft = amountOfJump;
            PCC.setCombatEnabled(false);
        }

        if (isTouchingWall)
        {
            canWallJump = true;
            PCC.setCombatEnabled(false);
        }

        if (amountOfJumpLeft <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }
    }

    private void checkMovementDirection()
    {
        if (isFaceRight && moveInputDirection < 0)
        {
            Flip();
        }
        else if (!isFaceRight && moveInputDirection > 0)
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                isRunning = true;
                PCC.setCombatEnabled(false);
            }
            else
            {
                isRunning = false;
                PCC.setCombatEnabled(true);
            }

        }
        else
        {
            isRunning = false;
        }
    }

    private void checkSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void CheckIfWallSliding()
    {
        if (isTouchingWall && moveInputDirection == facingDirection && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void UpdateAnimation()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }

    public int GetFacingDirection()
    {
        return facingDirection;
    }
    private void ApplyMovement()
    {
        if (!isGrounded && !isWallSliding && moveInputDirection == 0 && !knockback)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if (canMove && !knockback)
        {
            rb.velocity = new Vector2(moveSpeed * moveInputDirection, rb.velocity.y);

        }

        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    private void CheckInput()
    {
        moveInputDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || (amountOfJumpLeft > 0 && !isTouchingWall))
            {
                NormalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }

        if (Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if (!isGrounded && moveInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }

        if (!canMove)
        {
            turnTimer -= Time.deltaTime;

            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }
    }

    private void CheckJump()
    {
        if (jumpTimer > 0)
        {
            //WallJump
            if (!isGrounded && isTouchingWall && moveInputDirection != 0 && moveInputDirection != -facingDirection)
            {
                WallJump();
            }
            else if (isGrounded)
            {
                NormalJump();
            }
        }
        if (isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }

        if (wallJumpTimer > 0)
        {
            if (hasWallJumped && moveInputDirection == -lastWallJumpDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                hasWallJumped = false;
            }
            else if (wallJumpTimer <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTimer = Time.deltaTime;
            }
        }
    }

    private void NormalJump()
    {
        if (canNormalJump)
        {
            PCC.setCombatEnabled(false);
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            amountOfJumpLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
        }
    }

    private void WallJump()
    {
        if (canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            isWallSliding = false;
            amountOfJumpLeft = amountOfJump;
            amountOfJumpLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * moveInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;

        }
    }

    public void Flip()
    {
        if (!isWallSliding && canFlip && !knockback)
        {
            facingDirection *= -1;
            isFaceRight = !isFaceRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }

    }

    public void EnableFlip()
    {
        canFlip = true;
    }

    private void DisableFlip()
    {
        canFlip = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public void addTotal()
    {
        totalEnemyDestroyed++;
    }

    public bool getIsRunning()
    {
        return isRunning;
    }
}
