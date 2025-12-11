using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MiniGameAvatarSpawner : MonoBehaviour
{
    [Header("point avatar in game")]
    public Transform spawnPoint;

    [Header("UI Many resalt")]
    public GameObject resultMenu;   

    private void Start()
    {
        
        if (resultMenu != null)
            resultMenu.SetActive(false);

        StartCoroutine(PlaceAvatarAfterSceneLoad());
    }

    private IEnumerator PlaceAvatarAfterSceneLoad()
    {
        yield return null;

        var avatar = GameObject.Find("A");

        if (avatar != null)
        {
            avatar.tag = "Player";

            avatar.transform.position = spawnPoint.position;
            avatar.transform.rotation = spawnPoint.rotation;

            if (avatar.GetComponent<Rigidbody>() == null)
            {
                var rb = avatar.AddComponent<Rigidbody>();
                rb.mass = 1f;
                rb.useGravity = true;
            }

            if (avatar.GetComponent<Collider>() == null)
            {
                avatar.AddComponent<SphereCollider>();
            }

            Debug.Log("Avatar in game!");

            avatar.transform.rotation = Quaternion.Euler(0, 180, 0);
            avatar.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);

            var col = avatar.GetComponent<SphereCollider>();
            col.radius = 0.2f;
            col.center = new Vector3(0, 0.58f, 0);
        }
        else
        {
            Debug.LogWarning("avatar not in a game");
        }
    }

    
    public void ShowResultMenu()
    {
        if (resultMenu != null)
            resultMenu.SetActive(true);   
    }

    
    public void RestartMiniGame()
    {
        var avatar = GameObject.Find("A");

        if (avatar == null)
        {
            Debug.LogWarning("Avatar A not found during RestartMiniGame()");
            return;
        }

        Rigidbody rb = avatar.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        avatar.transform.position = spawnPoint.position;
        avatar.transform.rotation = spawnPoint.rotation;

        Debug.Log("MiniGame restarted!");

        
        if (resultMenu != null)
            resultMenu.SetActive(false);
    }

    
    public void ExitToMainScene()
    {
        SceneManager.LoadScene(1);
    }
}
