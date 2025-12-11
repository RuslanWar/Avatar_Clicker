using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultMenuController : MonoBehaviour
{
    public static ResultMenuController instance;

    [Header("UI")]
    public GameObject menuPanel;
    public TMP_Text resultMultiplierText;

    private void Awake()
    {
        instance = this;
    }

    public void ShowMenu(int chosenMultiplier)
    {
        if (menuPanel != null)
            menuPanel.SetActive(true);

        if (resultMultiplierText != null)
            resultMultiplierText.text = "x" + chosenMultiplier;
    }

    public void Retry()
    {
        FindObjectOfType<MiniGameAvatarSpawner>().RestartMiniGame();
        menuPanel.SetActive(false);
    }

    public void ExitToMain()
    {
        SceneManager.LoadScene(1);
    }
}
