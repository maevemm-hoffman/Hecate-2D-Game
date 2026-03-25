using UnityEngine;

public class playerAttackRange : MonoBehaviour
{
    private playerManager player;
    
     void Start()
    {
        player = gameObject.transform.parent.GetComponent<playerManager>();
    }

    // Update touching enemies list
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GemCluster") { player.touchingGem = true; return; } // If touching gem, set fla
        if (!collision.gameObject.CompareTag("Enemy")) { return; }  // Cancel if not enemy
        player.enemies.Add(collision);                                     // Add to List
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GemCluster") { player.touchingGem = false; return; } // If touching gem, set fla
        if (!collision.gameObject.CompareTag("Enemy")) { return; }  // Cancel if not enemy
        player.enemies.Remove(collision);                                  // Remove from List
    }
}
