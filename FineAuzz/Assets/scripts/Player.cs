using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sprite;
    public float speed = 5f;
    private bool playerIsMoving = false;
    private Rigidbody2D rb; 
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);

        rb.linearVelocity = movement * speed;

        //animator.SetBool("is_walk_front", movement.magnitude > 0);
        Animations();
    }

    private void ResetAnimations()
    {
        animator.SetBool("is_walk_front", false);
        animator.SetBool("is_back_walk", false);
        animator.SetBool("is_side_walk", false);
        animator.SetBool("is_idle", false);
        sprite.flipX = false;
    }

    private void Animations()
    {
        ResetAnimations();

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("is_walk_front", true);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("is_back_walk", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("is_side_walk", true);
            sprite.flipX = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("is_side_walk", true);
        }
        else
        {
            animator.SetBool("is_idle", true);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("yooo");
    }
    public void Die()
    {
        Debug.Log("Imagine the shocked animation playing right now");
        this.enabled = false;
    }
}
