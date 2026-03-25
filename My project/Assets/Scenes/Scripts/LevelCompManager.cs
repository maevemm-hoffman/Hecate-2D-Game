using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelCompManager : MonoBehaviour
{
   public GameObject levelCompleteUI;
    private bool levelComplete = false;

    public void ShowLevelComplete()
    {
        if (levelComplete) return;
        levelComplete = true;

        levelCompleteUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    // public void LoadNextLevel()
    // {
    //     Time.timeScale = 1f;
    //     SceneController.instance.NextLevel(); // Uses transition animation
    // }

     public void LoadNextLevel()
    {
        Time.timeScale = 1f;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Optional: Check to prevent going out of bounds
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No next scene in build settings.");
        }
    }
}
