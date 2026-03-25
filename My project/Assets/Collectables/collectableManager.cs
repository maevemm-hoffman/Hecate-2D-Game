using UnityEngine;
using UnityEngine.SceneManagement;

public class collectableManager : MonoBehaviour
{
    public string collectableType;
    public AudioClip sfx;
    public float sfxVolume = 1f;
    private collectableContainer container;

    //red zone #
    public int attempt = 0;
    private int needed = 5;
    public bool isCollected = false;
    private bool Red_dont_destroy = true;

    void Start()
    {
        container = transform.parent.GetComponent<collectableContainer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) { return; }

        if (sfx != null)
        {
            AudioSource.PlayClipAtPoint(sfx, transform.position, sfxVolume);
        }

        switch (collectableType)
        {
            case "Ingredient":
                Debug.Log("Ingredient picked up");
                container.addToCounter("Ingredient");
                gameObject.SetActive(false);
                break;

            case "Health":
                Debug.Log("Health picked up");
                gameObject.SetActive(false);
                break;

            case "Coin":
                Debug.Log("Coin picked up");
                container.addToCounter("Coin");
                gameObject.SetActive(false);
                break;

            case "Key":
                Debug.Log("Key picked up");
                container.hasKey = true;
                gameObject.SetActive(false);
                break;

            case "Red":
                Debug.Log("Red picked up");
                attempt++;

                if (attempt >= needed)
                {
                    Debug.Log("bollean");
                    isCollected = true;
                    SceneManager.LoadScene(6);
                }

                if (isCollected)
                {
                    Debug.Log("Fully collected");
                    container.addToCounter("Ingredient");
                    gameObject.SetActive(false);
                }
                else
                {
                    if (attempt == 1)
                    {
                        transform.position = new Vector3(8.5f, -1.4f, transform.position.z);
                    }
                    else if (attempt == 2)
                    {
                        transform.position = new Vector3(3.9f, 13.3f, transform.position.z);
                    }
                    else if (attempt == 3)
                    {
                        transform.position = new Vector3(-7.61f, -0.13f, transform.position.z);
                    }
                    else if (attempt == 4)
                    {
                        transform.position = new Vector3(7.9f, 6.46f, transform.position.z);
                    }


                }
                break;

            default:
                Debug.Log("Other collectable picked up");
                gameObject.SetActive(false);
                break;
        }
    }
}
