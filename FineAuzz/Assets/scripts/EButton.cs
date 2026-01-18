using UnityEngine;

public class EButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject portrait;
    [SerializeField] SpriteRenderer case4Sprite;
    [SerializeField] Sprite image;
    public bool open = false;
    private bool playerInRange = false;
    public bool hasBeenOpened = false;

    void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            open = !open;
            portrait.SetActive(open);

            if (open)
            {
                case4Sprite.sprite = image;
            }
            else
            {
                hasBeenOpened = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
