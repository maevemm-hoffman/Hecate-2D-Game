using UnityEngine;

public class moving : MonoBehaviour
{
    private float rotationSpeed = 270f; // Degrees per second

     private float moveSpeed = 2f;       // Units per second
    private float moveRange = 6f;       // Distance to move left and right

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        //spin 
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

         // Move left and right
        float moveStep = moveSpeed * Time.deltaTime;
        if (movingRight)
            transform.position += new Vector3(moveStep, 0, 0);
        else
            transform.position -= new Vector3(moveStep, 0, 0);

        // Change direction if we've reached the move range
        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveRange)
            movingRight = !movingRight;
    }
}
