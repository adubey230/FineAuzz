using UnityEngine;

public static class NewMonoBehaviourScript
{
    public static string GenerateUniqueID(GameObject obj){
        return $"{obj.scene.name}_{obj.transform.position.x}_{obj.transform.position.y}";
    }
}
