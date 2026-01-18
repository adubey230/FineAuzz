using UnityEngine;
using System.Collections;

public class GM1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] EButton button;
    [SerializeField] GameObject ebutton;
    [SerializeField] GameObject speech;
    [SerializeField] GameObject alarms;
    [SerializeField] GameObject portrait;
    private bool off = false;

    void Update()
    {
        if (!button.hasBeenOpened) return;

        // Disable E button once
        if (ebutton.activeInHierarchy)
            ebutton.SetActive(false);

        // Show alarms + speech
        if (!button.open)
        {
            alarms.SetActive(true);

            if (!off)
            {
                speech.SetActive(true);
                off = true;
                StartCoroutine(CloseSpeechAfterSeconds(3f));
            }
        }

        // Handle closing caption
        // if (off && Input.GetKeyDown(KeyCode.E) && !portrait.activeSelf)
        // {
        //     speech.SetActive(false);
        //     //off = true;
        // }
    }

    private IEnumerator CloseSpeechAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            speech.SetActive(false);
        }
}

