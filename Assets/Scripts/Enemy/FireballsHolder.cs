using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    private void Update()
    {
        // Check if the enemy reference is null before accessing it
        if (enemy != null)
        {
            transform.localScale = enemy.localScale;
        }
        else
        {
            // Handle the case where the enemy has been destroyed or is null
            // For example, you can destroy this Fireball object as well
            Destroy(gameObject);
        }
    }
}
