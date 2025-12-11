using UnityEngine;
using ReadyPlayerMe;
using ReadyPlayerMe.Core;

public class AvatarLoaderHandler : MonoBehaviour
{
    [SerializeField] private string avatarUrl; // manual URL for test
    private GameObject avatar;

    private void Start()
    {
        avatarUrl = PlayerPrefs.GetString("URL");
        if (!string.IsNullOrEmpty(avatarUrl))
        {
            LoadAvatar(avatarUrl);
        }
    }

    public void OnAvatarCreated(string url)
    {
        Debug.Log($"Avatar create: {url}");
        avatarUrl = url;
        LoadAvatar(url);
        Debug.Log($"get URL avatar: {url}");

        if (string.IsNullOrEmpty(url))
        {
            Debug.LogError(" URL avatar empty!");
            return;
        }

        LoadAvatar(url);
    }

    private void LoadAvatar(string url)
    {
        if (avatar != null)
        {
            Destroy(avatar);
        }

        var avatarLoader = new AvatarObjectLoader();
        avatarLoader.OnCompleted += OnAvatarLoaded;
        avatarLoader.LoadAvatar(url);
    }

    private void OnAvatarLoaded(object sender, CompletionEventArgs args)
    {
        avatar = args.Avatar;
        avatar.transform.position = new Vector3 (0,0,-7);
        avatar.transform.rotation = Quaternion.Euler (0, 180, 0);
        Debug.Log("Avatar loaded and in scene");
    }


}
