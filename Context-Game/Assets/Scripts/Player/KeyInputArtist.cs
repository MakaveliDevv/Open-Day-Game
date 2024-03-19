using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputArtist : Controller
{
    private PlayerManager playerManag;


    void Awake() 
    {
        if(playerManag != null) 
        {
            playerManag = PlayerManager.instance;
        }
    }

    void Update() 
    {
        if(detectObj != null && endPivPoint != null && startPointObj != null && startPivPoint != null) 
        {
            startPointObj.transform.position = startPivPoint.transform.position;
            detectObj.transform.position = endPivPoint.transform.position;
        }

        if(Input.GetKeyDown(KeyCode.G)) // Check also which player it is 
        {
            CreateObject();
        }

        ScaleInput();
    }
    

    private void ScaleInput() 
    {
        // Check if bridge is not active
        if(_instantiateObj == null) 
            return;

            if (Input.GetKeyDown(KeyCode.Space) && !player.isMoving)
        {
            // If scaling coroutine is running, stop it
            if (coroutine != null)
                StopCoroutine(coroutine);

            if(!stopScalingCuzEndPointReached) 
            {
                isExpandingBack = false; // Need to set this otherwise it bugs when pressing the button down to fast
                coroutine = StartCoroutine(Scalingg(Vector3.right));
            } 
            
        } else if (Input.GetKeyUp(KeyCode.Space)) 
        {            
            // If scaling coroutine is running, stop it and start scaling back
            if (coroutine != null) 
                StopCoroutine(coroutine);

            // If endpoint is detected cant scale back
            if(!stopScalingCuzEndPointReached) 
            {
                isExpanding = false;
                coroutine = StartCoroutine(ScaleBack(_instantiateObj.transform.localScale, _scriptObj.initialScale));
            }
        }


        if (stopScalingCuzEndPointReached) 
        {
            timer += Time.deltaTime; // Accumulate elapsed time

            if (timer >= timeUntilScaleBack) // Check if the timer has reached or exceeded the desired time
            {
                // Scale back
                coroutine = StartCoroutine(ScaleBack(_instantiateObj.transform.localScale, _scriptObj.initialScale));
                stopScalingCuzEndPointReached = false;

                if(Input.GetKeyDown(KeyCode.Space)) 
                {
                    if(coroutine != null)
                        StopCoroutine(coroutine);

                    StartCoroutine(Scalingg(Vector3.right)); 
                }

                // Reset the timer for the next iteration
                timer = 0;
            
            } else if(Input.GetKeyDown(KeyCode.R)) 
            {
                stopScalingCuzEndPointReached = false;
                coroutine = StartCoroutine(ScaleBack(_instantiateObj.transform.localScale, _scriptObj.initialScale));

                if(Input.GetKeyDown(KeyCode.Space)) 
                {
                    if(coroutine != null)
                        StopCoroutine(coroutine);

                    StartCoroutine(Scalingg(Vector3.right)); 
                }

                // Reset the timer for the next iteration
                timer = 0;
                    
            } else if(Input.GetKeyDown(KeyCode.L)) 
            {
                // Move player to the endpoint
                stopScalingCuzEndPointReached = false;
                StartCoroutine(ExpandBackTowardsEndPoint(transform.localScale));
                timer = 0;
            }
        }
        
    }
}
