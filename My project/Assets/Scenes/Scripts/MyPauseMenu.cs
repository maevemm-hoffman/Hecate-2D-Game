using UnityEngine;

public class MyPauseMenu : MonoBehaviour
{
    public GameObject menuUI;
    private bool isPaused = false;

  void Update()
{
    if (Input.GetKeyDown(KeyCode.Escape) && !FindObjectOfType<DeathManager>().IsDead())
    {
        TogglePause();
    }
}

    public void ResumeGame()
    {
        isPaused = false;
        menuUI.SetActive(false);
        Time.timeScale = 1f;
        }

    void TogglePause()
    {
        isPaused = !isPaused;
        menuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
