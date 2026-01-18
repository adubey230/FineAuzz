using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public float changeTime;

    private void Update() {
        changeTime -= Time.deltaTime;
        if(changeTime <= 0) {
            SceneManager.LoadScene(1);
        }
    }
}
