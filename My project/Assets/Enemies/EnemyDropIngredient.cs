using UnityEngine;

public class EnemyDropIngredient : MonoBehaviour
{
    public AudioClip pickupSFX;
    public Sprite dropSprite;  // Only declare once!

    private collectableContainer container;

    public void SetDropSprite(Sprite newSprite)
    {
        dropSprite = newSprite;
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null && dropSprite != null)
        {
            Debug.Log($"Setting drop sprite: {dropSprite.name}");
            sr.sprite = dropSprite;
        }
    }

    void Awake()
    {
        gameObject.SetActive(true);

        container = FindObjectOfType<collectableContainer>();
        if (container == null)
        {
            Debug.LogError("❌ No collectableContainer found in scene!");
        }

        if (dropSprite != null)
        {
            SetDropSprite(dropSprite);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect()
    {
        container?.addToCounter("Ingredient");

        if (pickupSFX != null)
        {
            AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
        }

        Debug.Log($"🛒 Player collected ingredient drop: {name}");
        Destroy(gameObject);
    }
}
