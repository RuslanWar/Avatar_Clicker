using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   
    public void openscene()
    {
        SceneManager.LoadScene("AvatarCreatorWizard");
    }

public void openscene1()
    {
        SceneManager.LoadScene("Scene_1");
    }
    public void openscene2()
    {
        SceneManager.LoadScene("Scene_menu");
    }
}

