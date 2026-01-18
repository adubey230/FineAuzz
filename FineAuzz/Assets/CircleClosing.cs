using UnityEngine;
using UnityEngine.UI;

public class CircleClosing : MonoBehaviour
{

    [SerializeField] public GameObject childObject;
    [SerializeField] public GameObject player;
    [SerializeField] Animator animator;
    public Camera cam;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(TransitionType.getType()){
            this.gameObject.GetComponent<Image>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(new Vector3(player.transform.position.x, player.transform.position.y+1f, 0));
        //Vector3 finalPos = new Vector3(screenPos.x, screenPos.y+40f, 0);
        // If using RectTransform:
        GetComponent<RectTransform>().position = screenPos;

        if(player.GetComponent<Player>().dead){
            Debug.Log("bleh");
            this.gameObject.GetComponent<Image>().enabled = true;
            animator.SetBool("Dead", true);
        }
    }
}