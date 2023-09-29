using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private Button attackButton;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        attackButton = GameObject.Find("Fire Button").GetComponent<Button>();
        attackButton.onClick.AddListener(Attack);
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        if (cooldownTimer > attackCooldown)
        {
            if (SoundScript.instance != null)
            {
                SoundScript.instance.PlaySound(fireballSound);
            }
            else
            {
                Debug.LogWarning("SoundScript.instance is null.");
            }

            anim.SetTrigger("Attack");
            cooldownTimer = 0;

            int fireballIndex = FindFireball();
            if (fireballIndex >= 0 && fireballIndex < fireballs.Length)
            {
                float direction = transform.localScale.x >= 0 ? 1f : -1f;
                fireballs[fireballIndex].transform.position = firePoint.position;
                fireballs[fireballIndex].GetComponent<Projectile>().SetDirection(direction);

            }
            else
            {
                Debug.LogWarning("Invalid fireball index: " + fireballIndex);
            }
        }
        else
        {
            Debug.LogWarning("Attack conditions not met.");
        }
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
