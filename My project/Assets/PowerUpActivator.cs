using UnityEngine;
using System.Collections;

public class PowerUpActivator : MonoBehaviour
{
    public GameObject powerUpObject;  // Drag the inactive GameObject here in the Inspector
    public float delay = 8f;

    private void Start()
    {
        StartCoroutine(ActivateAfterDelay());
    }

    private IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        if (powerUpObject != null)
        {
            powerUpObject.SetActive(true);
        }
    }
}
