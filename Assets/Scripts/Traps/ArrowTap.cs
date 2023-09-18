using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0;
        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindArrow()
    {
        
        Vector3 firingDirection = firePoint.right;

        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;

            
            Vector3 arrowDirection = (arrows[i].transform.position - firePoint.position).normalized;
            float dotProduct = Vector3.Dot(arrowDirection, firingDirection);

            
            if (dotProduct >= 0.9f)
                return i;
        }

        return 0; 
    }


    private void Update()
    {

        cooldownTimer += Time.deltaTime;

        if(cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }

}
