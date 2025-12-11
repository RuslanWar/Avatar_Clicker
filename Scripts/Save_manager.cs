using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public GameObject objectToSave; 
    public GameObject prefab; 
    private string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "object_save.json");
    }

    private void Start()
    {
        objectToSave = GameObject.Find("A");
        prefab = GameObject.Find("A");
    }

    public void Save()                              
    {
        SaveData data = new SaveData();

        Vector3 pos = objectToSave.transform.position;
        Quaternion rot = objectToSave.transform.rotation;

        data.posX = pos.x;
        data.posY = pos.y;
        data.posZ = pos.z;

        data.rotX = rot.x;
        data.rotY = rot.y;
        data.rotZ = rot.z;
        data.rotW = rot.w;

        data.prefabName = prefab.name;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("SAve: " + savePath);
    }

    public void Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("File not founde");
            return;
        }

        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        Vector3 position = new Vector3(data.posX, data.posY, data.posZ);
        Quaternion rotation = new Quaternion(data.rotX, data.rotY, data.rotZ, data.rotW);

        if (objectToSave != null)
        {
            objectToSave.transform.position = position;
            objectToSave.transform.rotation = rotation;
        }
        else
        {
            string path = "Avatars/" + data.prefabName;
            GameObject loadedPrefab = Resources.Load<GameObject>(path);

            if (loadedPrefab != null)
            {
                objectToSave = Instantiate(loadedPrefab, position, rotation);
            }
            else
            {
                Debug.LogError("Prefab not in  Resources: " + path);
            }
        }

        Debug.Log("Object Destroy");
    }
}