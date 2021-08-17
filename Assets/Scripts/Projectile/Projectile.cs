using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AttackDetails attackDetails;

    private float speed;
    private float travelDistance;
    private float xStartPos;

    [SerializeField]
    private float damageRadius;

    private Rigidbody2D rb;

    [SerializeField]
    private GameObject bulletProjectile;

    private bool isHitWall;
    private bool isProjectileGone;

    [SerializeField]
    private LayerMask whatIsWall;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private Transform damagePosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;

        isProjectileGone = false;

        xStartPos = transform.position.x;
    }

    private void Update()
    {
        if (!isHitWall)
        {
            attackDetails.position = transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (!isHitWall)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D wallHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsWall);

            if (damageHit)
            {
                damageHit.transform.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
            }

            if (wallHit)
            {
                isHitWall = true;
                Destroy(gameObject);
            }

            if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isProjectileGone)
            {
                Destroy(gameObject);
            }
        }
    }

    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius); 
    }
}
