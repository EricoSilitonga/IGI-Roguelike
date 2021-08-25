using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{

    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private GameObject
        deathChunkParticle, deathBloodParticle;

    private float currentHealth;

    private GameManager GM;

    public HealthBar healthBar;

    private void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        healthBar.setHealth(currentHealth);
        if(currentHealth <= 0.0f)
        {
            Die();
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
        healthBar.setHealth(currentHealth);
    }

    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
        SceneManager.LoadScene("DieScene");
    }
}
