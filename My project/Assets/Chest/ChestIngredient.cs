using UnityEngine;

public class ChestIngredient : MonoBehaviour
{
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        if (sr == null)
        {
            Debug.LogError("❌ No SpriteRenderer found on ChestIngredient!");
        }

        gameObject.SetActive(false); // Hide the ingredient at start
    }

    public void Reveal()
    {
        gameObject.SetActive(true); // Show the ingredient

        if (sr != null)
        {
            sr.sortingLayerName = "Default"; // Make sure this matches your chest's sorting layer
            sr.sortingOrder = 10;            // Higher than the chest to appear in front
        }

        Debug.Log("✅ Ingredient is now visible and rendered in front.");
    }
}
