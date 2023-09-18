using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    [Header("Jump Sound")]
    [SerializeField] private AudioClip jumpSound;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider  = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = SimpleInput.GetAxis("Horizontal");
        

        //Flip Player
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.0f)
            transform.localScale = new Vector3(-1, 1, 1);


        
        //animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());


        //wall jump
        if (wallJumpCooldown < 0.2f)
        {     

            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);


            if (onWall() && !isGrounded())
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            }

            else
                rb.gravityScale = 3;

            //if (Input.GetKey(KeyCode.Space) && isGrounded())
                //Jump();

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();

                if(Input.GetKeyDown(KeyCode.Space)&& isGrounded())
                    SoundScript.instance.PlaySound(jumpSound);


            }
        }
        else
            wallJumpCooldown += Time.deltaTime;

    }

    public void Attack()
    {
        canAttack();
    }

    public void Jump()
    {
        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            anim.SetTrigger("jump");
            SoundScript.instance.PlaySound(jumpSound); // Play jump sound here
        }
        else if (onWall() && !isGrounded())
        {
            wallJumpCooldown = 0;
            rb.gravityScale = 3; // Restore gravity scale
            rb.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * speed, jumpPower); // Adjust the direction based on facing
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null; 
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

}