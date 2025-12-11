using ReadyPlayerMe.AvatarCreator;
using ReadyPlayerMe.Core;
using ReadyPlayerMe.Samples.AvatarCreatorWizard;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarTransferHandler : MonoBehaviour
{
    [SerializeField] private AvatarCreatorStateMachine avatarCreatorStateMachine;
    [SerializeField] private AvatarConfig inGameConfig;

    private AvatarObjectLoader avatarObjectLoader;
    public static GameObject LoadedAvatar;

    private void OnEnable()
    {
        avatarCreatorStateMachine.AvatarSaved += OnAvatarSaved;
    }

    private void OnDisable()
    {
        avatarCreatorStateMachine.AvatarSaved -= OnAvatarSaved;
        avatarObjectLoader?.Cancel();
    }

    private void OnAvatarSaved(string avatarId)
    {
        PlayerPrefs.SetString("SavedAvatarId", avatarId);
        PlayerPrefs.Save();

        avatarObjectLoader = new AvatarObjectLoader();
        avatarObjectLoader.AvatarConfig = inGameConfig;
        avatarObjectLoader.OnCompleted += (sender, args) =>
        {
            LoadedAvatar = args.Avatar;
            LoadedAvatar.transform.position = new Vector3(0, -1.2f, -8);
            LoadedAvatar.transform.rotation = Quaternion.Euler(0, 180, 0);
            LoadedAvatar.transform.localScale = new Vector3(4, 4, 4);
            LoadedAvatar.name = "A";
            //DontDestroyOnLoad(LoadedAvatar);
            SceneManager.LoadScene("Scene_1");
        };

        avatarObjectLoader.LoadAvatar($"{Env.RPM_MODELS_BASE_URL}/{avatarId}.glb");
    }
}