using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void PlayGame()
  {
    //resume instead
    SceneManager.LoadScene(2);
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  public void SwitchLv1()
  {
    SceneManager.LoadScene(2);
    Time.timeScale = 1f;
  }
  public void SwitchLv2()
  {
    //if command if lv 2 been completed previously
    SceneManager.LoadScene(3);
    Time.timeScale = 1f;
  }
  public void SwitchLv3()
  {
    //if command if lv 2 been completed previously
    SceneManager.LoadScene(4);
    Time.timeScale = 1f;
  }

  public void SwitchMainMenu()
  {
    SceneManager.LoadScene(1);
    Time.timeScale = 1f;
  }

  public void SwitchCredits()
  {
    SceneManager.LoadScene(8);
    Time.timeScale = 1f;
  }
}
