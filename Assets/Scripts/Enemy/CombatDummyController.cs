using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth, knockbackSpeedX,knockbackSpeedY,knockbackDuration,knockbackDeathSpeedX,knockbackDeathSpeedY,deathTorque;
    private float currentHealth, knockbackStart;
    private int facingDirection;

    private bool playerOnLeft,knockback;

    [SerializeField]
    private bool applyKnockback;

    private PlayerMovement pc;
    private GameObject aliveGO, brokenTopGO, brokenBotGO;
    [SerializeField]
    private GameObject hitParticle;
    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
    private Animator aliveAnim;



    private void Start()
    {
        currentHealth = maxHealth;
        pc = GameObject.Find("Player").GetComponent<PlayerMovement>();
        aliveGO = transform.Find("Alive").gameObject;
        brokenTopGO = transform.Find("Broken Top").gameObject;
        brokenBotGO = transform.Find("Broken Bottom").gameObject;

        aliveAnim = aliveGO.GetComponent<Animator>();
        rbAlive = aliveGO.GetComponent<Rigidbody2D>();
        rbBrokenTop = brokenTopGO.GetComponent<Rigidbody2D>();
        rbBrokenBot = brokenBotGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);
        brokenTopGO.SetActive(false);
        brokenBotGO.SetActive(false);
    }

    private void Update()
    {
        CheckKnockback();
    }

    private void Damage(float[] attackDetails)
    {
        //Nyawa sekarang dikurangin dengan besar damage dari PlayerCombatController
        currentHealth -= attackDetails[0];

        if (attackDetails[1] < aliveGO.transform.position.x)
        {
            facingDirection = 1;
        }
        else
        {
            facingDirection = -1;
        }
        Instantiate(hitParticle,aliveGO.transform.position,Quaternion.Euler(0.0f,0.0f,Random.Range(0.0f,360)));

        if(facingDirection == 1)
        {
            playerOnLeft = true;
        }
        else
        {
            playerOnLeft = false;
        }

        aliveAnim.SetBool("playerOnLeft", playerOnLeft);
        aliveAnim.SetTrigger("damage");

        if(applyKnockback && currentHealth > 0.0f)
        {
            //apply knockback
            Knockback();
        }

        if(currentHealth <= 0.0f)
        {
            //ded
            Die();
            

        }
    }   

    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        rbAlive.velocity = new Vector2(knockbackSpeedX * facingDirection, knockbackSpeedY);
    }

    private void CheckKnockback()
    {
        if(Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }

    private void Die()
    {
        pc.addTotal();
        //Supaya yang active cuma 1 game object
        aliveGO.SetActive(false);
        brokenTopGO.SetActive(true);
        brokenBotGO.SetActive(false);

        brokenTopGO.transform.position = aliveGO.transform.position;
        brokenBotGO.transform.position = aliveGO.transform.position;

        rbBrokenBot.velocity = new Vector2(knockbackSpeedX * facingDirection, knockbackDeathSpeedY);
        rbBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * facingDirection, knockbackDeathSpeedY);
        rbBrokenTop.AddTorque(deathTorque - facingDirection, ForceMode2D.Impulse);
        
    }

}
