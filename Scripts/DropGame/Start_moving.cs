using UnityEngine;

public class Start_moving : MonoBehaviour
     
{
    private GameObject avatar;
 
    void Start()
    {
        avatar = GameObject.Find("A");
        if (avatar == null)
        {
            Debug.LogWarning("Avatar not found!");
            return;
        }
        avatar.AddComponent<Moving>();
    }

  
}
