using UnityEngine;

public class Alarms : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    void Start()
    {
        source.clip = clip;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
