using UnityEngine;

public class collectableContainer : MonoBehaviour
{
    public int score = 0;                 // General score
    public int ingredientCount = 0;       // Number of collected ingredients
    public int ingredientTotal = 3;       // Total needed for level completion
    public int coinCount = 0;             // Number of coins collected
    public bool hasKey = false;           // Whether player currently has a key
    public LevelCompManager levelComp;

    void Update()
    {
        if (ingredientCount >= ingredientTotal)
        {
            NextLevel();
        }
    }

    public void addToCounter(string collectableType)
    {
        switch (collectableType)
        {
            case "Coin":
                score++;
                break;

            case "Ingredient":
                ingredientCount++;
                break;

            case "Key":
                GiveKey();
                break;
        }
    }

    public void AddCoin()
    {
        coinCount++;
        Debug.Log($"🪙 Coin collected. Total coins: {coinCount}");
    }

    // Give the player a key
    public void GiveKey()
    {
        hasKey = true;
        Debug.Log("🗝️ Key added to inventory.");
    }

    // Remove the player's key (used when opening chest)
    public void UseKey()
    {
        hasKey = false;
        Debug.Log("🗝️ Key used.");
    }

    void NextLevel()
    {
        levelComp.ShowLevelComplete();
    }
}
