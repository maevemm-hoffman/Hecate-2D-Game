using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public float damageAmount = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            playerManager player = other.GetComponent<playerManager>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
        }
    }
}
