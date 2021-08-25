using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    private Transform PlayerTransform;
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attack1Radius, attack1Damage;
    [SerializeField]
    private float stunDamageAmount = 1;
    [SerializeField]
    private Transform attack1HitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;

    private bool gotInput = false, isAttacking = false , isFirstAttack = false;

    private float lastInputTime = Mathf.NegativeInfinity;

    private Animator anim;

    public PlayerMovement pm;

    private AttackDetails attackDetails;

    private PlayerStats PS;

    private PlayerMovement PM;

    private int counter = 1;
    private void Start()
    {
        PS = GetComponent<PlayerStats>();
        PM = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (combatEnabled)
            {
                //Attempt combat
                gotInput = true;
                lastInputTime = Time.time;
                //PlayerTransform.Translate(Vector3.forward * Time.deltaTime);
            }
        }
    }

    public void setCombatEnabled(bool yesNo)
    {
        combatEnabled = yesNo;
    }

    private void CheckAttacks()
    {
        if (gotInput && !pm.getIsRunning())
        {

            //Perform Attack1
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);

                //merubah setInt
             
                 anim.SetInteger("setInt", counter);
               
                if (Input.GetMouseButtonUp(0) && Time.deltaTime >= Time.deltaTime + 0.4f)
                {
                    isAttacking = false;
                    counter = 1;
                }
                else if(Input.GetMouseButtonUp(0))
                {
                    isAttacking = false;
                }
                else
                {
                    counter++;
                }

                if (counter == 1)
                {
                    SoundManager.PlaySound("attack1");
                }else if(counter == 2)
                {
                    SoundManager.PlaySound("attack2");
                }
                else if(counter == 3)
                {
                    SoundManager.PlaySound("attack3");
                }

                if (counter == 4)
                {
                    counter = 1;
                }
            }
            
        }

        if (Time.time >= lastInputTime + inputTimer)
        {
            //Wait for new input
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        attackDetails.damageAmount = attack1Damage;
        attackDetails.position = transform.position;
        attackDetails.stunDamageAmount = stunDamageAmount;

        foreach (Collider2D collider in detectedObjects)
        {
            //collider.transform.SendMessage("Damage", attackDetails);
            collider.transform.parent.SendMessage("Damage", attackDetails);
        }
    }

    private void Damage(AttackDetails attackDetails)
    {
        int direction;

        PS.DecreaseHealth(attackDetails.damageAmount);

        if (attackDetails.position.x < transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        PM.Knockback(direction);
    }

    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }

}
