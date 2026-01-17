
using UnityEngine;

public class Distraction : MonoBehaviour
{   
    public bool IsCracked = false;

    private bool InRange = false;

    private Collider2D vase;

    void Start(){
        vase = GetComponent<Collider2D>();
    }

    [SerializeField] public Sprite broken;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    void Update(){
        if(Input.GetKeyDown(KeyCode.F)){
            CrackVase();
        }
    }

    private void CrackVase(){
        if(!IsCracked && InRange){
            Debug.Log("crack");
            IsCracked = true;
            GetComponent<SpriteRenderer>().sprite = broken;
            //LookAtDist(transform.position.x,transform.position.y);
        }
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {   
        Debug.Log("enter");
        if(collider.tag == "Range"){
            InRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Range"){
            InRange = false;
        }
    }
}
