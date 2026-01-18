using UnityEngine;

public class GM1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] EButton button;
    [SerializeField] GameObject ebutton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(button.hasBeenOpened)
        {
            ebutton.SetActive(false);
        }
    }
}
