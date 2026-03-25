using UnityEngine;


public class immunity_script_liza : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        playerManager player = collision.GetComponent<playerManager>();
        if (player != null)
        {
            player.StartCoroutine(player.ActivateImmunity(7f));
        }

        Destroy(gameObject); // Remove the collectible
    }
}

}
