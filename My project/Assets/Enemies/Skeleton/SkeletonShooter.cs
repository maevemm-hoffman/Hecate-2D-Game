using UnityEngine;

public class SkeletonShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float shootingRange = 10f;
    public float projectileSpeed = 5f;
    public AudioClip shootSound;      
    public float shootVolume = 1f;    // Volume control

    public float shootInterval = 2f;   // Time between shots
    private float shootTimer;
    private Transform player;
    private float fireCooldown;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fireCooldown = 0f;
    }

    void Update()
    {
        if (!GetComponent<enemyManager>().isDead)
        { // Check if the skeleton is dead

            if (player == null) { return; }

            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= shootingRange)
            {
                Vector2 direction = (player.position - firePoint.position).normalized;

                if (fireCooldown <= 0f)
                {
                    Shoot(direction);
                    fireCooldown = 1f / fireRate;
                }
            }

            fireCooldown -= Time.deltaTime;
        }
    }

    void Shoot(Vector2 direction)
    {
        Debug.Log("Shooting projectile!");

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Debug.Log("Projectile spawned at: " + projectile.transform.position);

        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position, shootVolume);
        }

        // Make sure we get a Rigidbody2D
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }

        // Optional: prevent it from colliding with the skeleton itself
        Collider2D skeletonCollider = GetComponent<Collider2D>();
        Collider2D projectileCollider = projectile.GetComponent<Collider2D>();
        if (skeletonCollider != null && projectileCollider != null)
        {
            Physics2D.IgnoreCollision(skeletonCollider, projectileCollider);
        }
    }

}
