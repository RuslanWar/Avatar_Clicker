using UnityEngine;
using UnityEngine.UI;

public class URLSaver : MonoBehaviour
{
    public Text URLadress;
    private void OnEnable()
    {
        PlayerPrefs.SetString("URL",URLadress.text);
       
    }
}
