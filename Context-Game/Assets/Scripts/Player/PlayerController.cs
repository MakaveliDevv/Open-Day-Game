using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    // Player
    public bool isMoving = false;
    [HideInInspector] public Rigidbody2D rb;
    public float fallMultiplier;
    private Vector2 vecGravity;
    public Transform castPosition;
    public LayerMask layermask;
    public float radius;

    // Movement
    [SerializeField] private float moveSpeed;
    [HideInInspector] public Vector2 inputDirection;

    // Scaling
    private Controller controller;
    public GameObject playerRenderer;
    public bool playerIsGrounded;
    private GameObject sphereVisualizer;
    
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponentInChildren<Controller>();

        vecGravity = new(0f, - Physics2D.gravity.y);
    }

    void Update() 
    { 
        inputDirection = new(Input.GetAxisRaw("Horizontal"), 0f);
        MovePlayer();

        Collider2D hit = Physics2D.OverlapCircle(castPosition.position, radius, layermask);
        if(hit != null) 
        {

            playerIsGrounded = true;

        } else 
        {
            playerIsGrounded = false;
        }
    }

    private void MovePlayer() 
    {
        isMoving = false;

        if(inputDirection != new Vector2(0f, 0f) 
        && !controller.isExpanding 
        && !controller.isExpandingBack
        && !controller.stopScalingCuzEndPointReached
        && playerIsGrounded) 
        {
            isMoving = true;

            inputDirection = transform.TransformDirection(inputDirection);
            inputDirection *= moveSpeed;
            rb.velocity = new(inputDirection.x, rb.velocity.y);
        
        } else 
        {
            isMoving = false;            
        }

        // Check if player is in the air
        if(rb.velocity.y < 0.03f) 
        {
            rb.velocity -= fallMultiplier * Time.deltaTime * vecGravity; 
        }        
    }
}
