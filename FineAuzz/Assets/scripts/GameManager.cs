using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject exit;
    [SerializeField] private int timeToReset = 120;
    private bool startResetTimer = false;
    private Player player;
    private GameObject[] guards;
    [SerializeField] Distraction vase;

    private string currentScene;

    private void OnEnable()
    {
        GuardLOS.PlayerDetected += HandlePlayerCaught;
        Player.DestroyVase += TriggerVaseActions;
    }

    private void OnDisable()
    {
        GuardLOS.PlayerDetected -= HandlePlayerCaught;
        Player.DestroyVase += TriggerVaseActions;
    }

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        player = FindFirstObjectByType<Player>();
        guards = GameObject.FindGameObjectsWithTag("Guard");
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

    private void TriggerVaseActions(Distraction vase){
        vase.CrackVase();
        foreach(GameObject guardToLook in guards){
            float VecX = vase.gameObject.transform.position.x - guardToLook.transform.position.x;
            float VecY = vase.gameObject.transform.position.y - guardToLook.transform.position.y;
            Vector2 vaseAng = new Vector2(VecX, VecY);
            float angleDiff = Vector2.SignedAngle(vaseAng, guardToLook.GetComponent<GuardLOS>().GetCurrAng());
            guardToLook.GetComponent<GuardLOS>().IncrAimDirection(angleDiff);
            
        }
    }

}
