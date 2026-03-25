using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 3f;
    public int damage = 1;
    public AudioClip hitSound;
    
    public float soundVolume = 1f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerManager player = collision.GetComponent<playerManager>();
            if (player != null)
            {
                player.TakeDamage(damage); // Apply damage
                Debug.Log("Hit player for " + damage + " damage!");
            }

            Destroy(gameObject); // Destroy projectile after hit
        }
        else if (!collision.isTrigger)
        {
            Destroy(gameObject); // Hit wall or ground
        }
    }
        void PlayHitSound()
    {
        if (hitSound != null)
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position, soundVolume);
        }
    }

}
