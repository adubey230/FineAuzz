using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject exit;
    [SerializeField] private int timeToReset = 120;
    private bool startResetTimer = false;
    private Player player;

    private string currentScene;

    private void OnEnable()
    {
        GuardLOS.PlayerDetected += HandlePlayerCaught;
    }
    private void OnDisable()
    {
        GuardLOS.PlayerDetected -= HandlePlayerCaught;
    }
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        player = FindFirstObjectByType<Player>();
}
    void FixedUpdate()
    {
        if (startResetTimer)
        {
            timeToReset--;
        }
        if (timeToReset < 0)
        {
            SceneManager.LoadScene(currentScene);
        }
    }

    private void HandlePlayerCaught(GuardLOS guard)
    {
        Debug.Log("Game Over");
        startResetTimer = true;
        player.Die();
    }
}
