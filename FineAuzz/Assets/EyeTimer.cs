using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EyeTimer : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    [SerializeField] private GameObject guard;
    public float duration;
    [SerializeField] public float offset;

    private float remainingDur;

    private bool Closed = false;
    private RectTransform rectTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform = transform as RectTransform;
        Being(duration);
    }

    void Update(){
        
        transform.position = guard.transform.position + new Vector3(0, offset, 0);
    }

    private void Being(float second){
        remainingDur = second;
        StartCoroutine(UpdateTimer());
    }
    // Update is called once per frame
    private IEnumerator UpdateTimer()
    {   
        while(remainingDur >= 0){
            uiFill.fillAmount = Mathf.InverseLerp(0,duration, remainingDur);
            remainingDur--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }

    private void OnEnd(){

    }
}
