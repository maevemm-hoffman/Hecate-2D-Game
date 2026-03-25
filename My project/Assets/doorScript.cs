using UnityEngine;

public class doorScript : MonoBehaviour
{
    private bool isOpen = false;
    private bool playerNearby = false;
    private PlayerControls controls;
    private collectableContainer inventory; // ✅ changed type

    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GameObject.Find("Collectable Container").GetComponent<collectableContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNearby)
        {
            if (inventory != null && inventory.hasKey)
            {
                if (controls.Player.Interact.WasPressedThisFrame())
                {
                    OpenDoor();
                }
            }
        }
    }

    private void OpenDoor()
    {
        Debug.Log("🗝️ Door opened!");

        if (inventory != null)
            inventory.hasKey = false;

        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.parent.CompareTag("Player")) return;
        playerNearby = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.transform.parent.CompareTag("Player")) return;        
        playerNearby = false;

    }
}
