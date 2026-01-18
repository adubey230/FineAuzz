using UnityEngine;
using System.Collections;

public class TransitionType : MonoBehaviour
{
    // Static reference to the single instance of the class
    public static TransitionType Instance { get; private set; }
    public static bool AniType;
    //public GameObject load;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // If an instance already exists, destroy this duplicate
            Destroy(this.gameObject);
        }
        else
        {
            // Otherwise, set this as the instance
            Instance = this;
            // Optional: keep the object alive across scene loads
            // DontDestroyOnLoad(this.gameObject); 
            
        }
    }
    
    void Start(){
        //load = GameObject.FindWithTag("Canvas");
    }
    // Example public method that can be accessed from other scripts
    public static void SwitchType(bool type)
    {
        AniType = type;
    }

    public static bool getType(){

        return AniType;

    }

    // void Update(){
    //     if(AniType != null && load != null){
    //         load.SetActive(false);
    //     }
    // }
}
