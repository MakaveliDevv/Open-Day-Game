using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class ScalingController : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private BridgeManager bridgeManag;
    private Coroutine coroutine;

    // References
    public BridgeSO bridgeSO;
    private GameObject bridge;
    public GameObject instantiatePoint; 
    public GameObject detectPoint;
    private GameObject newPoint;
    private GameObject endPoint;
    private GameObject pivotPoint;

    // Scale
    private Vector3 initialScale;
    private Quaternion initialRotation;
    public bool isExpanding; 
    public bool isExpandingBack;
    public bool stopScaling;

    public float timer;
    public float timeUntilScaleBack = 4f;
    public float scaleFactor = 10f;

    void Start() 
    {
        player = GetComponentInParent<PlayerController>();

        // Scale
        initialScale = transform.localScale;

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
        if(bridge != null) 
        {
            if (Input.GetKeyDown(KeyCode.Space) && !player.isMoving)
            {
                // If scaling coroutine is running, stop it
                if (coroutine != null)
                    StopCoroutine(coroutine);

                if(!stopScaling) 
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
                if(!stopScaling) 
                {
                    isExpanding = false;
                    coroutine = StartCoroutine(ScaleBack(bridgeManag.transform.localScale, initialScale));
                }
            }
   

            if (stopScaling) 
            {
                timer += Time.deltaTime; // Accumulate elapsed time

                if (timer >= timeUntilScaleBack) // Check if the timer has reached or exceeded the desired time
                {
                    // Scale back
                    stopScaling = false;
                    coroutine = StartCoroutine(ScaleBack(bridgeManag.transform.localScale, initialScale));

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
                    stopScaling = false;
                    coroutine = StartCoroutine(ScaleBack(bridgeManag.transform.localScale, initialScale));

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
                    Debug.Log("Swag");
                    StartCoroutine(ExpandBackTowardsEndPoint(transform.localScale, detectPoint.transform.position));
                }
            }

        }
    }

    private void CreateBridge()
    {
        if(bridgeManag == null) 
        {
            bridgeManag = BridgeManager.instance;
        }
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            // Instantiate game object
            if (bridge == null)
            {
                bridge = Instantiate(bridgeSO.objectPrefab, instantiatePoint.transform.position, Quaternion.identity);
                bridge.name = "BridgePrefab";

                endPoint = GameObject.FindGameObjectWithTag("EndPoint");
                if (endPoint != null)
                {
                    newPoint = Instantiate(detectPoint, endPoint.transform.position, Quaternion.identity) as GameObject;
                }
                else
                {
                    Debug.LogError("No GameObject tagged with 'EndPoint' found.");
                }
            }

            if (bridge != null && player != null)
            {
                bridge.transform.SetParent(player.transform);
            }

        } 

        if (newPoint != null && endPoint != null)
        {
            newPoint.transform.position = endPoint.transform.position;
        }
    }

    IEnumerator Scaling(Vector3 _targetDirection)
    {
        while (true) // Continuously scale
        {
            Vector3 targetScale = bridgeManag.transform.localScale + scaleFactor * Time.deltaTime * _targetDirection;
            bridgeManag.transform.localScale = targetScale;
            isExpanding = true;

            // Check for connect points while scaling
            if (DetectionPoint.instance.PointDetected())
            {
                // Stop scaling
                FreezeScaling(bridgeManag.transform.localScale);
            } 
            yield return null;
        }
    }
    public IEnumerator ExpandBackTowardsEndPoint(Vector3 _startPosition, Vector3 _targetDirection)
    {
        // float journeyLength = Vector3.Distance(_startPosition, _targetDirection);
        float elapsedTime = 0f;
        float duration = 3f; // Adjust the duration as needed

        while (elapsedTime < duration)
        {
            // Interpolate between start and target scale
            float t = elapsedTime / duration;
            pivotPoint.transform.localScale = Vector3.Lerp(_startPosition, _targetDirection, t);
            
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the scale is exactly the target scale when done
        pivotPoint.transform.localScale = _targetDirection;

        Destroy(bridgeManag.gameObject);
    }

    public bool DestroyGameObject(GameObject _object) 
    {
        return true;
    }

    // public IEnumerator ExpandBackTowardsEndPoint(Vector3 _startPosition, Vector3 _targetDirection) 
    // {
    //     // Get distance from the startPoint to the endpoint
    //     // float startTime = Time.time;
    //     float journeyLength = Vector3.Distance(_startPosition, _targetDirection);

    //     GameObject pivotPoint = GameObject.Find("PivotPoint");
    //     Vector3 targetScale = new(0, 1, 1);
        
    //     while(pivotPoint.transform.localScale !=  targetScale) 
    //     {
    //         // float journeyTime = Time.time - startTime;
    //         float fracJourney = 3f * scaleFactor / journeyLength;
    //         pivotPoint.transform.localScale = Vector3.Lerp(_startPosition, _targetDirection, Mathf.Clamp01(fracJourney));
    //     }

    //     pivotPoint.transform.localScale = _targetDirection;

    //     Destroy(gameObject);

    //     yield return null;
    // }

    IEnumerator ScaleBack(Vector3 _startScale, Vector3 _targetScale)
    {
        // isExpandingBack = false;
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(_startScale, _targetScale);

        while (bridgeManag.transform.localScale != _targetScale)
        {
            float journeyTime = Time.time - startTime;
            float fracJourney = journeyTime * scaleFactor / journeyLength;
            bridgeManag.transform.localScale = Vector3.Lerp(_startScale, _targetScale, Mathf.Clamp01(fracJourney));

            isExpandingBack = true;
            isExpanding = false;

            yield return null;
        }

        // Ensure the scale is exactly the target scale when done
        bridgeManag.transform.localScale = _targetScale;
        isExpandingBack = false;

        // Reset stopScaling flag after scaling back
        stopScaling = false;
    }

    private void FreezeScaling(Vector3 _currentScale) 
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        bridgeManag.transform.localScale = _currentScale;
        stopScaling = true;

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
