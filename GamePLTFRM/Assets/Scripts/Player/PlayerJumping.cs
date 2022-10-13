using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    [Header ("Player Jumping Settings")]
    [Range (0, 20)] public float jumpForce;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJump;
    public int extraJumpValue;

    bool canJump;
    bool canWallJump;

    PlayerMovement player;
    private Rigidbody2D  rb;
    
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        extraJump = extraJumpValue;
    }

    void Update()
    {
        if (isGrounded == true || canWallJump == true)
        {
            canJump = true;
            extraJump = extraJumpValue;
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
    
        if(Input.GetKeyDown(KeyCode.Space) && extraJump > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJump--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJump == 0 && isGrounded == true)
        {
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
