using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class enemyAgroZone : MonoBehaviour
{
    private Transform enemy;
    private Transform player;
    [HideInInspector]
    public float speed;
    private bool isChasing = false;
    private bool isWaiting = false;
    private float patrolRadius;
    private float patrolWait;
    private int patrolDirection = 0;
    private Vector3 centerPoint;
    private float patrolOffset;
    private Rigidbody2D body;

    void Start()
    {
        // General Variables
        enemy = gameObject.transform.parent;
        player = GameObject.Find("Player").transform;
        speed = enemy.GetComponent<enemyManager>().speed;
        body = enemy.GetComponent<Rigidbody2D>();

        // Patrol Variables
        patrolRadius = enemy.GetComponent<enemyManager>().patrolRadius;
        patrolWait = enemy.GetComponent<enemyManager>().patrolWait;

        // Randomize Patrol
        patrolDirection = Random.value < 0.5f ? -1 : 1; // Ts so elegant omg
        if (patrolDirection == 1) { enemy.gameObject.GetComponent<enemyManager>().facingRight = true; }
        else { enemy.gameObject.GetComponent<enemyManager>().facingRight = false; }

        patrolOffset = Random.Range(-patrolRadius, patrolRadius);
        centerPoint = transform.position + Vector3.right * patrolOffset;;

        StartCoroutine(WaitAtEdge());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!enemy.GetComponent<enemyManager>().isDead)
        {
            if (isChasing)
            {
                enemy.gameObject.GetComponent<enemyManager>().isWalking = true;
                Vector2 direction = (player.position - enemy.position).normalized;
                body.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);
                centerPoint = transform.position;

                if (direction.x > 0) { enemy.gameObject.GetComponent<enemyManager>().facingRight = true; }
                else { enemy.gameObject.GetComponent<enemyManager>().facingRight = false; }
            }
            else
            {
                Patrol();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!enemy.GetComponent<enemyManager>().isDead)
        {
            if (collider.CompareTag("Player"))
            {
                isChasing = true;
                if (enemy.gameObject.GetComponent<enemyManager>().enemyType == "Slime")
                {
                    enemy.gameObject.GetComponent<slimeManager>().playerInAgroZone = true;
                    StartCoroutine(enemy.gameObject.GetComponent<slimeManager>().TemporarySpeedBoost());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!enemy.GetComponent<enemyManager>().isDead)
        {
            if (collider.CompareTag("Player"))
            {
                isChasing = false;
                if (enemy.gameObject.GetComponent<enemyManager>().enemyType == "Slime")
                {
                    enemy.gameObject.GetComponent<slimeManager>().playerInAgroZone = false;
                }
            }
        }
    }

    void Patrol()
    {
        if (isWaiting) { enemy.gameObject.GetComponent<enemyManager>().isWalking = false; return; }
        else
        {
            enemy.gameObject.GetComponent<enemyManager>().isWalking = true;
            enemy.position += new Vector3(patrolDirection * speed * Time.deltaTime, 0, 0);
            if (enemy.position.x >= centerPoint.x + patrolRadius || enemy.position.x <= centerPoint.x - patrolRadius)
            {
                StartCoroutine(WaitAtEdge());
            }
        }
    }

    IEnumerator WaitAtEdge()
    {
        isWaiting = true;
        yield return new WaitForSeconds(patrolWait + Random.Range(-1f, 1f));
        patrolDirection *= -1;
        enemy.gameObject.GetComponent<enemyManager>().facingRight = !enemy.gameObject.GetComponent<enemyManager>().facingRight;
        isWaiting = false;
    }
}
