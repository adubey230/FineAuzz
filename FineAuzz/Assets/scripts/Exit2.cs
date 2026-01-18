using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hello");
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Room5");
        }
    }
}
