using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPlayerInput : ScalingController
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
        // if(pivotPoint == null) 
        // {
        //     pivotPoint = GameObject.Find("PivotPoint");
        // }

        // CreateObject();
        
        // CreateBridge();
        // ScaleInput();
    }
    
    private void CreateObject(GameObject _instantiateObj, Transform _startPoint, Transform _endPoint) 
    {
       if (Input.GetKeyDown(KeyCode.P))
        {
            // Instantiate game object
            if (instantiatedObject == null)
            {   
                // Game Object
                instantiatedObject = Instantiate(_scriptObj.@object, instantiatePoint.transform.position, _scriptObj.finalRotation);

                startPivPoint = GameObject.FindGameObjectWithTag("StartPivPoint").transform;
                endPivPoint = GameObject.FindGameObjectWithTag("EndPivPoint").transform;

                // Detect point Game Object (End Pivot Point) 
                detectObj = Instantiate(_scriptObj.detectObj, endPivPoint.position, Quaternion.identity) as GameObject;
                detectObj.name = "Detect Point";

                // Start Pivot Point
                startPointObj = Instantiate(_scriptObj.startObj, startPivPoint.position, Quaternion.identity);
                startPointObj.name = "NewStartPointPrefab";
            }

            if (instantiatedObject != null && player != null)
                instantiatedObject.transform.SetParent(player.transform);
        } 

        if(detectObj != null && endPivPoint != null && startPointObj != null && startPivPoint != null) 
        {
            startPointObj.transform.position = startPivPoint.transform.position;
            detectObj.transform.position = endPivPoint.transform.position;
        }
    }
    // private void CreateBridge()
    // {    
    //     if (Input.GetKeyDown(KeyCode.G))
    //     {
    //         // Instantiate game object
    //         if (newBridge == null)
    //         {
    //             newBridge = Instantiate(bridgeSO.objectPrefab, instantiatePoint.transform.position, bridgeSO.finalRotation);

    //             pivotStartPoint = GameObject.FindGameObjectWithTag("StartPoint").transform;
    //             pivotEndPoint = GameObject.FindGameObjectWithTag("EndPoint").transform;
            
    //             if (pivotEndPoint != null && pivotStartPoint != null)
    //             {
    //                 newDetectPoint = Instantiate(detectPoint, pivotEndPoint.transform.position, Quaternion.identity) as GameObject;
    //                 newDetectPoint.name = "NewDetectPointPrefab";

    //                 newStartingPoint = Instantiate(startingPoint, pivotStartPoint.transform.position, Quaternion.identity);
    //                 newStartingPoint.name = "NewStartPointPrefab";

    //             }
    //             else
    //             {
    //                 Debug.LogError("No GameObject tagged with 'EndPoint' found.");
    //             }
    //         }

    //         if (newBridge != null && player != null)
    //         {
    //             newBridge.transform.SetParent(player.transform);
    //         }

    //     } 

    //     if (newDetectPoint != null && pivotEndPoint != null && newStartingPoint != null && pivotStartPoint != null)
    //     {
    //         newStartingPoint.transform.position = pivotStartPoint.transform.position;
    //         newDetectPoint.transform.position = pivotEndPoint.transform.position;
    //     }
    // }
    // private void ScaleInput() 
    // {
    //     // Check if bridge is not active
    //     if(newBridge == null) 
    //         return;

    //         if (Input.GetKeyDown(KeyCode.Space) && !player.isMoving)
    //     {
    //         // If scaling coroutine is running, stop it
    //         if (coroutine != null)
    //             StopCoroutine(coroutine);

    //         if(!stopScalingCuzEndPointReached) 
    //         {
    //             isExpandingBack = false; // Need to set this otherwise it bugs when pressing the button down to fast
    //             coroutine = StartCoroutine(Scaling(Vector3.right));
    //         } 
            
    //     } else if (Input.GetKeyUp(KeyCode.Space)) 
    //     {            
    //         // If scaling coroutine is running, stop it and start scaling back
    //         if (coroutine != null) 
    //             StopCoroutine(coroutine);

    //         // If endpoint is detected cant scale back
    //         if(!stopScalingCuzEndPointReached) 
    //         {
    //             isExpanding = false;
    //             coroutine = StartCoroutine(ScaleBack(newBridge.transform.localScale, initialScale));
    //         }
    //     }


    //     if (stopScalingCuzEndPointReached) 
    //     {
    //         timer += Time.deltaTime; // Accumulate elapsed time

    //         if (timer >= timeUntilScaleBack) // Check if the timer has reached or exceeded the desired time
    //         {
    //             // Scale back
    //             coroutine = StartCoroutine(ScaleBack(newBridge.transform.localScale, initialScale));
    //             stopScalingCuzEndPointReached = false;

    //             if(Input.GetKeyDown(KeyCode.Space)) 
    //             {
    //                 if(coroutine != null)
    //                     StopCoroutine(coroutine);

    //                 StartCoroutine(Scaling(Vector3.right)); 
    //             }

    //             // Reset the timer for the next iteration
    //             timer = 0;
            
    //         } else if(Input.GetKeyDown(KeyCode.R)) 
    //         {
    //             stopScalingCuzEndPointReached = false;
    //             coroutine = StartCoroutine(ScaleBack(newBridge.transform.localScale, initialScale));

    //             if(Input.GetKeyDown(KeyCode.Space)) 
    //             {
    //                 if(coroutine != null)
    //                     StopCoroutine(coroutine);

    //                 StartCoroutine(Scaling(Vector3.right)); 
    //             }

    //             // Reset the timer for the next iteration
    //             timer = 0;
                    
    //         } else if(Input.GetKeyDown(KeyCode.L)) 
    //         {
    //             // Move player to the endpoint
    //             stopScalingCuzEndPointReached = false;
    //             StartCoroutine(ExpandBackTowardsEndPoint(transform.localScale));
    //             timer = 0;
    //         }
    //     }
        
    // }
}
