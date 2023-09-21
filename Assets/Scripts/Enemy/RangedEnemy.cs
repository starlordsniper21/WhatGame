using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private Health playerHealth;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioClip attacksound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight() && playerHealth != null)
        {
            if (cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0)
            {
                cooldownTimer = 0;
                anim.SetTrigger("rangedAttack");
                if (SoundScript.instance != null)
                {
                    SoundScript.instance.PlaySound(attacksound);
                }
            }
        }

    }

    private bool PlayerInSight()
    {
        Vector2 castDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;

        RaycastHit2D hit = Physics2D.BoxCast(
            (Vector2)(boxCollider.bounds.center + (Vector3)(castDirection * range * transform.localScale.x * colliderDistance)), // Explicitly cast both to Vector2
            new Vector2(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y), // Explicitly cast to Vector2
            0, castDirection, 0, playerLayer);

        return hit.collider != null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 gizmoPosition = boxCollider.bounds.center;

        if (playerHealth != null)
        {

            if (spriteRenderer.flipX)
            {
                gizmoPosition -= transform.right * range * transform.localScale.x * colliderDistance;
            }
            else
            {
                gizmoPosition += transform.right * range * transform.localScale.x * colliderDistance;
            }

            Gizmos.DrawWireCube(gizmoPosition,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        }
    }


}
