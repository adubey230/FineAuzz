using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool exited = false;
    private float timer = 1;
    public bool start = false;
    public int scene;
    [SerializeField]public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(start){
            timer -= Time.deltaTime;
        }
        if(timer <= 0){
            SceneManager.LoadScene(scene + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TransitionType.SwitchType(true);
        player.SetActive(false);
        exited = true;
        Debug.Log("Hello");
        if(other.CompareTag("Player"))
        {
            start = true;
        }
    }
}
