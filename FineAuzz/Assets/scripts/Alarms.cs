using UnityEngine;

public class Alarms : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    [SerializeField] Audio1 bgAud;
    [SerializeField] AudioClip newClip;
    void Start()
    {
        source.clip = clip;
        source.Play();
        source.loop = true;
        bgAud.source.clip = newClip;
        bgAud.source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
