using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EyeTimer : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    [SerializeField] private Image Eye;
    [SerializeField] private Sprite OpenEye;
    [SerializeField] private Sprite ClosedEye;
    [SerializeField] private GameObject guard;
    public float blinkDuration;
    public float resetDuration;
    [SerializeField] public float offset;
    public Camera cam;
    private float remainingDur;

    private bool Closed = false;
    public RectTransform rectTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // rectTransform = GetComponent<RectTransform>();
        // rectTransform = transform as RectTransform;
        blinkDuration = guard.GetComponentInChildren<GuardLOS>().blinkTimerVal;
        resetDuration = guard.GetComponentInChildren<GuardLOS>().resetTimerVal;
        Being(blinkDuration);
        Eye.sprite = OpenEye;
    }

    void Update(){

        Vector3 screenPos = cam.WorldToScreenPoint(guard.transform.position);
        Vector3 finalPos = new Vector3(screenPos.x, screenPos.y + offset, 0);
        // If using RectTransform:
        GetComponent<RectTransform>().position = finalPos;


        //transform.position = cam.ScreenToWorldPoint(new Vector3(guard.transform.position.x, guard.transform.position.y ));
        
        //Debug.Log(transform.position);
    }

    private void Being(float second){
        remainingDur = second;
        StartCoroutine(UpdateTimer());
    }

    // Update is called once per frame
    private IEnumerator UpdateTimer()
    {   
        while(remainingDur >= 0){
            if(Closed){
                uiFill.fillAmount = Mathf.InverseLerp(0,resetDuration, remainingDur);
            }else{
                uiFill.fillAmount = Mathf.InverseLerp(0,blinkDuration, remainingDur);
            }
            

            remainingDur-=Time.deltaTime;
            yield return null;
            // remainingDur-=0.2f;
            // yield return new WaitForSeconds(0.2f);
        }
        OnEnd();
    }

    private bool wasShocked = false;
    private void OnEnd(){
        if(Closed){
            Closed = false;
            Eye.sprite = OpenEye;
            Being(blinkDuration);
            guard.GetComponent<GuardMove>().animator.SetBool("closed", false);
            guard.GetComponent<GuardMove>().animator.SetBool("shocked", wasShocked);
        }else{
            Closed = true;
            Eye.sprite = ClosedEye;
            Being(resetDuration);
            guard.GetComponent<GuardMove>().animator.SetBool("closed", true);
            wasShocked = guard.GetComponent<GuardMove>().animator.GetBool("shocked");
            guard.GetComponent<GuardMove>().animator.SetBool("shocked", false);
        }
    }
}
