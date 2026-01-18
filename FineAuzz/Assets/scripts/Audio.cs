using UnityEngine;

public class Audio1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public AudioSource source;
    [SerializeField] AudioClip clip;
    void Start()
    {
        source.clip = clip;
        source.loop = true;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
