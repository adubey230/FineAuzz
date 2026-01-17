using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject exit;

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
        
    }
    void Update()
    {
        
    }

    private void HandlePlayerCaught(GuardLOS guard)
    {
        Debug.Log("Game Over");
    }
}
