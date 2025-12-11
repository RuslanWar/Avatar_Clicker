using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Head_Tap : MonoBehaviour
{
    public int currency = 0;
    public int multiplier = 1;
    public TMP_Text currencyText;
    public TMP_Text multiplierText;

    private float lastActiveTime;
    private float timePassed;
    private float timeCheckInterval = 60f;
    private float timeSinceLastCheck;
    double TotalMin;
    public CoinBagPulse coinBagPulse;

    private void Start()
    {
        DateTime date;
        if (PlayerPrefs.GetString("Date") == "" || PlayerPrefs.GetString("Date") == null)
        {
            TotalMin = 1;
        }
        else
        {
            date = Convert.ToDateTime(PlayerPrefs.GetString("Date"));
            DateTime NowDate = DateTime.Now;
            TotalMin = (NowDate - date).TotalMinutes;
        }

        currency = PlayerPrefs.GetInt("Currency", 0);

        
        if (PlayerPrefs.HasKey("TempBoost"))
            multiplier = PlayerPrefs.GetInt("TempBoost");
        else
            multiplier = PlayerPrefs.GetInt("Multiplier", 1);

        lastActiveTime = PlayerPrefs.GetFloat("LastActiveTime", Time.time);
        currency = (int)TotalMin + currency;
        PlayerPrefs.SetInt("Currency", currency);

        UpdateUI();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("Date", DateTime.Now.ToString());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                AddCurrency();
            }
        }

        timeSinceLastCheck += Time.deltaTime;

        if (timeSinceLastCheck >= timeCheckInterval)
        {
            CalculateCurrencyOverTime();
            timeSinceLastCheck = 0f;
        }
    }

    public void AddCurrency()
    {
        currency += 1 * multiplier;
        if (coinBagPulse != null)
            coinBagPulse.Pulse();
        PlayerPrefs.SetInt("Currency", currency);
        PlayerPrefs.Save();
        UpdateUI();
    }

    public void UpgradeMultiplier(int value)
    {
        multiplier += value;
        PlayerPrefs.SetInt("Multiplier", multiplier);
        PlayerPrefs.Save();
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (currencyText != null)
            currencyText.text = currency.ToString();

        if (multiplierText != null)
            multiplierText.text = " x" + multiplier;
    }

    private void CalculateCurrencyOverTime()
    {
        timePassed = Time.time - lastActiveTime;

        int currencyToAdd = Mathf.FloorToInt(timePassed / 60);
        if (currencyToAdd > 0)
        {
            currency += currencyToAdd;
            PlayerPrefs.SetInt("Currency", currency);
            PlayerPrefs.Save();

            lastActiveTime = Time.time;
            PlayerPrefs.SetFloat("LastActiveTime", lastActiveTime);
            UpdateUI();
        }
    }
}
