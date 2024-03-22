using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class TestingInputSystem : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Rigidbody2D rb;
    public float jumpForce;
    public float moveSpeed;
    private PlayerInputActions _playerActions ;


    void Awake() 
    {
        _playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();

        _playerActions = new();
        _playerActions.Player.Jump.performed += Jump;
        _playerActions.Player.Enable();
        // _playerActions.Player.Movement.performed += Movement_Performed;
    }

    private void Update() 
    {
        if(Keyboard.current.tKey.wasPressedThisFrame) 
        {
            _playerInput.SwitchCurrentActionMap("UI");
        }

        if(Keyboard.current.yKey.wasPressedThisFrame) 
        {
            _playerInput.SwitchCurrentActionMap("Player");
        }
    }

    private void FixedUpdate() 
    {
        Vector2 inputVector = _playerActions.Player.Movement.ReadValue<Vector2>();
        rb.AddForce(new Vector2(inputVector.x, inputVector.y) * moveSpeed, ForceMode2D.Force);
    }   

    // private void Movement_Performed(InputAction.CallbackContext context)
    // {
    //     Debug.Log(context);
    //     Vector2 inputVector = context.ReadValue<Vector2>();
    //     rb.AddForce(new Vector2(inputVector.x, inputVector.y) * moveSpeed, ForceMode2D.Force);
    // }

    public void Jump(InputAction.CallbackContext context) 
    {
        Debug.Log(context);

        if(context.performed) 
        {   
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb.velocity = new(rb.velocity.x, jumpForce);
            Debug.Log("Jump" + context.phase);
        
        }
    }

    public void Submit(InputAction.CallbackContext context) 
    {
        Debug.Log("Submit: " + context);
    }
}
