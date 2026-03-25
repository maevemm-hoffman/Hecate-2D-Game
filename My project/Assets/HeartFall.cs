using UnityEngine;
using System.Collections;

public class HeartFall : MonoBehaviour
{
    //public playerManager playerManagerScript;
    private bool isHit = false;

    private void Start()
    {
        //gameObject.SetActive(false);
        //playerManagerScript = GameObject.Find("Player").GetComponent<playerManager>();
        transform.position = new Vector3(-200f, 2f, 0f);
        StartCoroutine(VisibilityCycle());
    }

    private void Update()
    {
        if (!isHit){
            Hit();
	}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerProjectile"))
        
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerManager pm = player.GetComponent<playerManager>();
                    if (pm != null)
                    {
                        Debug.Log("Player initial HP: " + pm.health);
                        pm.health += 1;  // Add health here
                        Debug.Log("Player healed. Current HP: " + pm.health);
                    }
            }
            transform.position = new Vector3(-200f, 2f, 0f);

        }
        isHit = true;
    }

    private void Hit(){
        

        StartCoroutine(isHitWaitingTime());

        isHit = false;
    }

    private IEnumerator isHitWaitingTime(){
        yield return new WaitForSeconds(3f);//when hit wait for no. of sec before next cycle

    }

    private IEnumerator VisibilityCycle()
    {
        //Debug.Log("HeartFall script started.");
        yield return new WaitForSeconds(3f); // Wait 5 seconds initially

        while (true)
        {
            TeleportToRandomPosition();
            //gameObject.SetActive(true);

            yield return new WaitForSeconds(5f); // Visible for 10 seconds

            //gameObject.SetActive(false);
            transform.position = new Vector3(-200f, 2f, 0f);

            yield return new WaitForSeconds(5f); // Hidden for 15 seconds
        }
    }

    private void TeleportToRandomPosition()
    {
        float randomX = Random.Range(-8f, 9f);
        float randomY = Random.Range(2f, 11f);
        transform.position = new Vector3(randomX, randomY, transform.position.z);
        //Debug.Log("HeartFall script started.");
    }
}
