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
        guards = GameObject.FindGameObjectsWithTag("Enemy");;
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
        float VecX = vase.gameObject.transform.x - guardToLook.transform.x;
        float VecY = vase.gameObject.transform.y - guardToLook.transform.y;
        Vector2 vaseAng = new Vector2(VecX, VecY);
        Vector2 angleDiff = Vector2.SignedAngle(vaseAng, guardToLook.GetComponent<GuardLOS>().GetCurrAng());
        foreach(GameObject guardToLook in guards){
            guardToLook().IncrAimDirection(angleDiff);
        }
    }

}
