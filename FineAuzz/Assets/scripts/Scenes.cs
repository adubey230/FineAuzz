using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public float changeTime;
    public int scene;

    private void Update() {
        changeTime -= Time.deltaTime;
        if(changeTime <= 0 && scene == 0) {
            SceneManager.LoadScene(1);
        }
    }

    public void StartRoom1() {
        SceneManager.LoadScene(2);
    }
}
