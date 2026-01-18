using UnityEngine;

public class GM1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] EButton button;
    [SerializeField] GameObject ebutton;
    [SerializeField] GameObject speech;
    [SerializeField] GameObject alarms;
    private bool beenOn = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(button.hasBeenOpened)
        {
            ebutton.SetActive(false);
            if(!button.open)
            {
                alarms.SetActive(true);
                speech.SetActive(true);
            }

            if(!beenOn)
            {
                speech.SetActive(true);
                beenOn = true;
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                speech.SetActive(false);
            }
        }
    }
}
