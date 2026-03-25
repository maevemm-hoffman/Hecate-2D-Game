using UnityEngine;
using UnityEngine.SceneManagement;
public class bossDeath : MonoBehaviour
{
    void Update()
    {
        Debug.Log(GameObject.Find("Skeleton/Enemy"));
        if (GameObject.Find("Skeleton/Enemy") == null)
        {
            die();
        }     
    }

    void die()
    {
        SceneManager.LoadScene(7);
    }
}
