using UnityEngine;

public class Moving_bat : MonoBehaviour
{
     public float minAngle = -90f;            // Straight up
   public float swingAngle = 180f;         // Total rotation range
    private float rotationSpeed = 0.4f;       // How fast it swings

    void Update()
    {
        // Compute current angle using PingPong
        float angle = Mathf.PingPong(Time.time * rotationSpeed * swingAngle, swingAngle);
        transform.rotation = Quaternion.Euler(0, 0, minAngle + angle);
    }
}
