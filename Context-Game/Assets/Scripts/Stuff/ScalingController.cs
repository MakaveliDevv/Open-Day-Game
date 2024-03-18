using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ScalingController : MonoBehaviour
{
    public BridgeSO bridgeSO;
    public BridgeManager bridgeManag;
    private PlayerController player;
    private Coroutine coroutine;

    // References
    private GameObject newBridge; 

    // Points
    public GameObject detectPoint;
    private GameObject newDetectPoint;

    public GameObject startingPoint;
    private GameObject newStartingPoint;
    
    public Transform instantiatePoint; 
    private Transform pivotStartPoint;
    private Transform pivotEndPoint;
    private GameObject pivotPoint;

    // Scale
    private Vector3 initialScale;
    private Quaternion initialRotation;
    public bool isExpanding; 
    public bool isExpandingBack;
    public bool stopScalingCuzEndPointReached;

    public float timer;
    public float timeUntilScaleBack = 4f;
    public float scaleFactor = 10f;

    void Start() 
    {
        player = GetComponentInParent<PlayerController>();

        // Scale
        initialScale = bridgeSO.objectPrefab.transform.localScale;

        // Rotation
        initialRotation = transform.rotation;
    }

    void Update() 
    {
        if(pivotPoint == null) 
        {
            pivotPoint = GameObject.Find("PivotPoint");
        }

        CreateBridge();
        InputToCreateScaleBridge();
    }

    private void InputToCreateScaleBridge()
    {
        if(newBridge == null) 
            return;

        if (Input.GetKeyDown(KeyCode.Space) && !player.isMoving)
        {
            // If scaling coroutine is running, stop it
            if (coroutine != null)
                StopCoroutine(coroutine);

            if(!stopScalingCuzEndPointReached) 
            {
                isExpandingBack = false; // Need to set this otherwise it bugs when pressing the button down to fast
                coroutine = StartCoroutine(Scaling(Vector2.right));
            
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
                coroutine = StartCoroutine(ScaleBack(newBridge.transform.localScale, initialScale));
            }
        }


        if (stopScalingCuzEndPointReached) 
        {
            timer += Time.deltaTime; // Accumulate elapsed time

            if (timer >= timeUntilScaleBack) // Check if the timer has reached or exceeded the desired time
            {
                // Scale back
                stopScalingCuzEndPointReached = false;
                coroutine = StartCoroutine(ScaleBack(newBridge.transform.localScale, initialScale));

                if(Input.GetKeyDown(KeyCode.Space)) 
                {
                    if(coroutine != null)
                        StopCoroutine(coroutine);

                    StartCoroutine(Scaling(Vector2.right)); 
                }

                // Reset the timer for the next iteration
                timer = 0;
            
            } else if(Input.GetKeyDown(KeyCode.R)) 
            {
                stopScalingCuzEndPointReached = false;
                coroutine = StartCoroutine(ScaleBack(newBridge.transform.localScale, initialScale));

                if(Input.GetKeyDown(KeyCode.Space)) 
                {
                    if(coroutine != null)
                        StopCoroutine(coroutine);

                    StartCoroutine(Scaling(Vector2.right)); 
                }

                // Reset the timer for the next iteration
                timer = 0;
                    
            } else if(Input.GetKeyDown(KeyCode.L)) 
            {
                // Move player to the endpoint
                StartCoroutine(ExpandBackTowardsEndPoint(transform.localScale));
            }
        }
    }

    private void CreateBridge()
    {    
        if (Input.GetKeyDown(KeyCode.G))
        {
            // Instantiate game object
            if (newBridge == null)
            {
                newBridge = Instantiate(bridgeSO.objectPrefab, instantiatePoint.transform.position, Quaternion.identity);
                newBridge.name = "BridgePrefab";

                pivotStartPoint = GameObject.FindGameObjectWithTag("StartPoint").transform;
                pivotEndPoint = GameObject.FindGameObjectWithTag("EndPoint").transform;
            
                if (pivotEndPoint != null && pivotStartPoint != null)
                {
                    newDetectPoint = Instantiate(detectPoint, pivotEndPoint.transform.position, Quaternion.identity) as GameObject;
                    newDetectPoint.name = "NewDetectPointPrefab";

                    newStartingPoint = Instantiate(startingPoint, pivotStartPoint.transform.position, Quaternion.identity);
                    newStartingPoint.name = "NewStartPointPrefab";

                }
                else
                {
                    Debug.LogError("No GameObject tagged with 'EndPoint' found.");
                }
            }

            if (newBridge != null && player != null)
            {
                newBridge.transform.SetParent(player.transform);
            }

        } 

        if (newDetectPoint != null && pivotEndPoint != null && newStartingPoint != null && pivotStartPoint != null)
        {
            newStartingPoint.transform.position = pivotStartPoint.transform.position;
            newDetectPoint.transform.position = pivotEndPoint.transform.position;
        }
    }

    IEnumerator Scaling(Vector3 _targetDirection)
    {
        while (true) // Continuously scale
        {
            Vector3 initialScale = new(newBridge.transform.localScale.x, bridgeSO.height, bridgeSO.depth);
            Vector3 targetScale = initialScale + scaleFactor * Time.deltaTime * _targetDirection;
            newBridge.transform.localScale = targetScale;
            isExpanding = true;

            // Check for connect points while scaling
            if (DetectionPoint.instance.PointDetected())
            {
                // Stop scaling
                FreezeScaling(newBridge.transform.localScale);
            } 
            yield return null;
        }
    }

    public IEnumerator ExpandBackTowardsEndPoint(Vector3 _startPosition)
    {
        float elapsedTime = 0f;
        float duration = 2f;
        Vector3 targetScale = new(0, 1, 1);
        Point _point = newStartingPoint.GetComponent<Point>();

        while (elapsedTime < duration)
        {
            // Interpolate between start and target scale
            float t = elapsedTime / duration;
            pivotPoint.transform.localScale = Vector3.Lerp(_startPosition, targetScale, t);
            
            if(_point.Movingg(newStartingPoint.transform)) 
            {
                Debug.Log("We are moving");
                player.playerRenderer.SetActive(false);
            }
            
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }            

        // Ensure the scale is exactly the target scale when done
        pivotPoint.transform.localScale = targetScale;

        // If the pivoitPoint is at the target position (detection position)
        if(pivotPoint.transform.position == newDetectPoint.transform.position) 
        {
            // Set player active at that position
            Debug.Log("Reached the endpoint");
            player.playerRenderer.SetActive(true);
            player.transform.position = pivotEndPoint.transform.position;
        }

        if(DestroyGameObject(newBridge)) 
        {
            DestroyGameObject(newDetectPoint);
            DestroyGameObject(newStartingPoint);

            stopScalingCuzEndPointReached = false;
            _point.isMoving = false;
            
            player.rb.velocity = player.inputDirection;
        }
    }

    public bool DestroyGameObject(GameObject _object) 
    {
        Destroy(_object);

        return true;
    }

    IEnumerator ScaleBack(Vector3 _startScale, Vector3 _targetScale)
    {
        // isExpandingBack = false;
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(_startScale, _targetScale);           

        while (newBridge.transform.localScale != _targetScale)
        {
            float journeyTime = Time.time - startTime;
            float fracJourney = journeyTime * scaleFactor / journeyLength;
            newBridge.transform.localScale = Vector3.Lerp(_startScale, _targetScale, Mathf.Clamp01(fracJourney));

            isExpandingBack = true;
            isExpanding = false;

            yield return null;
        }

        // Ensure the scale is exactly the target scale when done
        newBridge.transform.localScale = _targetScale;
        isExpandingBack = false;

        // Reset stopScaling flag after scaling back
        stopScalingCuzEndPointReached = false;
    }

    private void FreezeScaling(Vector3 _currentScale) 
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        newBridge.transform.localScale = _currentScale;
        stopScalingCuzEndPointReached = true;

        // Reset expansion state flags
        isExpanding = false;
        isExpandingBack = false;
    }

        // Now start rotating back to the initial rotation
        // startTime = Time.time;
        // journeyLength = Quaternion.Angle(startRotation, targetRotation);

        // while (transform.rotation != targetRotation)
        // {
        //     float journeyTime = Time.time - startTime;
        //     float fracJourney = journeyTime * player.scaleFactor / journeyLength;
        //     transform.rotation = Quaternion.Slerp(startRotation, targetRotation, Mathf.Clamp01(fracJourney));

        //     yield return null;
        // }

        // Ensure the rotation is exactly the initial rotation when done
        // transform.rotation = targetRotation;
    
        // Allow player to move again
        // player.rb.velocity = new Vector2(player.inputDirection.x, player.rb.velocity.y);
        // player.isMoving = true;
}
