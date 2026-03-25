using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float lifetime = 3f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyManager enemy = collision.GetComponent<enemyManager>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Apply damage
                Debug.Log("Hit enemy for " + damage + " damage!");
            }

            Destroy(gameObject); // Destroy projectile after hit
        }
        else if (collision.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject); 
            Destroy(gameObject); 
        }
        else if (!collision.isTrigger)
        {
            Destroy(gameObject); // Hit wall or ground
        }
    }
}
