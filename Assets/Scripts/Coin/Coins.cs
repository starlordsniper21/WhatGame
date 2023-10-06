using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int value;
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        CoinCounter.Instance.IncreaseCoin(value);
    }


}
