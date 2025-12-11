using UnityEngine;
using TMPro;

public class BoostTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text multiplierText;

    public float boostDuration = 60f; 
    private float boostTimeLeft;
    private bool isBoostActive = false;

    private int activeMultiplier = 1;

    void Update()
    {
        if (!isBoostActive) return;

        boostTimeLeft -= Time.deltaTime;

       
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(boostTimeLeft / 60f);
            int seconds = Mathf.FloorToInt(boostTimeLeft % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }

        
        if (boostTimeLeft <= 0)
        {
            EndBoost();
        }
    }

    public void ActivateMultiplier(int multiplier)
    {
        activeMultiplier = multiplier;
        boostTimeLeft = boostDuration;
        isBoostActive = true;

       
        if (multiplierText != null)
            multiplierText.text = "x" + multiplier;

        
        PlayerPrefs.SetInt("TempBoost", multiplier);
        PlayerPrefs.Save();
    }

    private void EndBoost()
    {
        isBoostActive = false;
        activeMultiplier = 1;

        if (multiplierText != null)
            multiplierText.text = "x1";

        PlayerPrefs.SetInt("TempBoost", 1);
        PlayerPrefs.Save();
    }
}
