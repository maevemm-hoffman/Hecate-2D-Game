using UnityEngine;

public class FadeWithDistance : MonoBehaviour
{
    public Transform player;             // Assign in inspector or find in Start
    public float maxDistance = 6f;      // Distance at which sprite is fully transparent
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null || spriteRenderer == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Calculate alpha: 1 when close, 0 when at or beyond maxDistance
        float alpha = Mathf.Clamp01(1 - (distance / maxDistance));

        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
