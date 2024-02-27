using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    private Rigidbody2D playerRB;
    public float movementSpeed;
    private float input;

    private Vector2 inputDirection;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), playerRB.velocity.y);
    }

    private void FixedUpdate() 
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        MovePlayer();
    }

    private void MovePlayer() 
    {
        inputDirection = transform.TransformDirection(inputDirection);
        inputDirection *= movementSpeed;

        playerRB.velocity = inputDirection;
    }
}
