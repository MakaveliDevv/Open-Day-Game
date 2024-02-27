using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player
    private CapsuleCollider2D capsuleCollider;
    [SerializeField] private LayerMask layerMask;
    // Movement
    private Rigidbody2D playerRB;
    public float movementSpeed;
    private Vector2 inputDirection;

    // Gravity
    public float jumpForce = 10f;
    public float gravityForce = 20f;
    private bool isGrounded = true;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
    }

    private void FixedUpdate() 
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        MovePlayer();
        Jump();
    }

    private void MovePlayer() 
    {
        inputDirection = transform.TransformDirection(inputDirection);
        inputDirection *= movementSpeed;
        playerRB.velocity = inputDirection;
    }

    private void Jump() 
    {
        if(IsGrounded() && Input.GetKeyDown(KeyCode.Space)) 
        {
            Debug.Log("Player is jumping...");
            playerRB.velocity = Vector2.up * jumpForce;
            isGrounded = false;
        }
    }

    private bool IsGrounded() 
    {
        if (capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider2D is not assigned to the player object.");
            return false;
        }

        float extraHeight = 0.01f;
        RaycastHit2D raycastHit = Physics2D.Raycast(capsuleCollider.bounds.center, Vector2.down, capsuleCollider.bounds.extents.y + extraHeight, layerMask);
        Color rayColor;
        
        if(raycastHit.collider != null) 
        {
            rayColor = Color.blue;
            isGrounded = true;
        } 
        else 
        {
            rayColor = Color.red;    
            isGrounded = false; // Set isGrounded to false if no ground is detected
        }

        Debug.DrawRay(capsuleCollider.bounds.center, Vector2.down * (capsuleCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.Log(raycastHit.collider);
        return isGrounded;
    }
}
