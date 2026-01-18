using UnityEngine;

public class CircleClosing : MonoBehaviour
{

    [SerializeField] public GameObject childObject;
    [SerializeField] public GameObject player;
    [SerializeField] Animator animator;
    public Camera cam;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(player.transform.position);
        Vector3 finalPos = new Vector3(screenPos.x, screenPos.y+40f, 0);
        // If using RectTransform:
        GetComponent<RectTransform>().position = finalPos;

        if(player.GetComponent<Player>().dead){
            Debug.Log("bleh");
            animator.SetBool("Dead", true);
        }
    }
}