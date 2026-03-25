using UnityEngine;

public class stick : MonoBehaviour
{
    public float speed = 1f;
    public float height = 5.6f;

    private Vector3 startPos;
    private float lastY;
    private SpriteRenderer sr;

    void Start()
    {
        startPos = transform.position;
        lastY = startPos.y;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * height;
        float currentY = startPos.y + newY;

        transform.position = new Vector3(startPos.x, currentY, startPos.z);

        // Flip using SpriteRenderer, not scale
        if (currentY > lastY)
        {
            sr.flipY = false; // Moving up — normal
        }
        else if (currentY < lastY)
        {
            sr.flipY = true; // Moving down — flipped vertically
        }

        lastY = currentY;
    }
}
