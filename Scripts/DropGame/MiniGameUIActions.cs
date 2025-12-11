using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameUIActions : MonoBehaviour
{
    public MiniGameAvatarSpawner spawner;

    public void TryAgain()
    {
        if (spawner != null)
            spawner.RestartMiniGame();
        else
            Debug.LogError("Spawner not assigned in MiniGameUIActions!");
    }

    public void Exit()
    {
        SceneManager.LoadScene("Scene_1"); 
    }
}
