using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMovement : MonoBehaviour
{
    [Header ("Player Jumping Settings")]
    [Range (0, 20)] public float jumpForce;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    bool canJump;
    bool canWallJump;

    PlayerMovement player;
    private Rigidbody2D  rb;
    
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (isGrounded == true || canWallJump == true)
        {
            canJump = true;
        }
        else
        {
            canJump  = false;
        }


        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            DoJump();
            rb.velocity = Vector2.up * jumpForce;
        }
    }
 
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    public void DoJump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            canWallJump = true;
        }
    }

    public void OnCollisionExit2D(Collision2D  collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            canWallJump = false;
        }
    }
}
