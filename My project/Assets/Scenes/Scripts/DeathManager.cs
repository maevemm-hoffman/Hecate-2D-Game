

using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public GameObject deathUI;
    private bool isDead = false; 

    public bool IsDead() => isDead;

    public void PlayerDied()
    {
        if (isDead) return;
        isDead = true;

        deathUI.SetActive(true);
        Time.timeScale = 0f; // pause the game
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // assuming 0 is main menu
    }
}