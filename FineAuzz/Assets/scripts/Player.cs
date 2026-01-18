using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sprite;
    public float speed = 5f;
    public bool InRange = false;
    public bool VaseCheck = false;
    private bool playerIsMoving = false;
    private Rigidbody2D rb;
    private Distraction vase;
    public static event Action<Distraction> DestroyVase;
    bool inputPossible = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.F) && InRange){
                Debug.Log("KMS");
                DestroyVase?.Invoke(vase);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if (inputPossible)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(horizontal, vertical);

            rb.linearVelocity = movement * speed;

            //animator.SetBool("is_walk_front", movement.magnitude > 0);
            Animations();
        }
        if(vase != null){
            VaseCheck = true;
        }else{
            VaseCheck = false;
        }
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

    void OnTriggerEnter2D(Collider2D collider)
    {   
        Debug.Log("plss");
        if(collider.CompareTag("case"))
        {
            Debug.Log("changed sorting order");
            sprite.sortingOrder = 2;
        }
        
        Debug.Log("enter");
        if(collider.tag == "Vase"){
            InRange = true;
            vase = collider.gameObject.GetComponent<Distraction>();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Vase"){
            InRange = false;
            vase = null;
        }
        

        if(collider.CompareTag("case"))
        {
            sprite.sortingOrder = 10;
        }
    }

    public void Die()
    {
        animator.Play("shocked", 0, 0f);
        inputPossible = false;
        rb.linearVelocity = new Vector2(0, 0);
    }
}
