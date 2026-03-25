using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class enemyManager : MonoBehaviour
{
    public string enemyType = "";
    public float maxHealth = 5;
    public float attackDamage = 1;
    public float speed = 2f;
    public float patrolRadius = 5f;
    public float patrolWait = 3f;
    [HideInInspector]
    public float health;
    [HideInInspector]
    public bool isWalking = false;
    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool isDead = false;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Collider2D collider;
    [HideInInspector]
    public Animator animation;
    private Slider healthBar;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        collider = gameObject.GetComponent<CapsuleCollider2D>();
        animation = gameObject.GetComponent<Animator>();
        health = maxHealth;
        healthBar = transform.Find("Canvas/Health").GetComponent<Slider>();
        healthBar.value = 1; // Initialize health bar
    }

    void Update()
    {
        if (!isDead)
        {
            if (enemyType != "Slime")
            {
                if (isWalking) { animation.SetBool("isWalking", true); }
                else { animation.SetBool("isWalking", false); }
            }

            if (facingRight) { sprite.flipX = false; }
            else { sprite.flipX = true; }
        }
        else
        {
            collider.enabled = false; // Disable collider when dead
            body.linearVelocity = Vector2.zero; // Stop movement when dead
            healthBar.value = 0;
        }
    }

    // Take Damage
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0) health = 0; // Prevent health from going below 0
        healthBar.value = health / maxHealth; // Update health bar

        animation.SetTrigger("isHit");

        // Run Zombie Code if Zombie
        if (enemyType == "Zombie")
        {
            GetComponent<zombieManager>().changeSpeed(health);
            transform.Find("AgroZone").gameObject.GetComponent<enemyAgroZone>().speed = speed;
        }

        // If enemy health below 0 run Die()
        if (health <= 0) { StartCoroutine(Die()); }

        //UpdateZombieHealthUI();   
    }
    
    // Collision detection to damage player
    // Requires playerManager script and player TakeDamage function
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead)
        {
            // Check for player
            if (collision.gameObject.CompareTag("Player"))
            {
                // Find player script and run TakeDamage() function
                playerManager player = GameObject.Find("Player").GetComponent<playerManager>();
                if (player != null)
                {
                    player.TakeDamage(attackDamage);
                }
            }
        }
    }

    IEnumerator Die()
    {
        isDead = true;
        animation.SetTrigger("isDead");
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    // void UpdateZombieHealthUI(){
    //     healthText.text = "Health: " + currentHealth;
    // }
}
