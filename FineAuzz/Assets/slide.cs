using UnityEngine;

public class slide : MonoBehaviour
{
    // [SerializeField] public GameObject childObject;
    [SerializeField] public GameObject exit;
    [SerializeField] Animator animator;
    //public Camera cam;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 screenPos = cam.WorldToScreenPoint(player.transform.position);
        // Vector3 finalPos = new Vector3(screenPos.x, screenPos.y+40f, 0);
        // // If using RectTransform:
        // GetComponent<RectTransform>().position = finalPos;

        if(TransitionType.getType()){
            TransitionType.SwitchType(false);
            Debug.Log("passed");
            animator.SetBool("Type", true);
        }

        if(exit.GetComponent<Exit>().exited){
            animator.SetBool("Exit", true);
        }
    }
}
