using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask monsterCheck;

    public int extraJumps;
    public int jumpForce;

    public Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround) || Physics2D.OverlapCircle(groundCheck.position, checkRadius, monsterCheck);

    }

    private void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = 1;
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }
     
    }

    public void UpButtonClicked()
    {
        if(isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
        else
        {
            if(extraJumps > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
        }
    }

}
