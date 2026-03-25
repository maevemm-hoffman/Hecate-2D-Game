using UnityEngine;

public class chainsaw : MonoBehaviour
{
    public float speed = 1f;
    public float height = 2.5f;

    private Vector3 startPos;
    private float lastY;
    private bool isFlipped = false;

    void Start()
    {
        startPos = transform.position;
        lastY = startPos.y;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * height;
        float currentY = startPos.y + newY;

        transform.position = new Vector3(startPos.x, currentY, startPos.z);

        // Detect direction change and flip X
        if (currentY > lastY && isFlipped)
        {
            FlipX(false); // Moving up
        }
        else if (currentY < lastY && !isFlipped)
        {
            FlipX(true); // Moving down
        }

        lastY = currentY;
    }

    void FlipX(bool flip)
    {
        isFlipped = flip;
        transform.localScale = new Vector3(flip ? -1f : 1f, 1f, 1f);
    }
}
