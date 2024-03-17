using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    // Player
    public bool isMoving = false;
    [HideInInspector] public Rigidbody2D rb;
    public float fallMultiplier;
    private Vector2 vecGravity;

    // Movement
    [SerializeField] private float movementSpeed;
    [HideInInspector] public Vector2 inputDirection;

    // Scaling
    private ScalingController scalingContr;


    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        scalingContr = GetComponentInChildren<ScalingController>();

        vecGravity = new(0f, - Physics2D.gravity.y);
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

        if(inputDirection != new Vector2(0f, 0f) 
        && !scalingContr.isExpanding 
        && !scalingContr.isExpandingBack
        && !scalingContr.stopScalingCuzEndPointReached) 
        {
            isMoving = true;

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
        }
    }
}
