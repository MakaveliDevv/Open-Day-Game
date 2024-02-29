using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    Rigidbody2D rb;
    public int jumpPower;

    public Transform groundCheck;
    public LayerMask groundLayer;
    bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCapsule(new Vector2(groundCheck.position.x, groundCheck.position.y -.45f), new Vector2(1f, .3f), CapsuleDirection2D.Horizontal, 0f, groundLayer);
        if(Input.GetButtonDown("Jump") && isGrounded) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }   
    }
}
