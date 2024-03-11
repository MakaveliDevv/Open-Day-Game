using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;

    // Movement
    public float movementSpeed;
    private Vector2 inputDirection;

    // Jump
    [Header("Jump System")]
    public float jumpTime;
    public int jumpForce;
    public float fallMultiplier;
    public float jumpMultiplier;

    private bool isJumping;
    private float jumpCounter;

    private Vector2 vecGravity;

    [SerializeField] private LayerMask groundLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Start() 
    {
        vecGravity = new(0f, -Physics2D.gravity.y);
    }

    void Update() 
    {
        inputDirection = new(Input.GetAxisRaw("Horizontal"), 0f);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        Jump();
        MovePlayer();
        
        if(rb.velocity.y < 0.03f) 
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;

            if(jumpCounter > jumpTime) 
                isJumping = false;

            rb.velocity += vecGravity * jumpMultiplier * Time.deltaTime;    
        }      
    }

    private void MovePlayer() 
    {
        inputDirection = transform.TransformDirection(inputDirection);
        inputDirection *= movementSpeed;
                
        rb.velocity = new(inputDirection.x, rb.velocity.y);
    }

    private void Jump() 
    {
        if(IsGrounded() && Input.GetButtonDown("Jump")) 
        {
            rb.velocity = new(rb.velocity.x, jumpForce);
            isJumping = true;
            jumpCounter = 0f;
        }
    }

    private bool IsGrounded() 
    {
        Vector2 capsuleBottom = new(capsuleCollider.bounds.center.x, capsuleCollider.bounds.min.y);
        float capsuleRadius = capsuleCollider.size.x * 0.5f;
        float checkDistance = 0.55f; // Adjust this value as needed

        RaycastHit2D hit = Physics2D.CircleCast(capsuleBottom, capsuleRadius, Vector2.down, checkDistance, groundLayer);
        return hit.collider != null;
    }
}
