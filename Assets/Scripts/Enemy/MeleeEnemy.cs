using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private Health playerHealth;

    [SerializeField] private float MovementDistance;
    [SerializeField] private float speed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private SpriteRenderer spriteRenderer;

    [Header("Attack Sound Layer")]
    [SerializeField] private AudioClip attacksound;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        leftEdge = transform.position.x - MovementDistance;
        rightEdge = transform.position.x + MovementDistance;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight() && playerHealth != null)
        {
            if (cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
                if (SoundScript.instance != null)
                {
                    SoundScript.instance.PlaySound(attacksound);
                }
            }
        }

        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = false;
            spriteRenderer.flipX = false;
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = true;
            spriteRenderer.flipX = true;

        }

    }

    private bool PlayerInSight()
    {
        Vector2 castDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;

        RaycastHit2D hit = Physics2D.BoxCast(
            (Vector2)(boxCollider.bounds.center + (Vector3)(castDirection * range * transform.localScale.x * colliderDistance)), // Explicitly cast both to Vector2
            new Vector2(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y), // Explicitly cast to Vector2
            0, castDirection, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
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


    private void DamagePlayer()
    {
        if (PlayerInSight() && playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
