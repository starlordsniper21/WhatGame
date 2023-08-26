using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter Instance;

    public TMP_Text coinText;
    public int currentCoin = 0;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        coinText.text = "Score : " + currentCoin.ToString();
    }

    public void IncreaseCoin(int v)
    {
        currentCoin += v;
        coinText.text = "Score : " + currentCoin.ToString();
    }

}
