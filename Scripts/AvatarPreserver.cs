using UnityEngine;

public class AvatarPreserver : MonoBehaviour
{
    private static AvatarPreserver instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}