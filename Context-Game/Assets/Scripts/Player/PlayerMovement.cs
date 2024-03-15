using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player
    [HideInInspector] public bool isMoving = false;

    [HideInInspector]public Rigidbody2D rb;
    // private CapsuleCollider2D capsuleCollider;

    // Movement
    [SerializeField] private float movementSpeed;
    [HideInInspector] public Vector2 inputDirection;

    // Jump
    // [Header("Jump System")]
    // public float jumpTime;
    // public int jumpForce;
    public float fallMultiplier;
    // public float jumpMultiplier;
    private Vector2 vecGravity;

    // [SerializeField] private LayerMask groundLayer;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        // capsuleCollider = GetComponent<CapsuleCollider2D>();
        vecGravity = new(0f, -Physics2D.gravity.y);
    }

    void Update() 
    { 
        inputDirection = new(Input.GetAxisRaw("Horizontal"), 0f);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        MovePlayer();
    }

    private void MovePlayer() 
    {
        isMoving = false;

        if(inputDirection != new Vector2(0f, 0f)) 
        {
            isMoving = true;

            Debug.Log(inputDirection);
            inputDirection = transform.TransformDirection(inputDirection);
            inputDirection *= movementSpeed;
            rb.velocity = new(inputDirection.x, rb.velocity.y);
        
        } else 
        {
            isMoving = false;
        }
        // Check if player is in the air
        if(rb.velocity.y < 0.03f) 
        {
            rb.velocity -= fallMultiplier * Time.deltaTime * vecGravity;

            // if(jumpCounter > jumpTime) 
            //     isJumping = false;

            // rb.velocity += jumpMultiplier * Time.deltaTime * vecGravity;    
        }     
    }

        


    // private void Jump() 
    // {
    //     if(IsGrounded() && Input.GetButtonDown("Jump")) 
    //     {
    //         rb.velocity = new(rb.velocity.x, jumpForce);
    //         isJumping = true;
    //         jumpCounter = 0f;
    //     }
    // }

    // private bool IsGrounded() 
    // {
    //     Vector2 capsuleBottom = new(capsuleCollider.bounds.center.x, capsuleCollider.bounds.min.y);
    //     float capsuleRadius = capsuleCollider.size.x * 0.5f;
    //     float checkDistance = 0.55f; // Adjust this value as needed

    //     RaycastHit2D hit = Physics2D.CircleCast(capsuleBottom, capsuleRadius, Vector2.down, checkDistance, groundLayer);
    //     return hit.collider != null;
    // }

    // Method to control player movement status
    // public void SetCanMove(bool move)
    // {
    //     canMove = move;
    // }
}
