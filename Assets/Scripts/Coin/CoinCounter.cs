using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter Instance;

    public TMP_Text coinText;
    public int currentCoin;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadSavedScore();
    }

    public void IncreaseCoin(int v)
    {
        currentCoin += v;
        UpdateCoinText();
        SaveScore();
    }

    public void UpdateCoinText()
    {
        coinText.text = currentCoin.ToString();
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("PlayerScore", currentCoin);
        PlayerPrefs.Save();
    }

    private void LoadSavedScore()
    {
        currentCoin = PlayerPrefs.GetInt("PlayerScore", 0);
        UpdateCoinText();
    }
}
