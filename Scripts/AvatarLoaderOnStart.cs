using ReadyPlayerMe.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarLoaderOnStart : MonoBehaviour
{
    [SerializeField] private AvatarConfig avatarConfig;
    private AvatarObjectLoader avatarObjectLoader;

    private void Start()
    {
        string savedId = PlayerPrefs.GetString("SavedAvatarId", null);
        if (string.IsNullOrEmpty(savedId))
        {
            Debug.LogWarning("Avatar not found");
            return;
        }
       
        avatarObjectLoader = new AvatarObjectLoader();
        avatarObjectLoader.AvatarConfig = avatarConfig;
        avatarObjectLoader.OnCompleted += OnAvatarLoaded;

        avatarObjectLoader.LoadAvatar($"https://models.readyplayer.me/{savedId}.glb");
    }

    private void OnAvatarLoaded(object sender, CompletionEventArgs args)
    {
        GameObject avatar = args.Avatar;
        avatar.transform.position = new Vector3(0, -1.2f, -8);
        avatar.transform.rotation = Quaternion.Euler(0, 180, 0);
        avatar.transform.localScale = new Vector3(4, 4, 4);
        avatar.name = "A";

        
        avatar.AddComponent<AvatarPreserver>();

       
        var headMovingObj = GameObject.Find("Head_Moving");
       
        if (headMovingObj != null)
        {
            var headMover = headMovingObj.GetComponent<Head_Moviving>();
            if (headMover != null)
            {
                Debug.Log("Head_Moving get and start.");
                headMover.InitializeFromAvatar(avatar);
            }
        }
        else
        {
            Debug.LogWarning("Объект Head_Moving not in ascene.");
        }
    }
}