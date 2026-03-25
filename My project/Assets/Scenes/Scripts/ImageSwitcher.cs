using UnityEngine;

public class ImageSwitcher : MonoBehaviour
{
   public GameObject nextScreen; // Assign the next GameObject here

    public void ShowNext()
    {
        if (nextScreen != null)
        {
            nextScreen.SetActive(true); // Show the next screen
        }

        gameObject.SetActive(false); // Hide this screen
    }
}
