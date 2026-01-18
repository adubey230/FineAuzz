
using UnityEngine;

public class Distraction : MonoBehaviour
{   
    public bool IsCracked = false;

    [SerializeField] public Sprite broken;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        
    }

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    void Update(){
        // if(Input.GetKeyDown(KeyCode.F)){
        //     CrackVase();
        // }
    }

    public void CrackVase(){
        if(!IsCracked){
            Debug.Log("crack");
            IsCracked = true;
            GetComponent<SpriteRenderer>().sprite = broken;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
