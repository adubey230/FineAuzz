
using UnityEngine;

public class Distraction : MonoBehaviour
{   
    public bool IsCracked = false;

    private Collider2D vase;

    void Start(){
        vase = GetComponent<Collider2D>();
    }

    [SerializeField] public Sprite broken;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool canInteract(){
        return !IsCracked && InRange();
    }
    
    public bool InRange(){
        vase.OnTriggerStay2D(Collider2D collider){
            if(collider.tag == "range"){
                return true;
            }
        }
        return false;
    }

    public void Interact(){
        if(!canInteract()){
            return;
        }
        CrackVase();
    }

    private void CrackVase(){
        IsCracked = true;
        GetComponent<SpriteRenderer>().sprite = broken;
        //LookAtDist(transform.position.x,transform.position.y);
        
    }
}
