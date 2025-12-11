#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
#endif
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using GLTFast;
using System.Collections;
using System.IO;
using System.Threading.Tasks;

public class GLBModelHandler : MonoBehaviour
{
    public string modelUrl = "Your_URL_Model";
    private string modelPath;
    private const string modelKey = "MyGLBModel"; // Key for Addressables
    public void ButtonPlay() 
    {
        modelPath = Application.dataPath + "/Models/model.glb";
        StartCoroutine(DownloadAndLoadModel());
    }
   

    IEnumerator DownloadAndLoadModel()
    {
        UnityWebRequest request = UnityWebRequest.Get(modelUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            byte[] modelData = request.downloadHandler.data;
            string folderPath = Application.dataPath + "/Models";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            File.WriteAllBytes(modelPath, modelData);
            Debug.Log("Model load in: " + modelPath);

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
            SetAddressable("Assets/Models/model.glb");
#endif

            LoadModel(); 
        }
        else
        {
            Debug.LogError("Error loadimg: " + request.error);
        }
    }

    void LoadModel()
    {
        Addressables.LoadAssetAsync<GameObject>(modelKey).Completed += OnModelLoaded;
    }

    void OnModelLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(handle.Result, transform);
            Debug.Log("GLB in scene!");
        }
        else
        {
            Debug.LogError("Error load GLB from Addressables.");
        }
    }

#if UNITY_EDITOR
    void SetAddressable(string assetPath)
    {
        var assetGUID = AssetDatabase.AssetPathToGUID(assetPath);
        if (!string.IsNullOrEmpty(assetGUID))
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            var entry = settings.CreateOrMoveEntry(assetGUID, settings.DefaultGroup);
            entry.address = modelKey;
            settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, true);
            AssetDatabase.SaveAssets();
            Debug.Log("Model mark like Addressable with key: " + modelKey);
        }
        else
        {
            Debug.LogError("Cant find model in AssetDatabase.");
        }
    }
#endif
}
