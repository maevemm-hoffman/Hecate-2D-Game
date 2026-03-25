using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    
    public Transform firePoint;
    public float fireRate = 0.2f;
    private PlayerControls controls; 

    private bool isShooting = false;
    private float nextFireTime = 0f;
    void Awake()
    {
        controls = new PlayerControls(); 
        controls.Player.Interact.performed += ctx => isShooting = true;
        
        controls.Player.Interact.canceled += ctx => isShooting = false;
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
        void Update()
    {
        if (isShooting && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; 
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.up * projectileSpeed; 
        }
    }
}
