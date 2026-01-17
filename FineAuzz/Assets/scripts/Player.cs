using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Animator animator;
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

        animator.SetBool("is_walk_front", movement.magnitude > 0);
    }

    private void Animations()
    {
       
    }
}
