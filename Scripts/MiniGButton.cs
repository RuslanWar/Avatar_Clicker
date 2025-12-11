using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGButton : MonoBehaviour
{
    [Header("MiniGame Settings")]
    public string miniGameSceneName = "DropGame";
    public Vector3 miniGamePosition = new Vector3(0, 1, 0); // start
    public Vector3 miniGameScale = Vector3.one;             // size avatar
    public float moveForce = 5f;                            // pawer moving

    private GameObject avatar;
    private Rigidbody rb;

    private Rigidbody originalRb;
    private Collider originalCol;
    private Vector3 originalScale;
    private bool wasKinematic;

    public void LoadMiniGame()
    {
        avatar = GameObject.Find("A");
        if (avatar == null)
        {
            Debug.LogWarning("Avatar not a found!");
            return;
        }

      
        originalRb = avatar.GetComponent<Rigidbody>();
        originalCol = avatar.GetComponent<Collider>();
        originalScale = avatar.transform.localScale;
        if (originalRb != null)
            wasKinematic = originalRb.isKinematic;

        DontDestroyOnLoad(avatar);

        
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(miniGameSceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == miniGameSceneName)
        {
            // New API: PhysicsMaterialCombine ( S)
            SetupMiniGameAvatar(PhysicsMaterialCombine.Multiply);
        }

        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void SetupMiniGameAvatar(PhysicsMaterialCombine combineMode)
    {
        if (avatar == null) return;

        avatar.transform.position = miniGamePosition;
        avatar.transform.localScale = miniGameScale;
        avatar.transform.rotation = Quaternion.Euler(0, -90, 0);

        rb = avatar.GetComponent<Rigidbody>();
        if (rb == null) rb = avatar.AddComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
                         //RigidbodyConstraints.FreezeRotationX |
                         //RigidbodyConstraints.FreezeRotationY |
                         //RigidbodyConstraints.FreezeRotationZ;

        Collider col = avatar.GetComponent<Collider>();
        if (col == null) col = avatar.AddComponent<SphereCollider>();

       
        PhysicsMaterial mat = new PhysicsMaterial("Head")
        {
            bounciness = 0.8f,
            dynamicFriction = 0,
            staticFriction = 0.4f,
            bounceCombine = PhysicsMaterialCombine.Maximum,
            frictionCombine = PhysicsMaterialCombine.Average
        };

        col.material = mat;
       
    }

    private void Update()
    {
        if (rb == null) return;

        
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == avatar)
                {
                    Vector3 dir = hit.point - avatar.transform.position;
                    dir.y = 0;
                    dir.z = 0;
                    rb.AddForce(dir.normalized * moveForce, ForceMode.Force);
                }
            }
        }
    }

    
    public void OnLeftButton()
    {
        if (rb != null)
            rb.AddForce(Vector3.left * moveForce, ForceMode.Force);
    }

    public void OnRightButton()
    {
        if (rb != null)
            rb.AddForce(Vector3.right * moveForce, ForceMode.Force);
    }

    public void ExitMiniGame(string mainSceneName)
    {
        if (avatar == null) return;

        avatar.transform.localScale = originalScale;

        if (rb != null)
        {
            if (originalRb != null)
            {
                rb.isKinematic = wasKinematic;
                rb.constraints = RigidbodyConstraints.None;
            }
            else
            {
                Destroy(rb);
            }
        }

        Collider col = avatar.GetComponent<Collider>();
        if (col != null && originalCol == null)
            Destroy(col);

        SceneManager.LoadScene(mainSceneName);
    }
}
