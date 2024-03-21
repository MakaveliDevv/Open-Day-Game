using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    public float speed = 6f;
    private bool playerDetected = false;
    private bool playerMovingOnLadder = false;

    private void OnTriggerStay2D(Collider2D collider) 
    {
        if (collider.GetComponent<PlayerManager>()) 
        {
            // PlayerManager player  = collider.GetComponent<PlayerManager>();

            // if(player.playerType == PlayerManager.PlayerType.DEVELOPER) 
            //     return;


            playerDetected = true; // Set the flag to true when player is detected
            rbPlayer = collider.GetComponent<Rigidbody2D>();
            
            // Handle vertical movement here if needed
            float verticalInput = Input.GetAxisRaw("Vertical");

            if(verticalInput > 0) 
            {
                playerMovingOnLadder = true;
                rbPlayer.velocity = new(0, speed); // Up
            } 
            else if(verticalInput < 0) 
            {
                playerMovingOnLadder = true;
                rbPlayer.velocity = new(0, -speed); // Down
            } 
            else 
            {
                playerMovingOnLadder = false;
                rbPlayer.velocity = new(0, rbPlayer.velocity.y); // No input
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.GetComponent<PlayerManager>()) 
        {
            playerDetected = false; // Reset the flag when player exits the trigger area
        }
    }

    // ONLY THE DESIGNER AND DEVELOPER MAY CLIMB THE LADDER


}
