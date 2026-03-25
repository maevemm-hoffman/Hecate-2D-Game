using UnityEngine;
using System.Collections;

public class slimeManager : MonoBehaviour
{
    private float defaultSpeed;
    private enemyAgroZone enemy;
    [HideInInspector]
    public bool playerInAgroZone = false;

    void Start()
    {
        enemy = transform.Find("AgroZone").GetComponent<enemyAgroZone>();
        defaultSpeed = enemy.speed;
    }

    public IEnumerator TemporarySpeedBoost()
    {
        while (playerInAgroZone)
        {
            yield return new WaitForSeconds(Random.Range(4f, 6f));
            enemy.speed = defaultSpeed * 20f; // boost
            enemy.transform.parent.GetComponent<enemyManager>().animation.SetTrigger("isJumping");
            yield return new WaitForSeconds(0.15f);
            enemy.speed = defaultSpeed; // reset
        }
    }
}
