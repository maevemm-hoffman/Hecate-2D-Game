using UnityEngine;
using System.Collections;

public class Blood_liza : MonoBehaviour
{
   private float damageAmount = 1f;
    public float damageInterval = 2f;

    public GameObject bloodImage;  // Assign the image object here in Inspector

    private bool isPlayerInside = false;
    private Coroutine damageCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;

            if (bloodImage != null)
                bloodImage.SetActive(true);

            damageCoroutine = StartCoroutine(DamagePlayerOverTime(other));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;

            if (bloodImage != null)
                bloodImage.SetActive(false);

            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DamagePlayerOverTime(Collider2D playerCollider)
    {
        playerManager player = playerCollider.GetComponent<playerManager>();

        while (isPlayerInside)
        {
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }

            yield return new WaitForSeconds(damageInterval);
        }
    }
}
