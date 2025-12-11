using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using GLTFast;

[Serializable]
public class RpmAsset
{
    public string id;
    public string name;
    public string category;
    public string previewImage;
    public string assetUrl;
}

[Serializable]
public class RpmAssetList
{
    public RpmAsset[] list;
}

public class ReadyPlayerMeAccessoryLoader : MonoBehaviour
{
    [Header("Avatar Reference")]
    public GameObject avatar; // Твій Ready Player Me аватар
    public string headBoneName = "Head"; // Назва кістки голови

    [Header("UI")]
    public TextMeshProUGUI statusText;

    private const string API_URL = "https://api.readyplayer.me/v1/assets";
    private const string AUTH_TOKEN = "Bearer sk_live_XXqgNHCfkhvs0C9neWn9sFALd5hDMw_Zzo89";

    void Start()
    {
        StartCoroutine(LoadAccessoryList());
    }

    IEnumerator LoadAccessoryList()
    {
        statusText.text = "Завантаження аксесуарів...";

        UnityWebRequest request = UnityWebRequest.Get(API_URL);
        request.SetRequestHeader("Authorization", AUTH_TOKEN);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Помилка: " + request.error);
            statusText.text = "Не вдалося отримати аксесуари.";
            yield break;
        }

        string json = "{\"list\":" + request.downloadHandler.text + "}"; // обгортка для масиву
        RpmAssetList assets = JsonUtility.FromJson<RpmAssetList>(json);

        var accessory = assets.list.FirstOrDefault(a => a.category == "headwear");

        if (accessory == null)
        {
            statusText.text = "Немає аксесуарів для голови.";
            yield break;
        }

        statusText.text = "Завантаження аксесуара...";
        StartCoroutine(LoadAndAttachGLB(accessory.assetUrl));
    }

    IEnumerator LoadAndAttachGLB(string glbUrl)
    {
        var gltf = new GltfImport();
        var success = gltf.Load(new Uri(glbUrl)).Result;

        if (!success)
        {
            Debug.LogError("Не вдалося завантажити GLB.");
            statusText.text = "Помилка завантаження аксесуара.";
            yield break;
        }

        GameObject accessoryObj = new GameObject("Accessory");
        success = gltf.InstantiateMainScene(accessoryObj.transform);

        if (!success)
        {
            Debug.LogError("Помилка інстанціювання аксесуара.");
            statusText.text = "Помилка відображення.";
            yield break;
        }

        var head = avatar.transform.FindDeepChild(headBoneName);
        if (head == null)
        {
            Debug.LogError("Кістка голови не знайдена.");
            statusText.text = "Голова не знайдена.";
            yield break;
        }

        accessoryObj.transform.SetParent(head, false);
        accessoryObj.transform.localPosition = Vector3.zero;
        accessoryObj.transform.localRotation = Quaternion.identity;
        accessoryObj.transform.localScale = Vector3.one;

        statusText.text = "Аксесуар додано!";
    }
}
