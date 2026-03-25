using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GemManager : MonoBehaviour
{
    public string enemyType = "";
    public float maxHealth = 5;
  
    public float health;

    [Header("Drop Settings")]
    public GameObject ingredientDropPrefab; // Assign in inspector for enemies that should drop
    public Sprite ingredientDropSprite;
    public bool dropIngredient = true;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private GameObject collectableContrianer;
  
    
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        health = maxHealth;
        collectableContrianer = GameObject.Find("Collectable Container");

          // Prevent any movement or physics interaction
        body.bodyType = RigidbodyType2D.Static;
      
    }
    // Take Damage
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Lost Health: Health = " + health);

   

        // If enemy health below 0 run Die()
        if (health <= 0) { Die(); }

       
    }
    

    private void Die()
    {
        if (dropIngredient && ingredientDropPrefab != null)
        {
            GameObject drop = Instantiate(ingredientDropPrefab, transform.position, Quaternion.identity, collectableContrianer.transform);
            EnemyDropIngredient dropScript = drop.GetComponent<EnemyDropIngredient>();
            if (dropScript != null)
            {
                dropScript.SetDropSprite(ingredientDropSprite);
            }
 
        }
        {
            Destroy(gameObject);
        }
    }

}
