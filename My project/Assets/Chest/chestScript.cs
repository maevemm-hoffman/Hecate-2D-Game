using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class chestScript : MonoBehaviour
{
    [Header("Chest Settings")]
    public bool requiresKey = true;
    public ChestIngredient ingredient;

    private Animator anim;
    private bool isOpen = false;
    private bool playerNearby = false;

    private collectableContainer inventory; // ✅ changed type
    private PlayerControls controls;

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

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
            Debug.LogError("❌ Animator not found on chest.");

        inventory = GameObject.Find("Collectable Container").GetComponent<collectableContainer>();
        
    }

    void Update()
    {
        if (playerNearby && !isOpen)
        {
            if (!requiresKey || (inventory != null && inventory.hasKey))
            {
                if (controls.Player.Interact.WasPressedThisFrame())
                {
                    OpenChest();
                }
            }
        }
    }

    private void OpenChest()
    {
        anim.SetBool("Open", true);
        isOpen = true;

        if (requiresKey && inventory != null)
            inventory.hasKey = false;

        StartCoroutine(DelayedReveal(1f));
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

    private System.Collections.IEnumerator DelayedReveal(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (ingredient != null)
            ingredient.Reveal();
        else
            Debug.LogWarning("⚠️ No ingredient assigned to chest.");
    }
}
