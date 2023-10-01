using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERMOVEMENT : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Adjust the move speed as needed
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        // Handle left movement when the "LeftButton" is clicked.
        if (Input.GetButtonDown("LeftButton"))  
        {
            MoveLeft();
        }

        // Handle right movement when the "RightButton" is clicked.
        if (Input.GetButtonDown("RightButton"))
        {
            MoveRight();
        }
    }

    public void MoveLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }

    public void MoveRight()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }
}