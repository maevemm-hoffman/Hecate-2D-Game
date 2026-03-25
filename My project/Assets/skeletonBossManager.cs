using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMinion : MonoBehaviour
{
    public float maxHealth = 1;
    public float respawnDelay = 3f;          // Time before minion respawns
    public Vector2 spawnAreaMin;             // Bottom-left corner of spawn area
    public Vector2 spawnAreaMax;             // Top-right corner of spawn area

    [HideInInspector] public float health;
    [HideInInspector] public bool isWalking = true;

    private bool isHit = false;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator anim;
    private playerManager playerScript;
    private Vector3 spawnPosition;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerScript = player.GetComponent<playerManager>();

        health = maxHealth;

        StartCoroutine(VisibilityCycle());
    }

    void FixedUpdate()
    {
        Vector2 vel = body.linearVelocity;
        vel.y = Mathf.Clamp(vel.y, -0.5f, float.MaxValue); // max falling speed
        body.linearVelocity = vel;
    }




    private IEnumerator HideAndRespawn()
    {
        // Hide minion (move offscreen or disable sprite)
        transform.position = new Vector3(-40f, 2f, 0f);

        yield return new WaitForSeconds(respawnDelay);

        isHit = false;
    }

     private IEnumerator VisibilityCycle()
    {
        //Debug.Log("HeartFall script started.");
        yield return new WaitForSeconds(3f); // Wait 5 seconds initially
 
        while (true)
        {
            TeleportToRandomPosition();
            // gameObject.SetActive(true);
 
            yield return new WaitForSeconds(15f); // Visible for 10 seconds
 
            transform.position = new Vector3(-40f, 2f, 0f);
            // gameObject.SetActive(false);

            yield return new WaitForSeconds(3f); // Hidden for 15 seconds
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Line"))
        {
            if (playerScript != null)
                playerScript.TakeDamage(1000);
        }
        if (collision.gameObject.CompareTag("PlayerProjectile") && !isHit)
        {
            Debug.Log("Hit by projectile!");
            isHit = true;
            StartCoroutine(HideAndRespawn());
        }
    }

    private void TeleportToRandomPosition()
    {
        float randomX = Random.Range(-8f, 9f);
        float randomY = Random.Range(6f, 11f);
        transform.position = new Vector3(randomX, randomY, transform.position.z);
        //Debug.Log("HeartFall script started.");
    }
}
