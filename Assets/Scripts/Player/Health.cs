using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField]private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Death sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private const int PlayerLayer = 10;
    private const int EnemyLayer = 11;
    private const int FlashWaitFactor = 2;

    private bool invulnerable;


    private GameOverManager gameOverManager;


    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        gameOverManager = FindObjectOfType<GameOverManager>();

    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage,0,startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundScript.instance.PlaySound(hurtSound);
        }
        else
        {
            if(!dead)
            {
                anim.SetTrigger("die");

                if (GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().enabled = false;
                    dead = true;
                    SoundScript.instance.PlaySound(deathSound);
                    gameOverManager.gameOver();
                    
                    // Show game over screen for player
                }
                else if (GetComponent<MeleeEnemy>() != null)
                {
                    GetComponent<MeleeEnemy>().enabled = false;
                    Destroy(gameObject);
                }
                else if (GetComponent<RangedEnemy>() != null)
                {
                    GetComponent<RangedEnemy>().enabled = false;
                    Destroy(gameObject);
                }

            }
            
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }


    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(PlayerLayer, EnemyLayer, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * FlashWaitFactor));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * FlashWaitFactor));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }


}
