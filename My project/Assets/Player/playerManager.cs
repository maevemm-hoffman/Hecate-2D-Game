// When Adding Animation: Make sure parameters are called isWalking and isAttacking. isWalking should be a boolean and isAttacking should be a trigger
// When Adding Enemy Health and Attacking: Name Enemy taking damage function TakeDamage(float damage) like the players
// Also Make Sure Enemies are Tagged With "Enemy"

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;


public class playerManager : MonoBehaviour
{
    // Public Variables
    public float maxHealth = 100f;
    public float speed = 3f;
    public float attackDamage = 5f;
    public float attackAngle = 30;

    // Private Variables
    private PlayerControls controls;
    private InputAction move;
    private Vector2 movement;
    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer sprite;
    private Collider2D collider;
    private DeathManager deathManager;
    [HideInInspector] public float health;
    private bool isDead = false;
    private bool facingRight = true;
    private Slider healthBar;
    [HideInInspector] public bool touchingGem = false;
    private bool isImmune = false;

    // List of Enemies touching player
    public List<Collider2D> enemies = new List<Collider2D>();

    // Create Instance of controls and enable them: (Please do not touch oh my god)
    private void Awake() => controls = new PlayerControls();
    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    void Start()
    {
        // Run OnAttack() when attack buttons pressed
        controls.Player.Move.performed += ctx => OnMove();
        controls.Player.Move.canceled += ctx => OnMoveCancel();
        controls.Player.Attack.performed += ctx => OnAttack();

        body = gameObject.GetComponent<Rigidbody2D>();
        move = InputSystem.actions.FindAction("Move");
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        collider = gameObject.GetComponent<CapsuleCollider2D>();
        deathManager = GameObject.Find("DeathManager").GetComponent<DeathManager>();
        healthBar = GameObject.Find("Canvas/Player Health").GetComponent<Slider>();

        health = maxHealth;
        healthBar.value = 1;
    }

    void Update()
    {
        // Move (via rigidbody)
        if (!isDead)
        {
            body.MovePosition(body.position + (movement * speed * Time.fixedDeltaTime));
            if (!facingRight) { sprite.flipX = true; }
            else { sprite.flipX = false; }
        }
        else
        {
            collider.enabled = false; // Disable collider when dead
            body.linearVelocity = Vector2.zero; // Stop movement when dead
        }
    }

    void OnMove()
    {
        // Update direction value when Moving
        movement = move.ReadValue<Vector2>();
        // Start Animation
        animator.SetBool("isWalking", true);
        // Check if moving right or left
        if (movement.x > 0) { facingRight = true; } else if (movement.x < 0) { facingRight = false; }
    }
    void OnMoveCancel()
    {
        // Stop Moving either direction
        movement = Vector2.zero;
        // Stop Animation
        animator.SetBool("isWalking", false);
    }

    void OnAttack()
    {
        animator.SetTrigger("isAttacking");

        if (touchingGem)
        {
            GameObject.Find("GemCluster").GetComponent<GemManager>().TakeDamage(attackDamage);
        }

        // For every enemy touching
        for (int i = 0; i < enemies.Count; i++)
        {
            // float angle = Vector3.Angle((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized, (enemies[i].transform.position - transform.position).normalized);
            // Debug.Log(angle);
            // if (angle <= attackAngle / 2f)
            // {
            enemies[i].gameObject.GetComponent<enemyManager>().TakeDamage(attackDamage);
            // }
            // else
            // {
            // Debug.Log("T");
            // }
        }
    }

    // Take Damage
    public void TakeDamage(float damage)
    {
        if (!isImmune)
        {
            health = health - damage;
            healthBar.value = 1 * (health / maxHealth);
            // Run Die if health below 0 
            if (health <= 0) { StartCoroutine(DiePlayer()); }
        }
    }

    // Die
    IEnumerator DiePlayer()
    {
        isDead = true;
        animator.SetTrigger("isDead");
        yield return new WaitForSeconds(2f);
        deathManager.PlayerDied();
    }

    public IEnumerator ActivateImmunity(float duration)
    {
        isImmune = true;

        yield return new WaitForSeconds(duration);

        isImmune = false;
    }
}
