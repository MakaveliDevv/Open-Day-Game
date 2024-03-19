using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Controller : MonoBehaviour
{
    public ScriptObject _scriptObj;
    protected PlayerController player;
    protected Coroutine coroutine;

    // Object Points
    public GameObject pivotPoint;
    protected GameObject _instantiateObj;
    protected GameObject startPointObj, detectObj;

    // Transform
    [SerializeField] protected Transform instantiatePoint, startPivPoint, endPivPoint;


    public bool isExpanding; 
    public bool isExpandingBack;
    public bool stopScalingCuzEndPointReached;

    public float timer;
    public float timeUntilScaleBack = 4f;
    public float scaleFactor = 10f;
    public Vector3 teleportOffset;

    void Start() 
    {
        player = GetComponentInParent<PlayerController>();
    }

    protected void CreateObject() 
    {
        if (_instantiateObj == null)
        {   
            // Game Object
            _instantiateObj = Instantiate(_scriptObj.@object, instantiatePoint.transform.position, _scriptObj.finalRotation);

            startPivPoint = GameObject.FindGameObjectWithTag("StartPivPoint").transform;
            endPivPoint = GameObject.FindGameObjectWithTag("EndPivPoint").transform;

            // Detect point Game Object (End Pivot Point) 
            detectObj = Instantiate(_scriptObj.detectObj, endPivPoint.position, Quaternion.identity) as GameObject;
            detectObj.name = "Detect Point";

            // Start Pivot Point
            startPointObj = Instantiate(_scriptObj.startObj, startPivPoint.position, Quaternion.identity);
            startPointObj.name = "NewStartPointPrefab";

            pivotPoint = GameObject.FindGameObjectWithTag("PivotPoint");
        }

        if (_instantiateObj != null && player != null)
            _instantiateObj.transform.SetParent(player.transform);
    }


    protected IEnumerator Scalingg(Vector3 _targetDirection)
    {
        while (true) // Continuously scale
        {
            Vector3 initialScale = new(_instantiateObj.transform.localScale.x, _scriptObj.height, _scriptObj.depth);
            Vector3 targetScale = initialScale + scaleFactor * Time.deltaTime * _targetDirection;
            _instantiateObj.transform.localScale = targetScale;
            isExpanding = true;

            // Check for connect points while scaling
            if (DetectionPoint.instance.PointDetected())
            {
                // Stop scaling
                FreezeScaling(_instantiateObj.transform.localScale);
            } 
            yield return null;
        }
    }

    // SCALE BACK TOWARDS THE ENDPOINT
    protected IEnumerator ExpandBackTowardsEndPoint(Vector3 _startPosition)
    {
        float elapsedTime = 0f;
        float duration = 2f;
        Vector3 targetScale = new(0, 1, 1);
        Point _point = startPointObj.GetComponent<Point>();

        while (elapsedTime < duration)
        {
            // Interpolate between start and target scale
            float t = elapsedTime / duration;
            pivotPoint.transform.localScale = Vector3.Lerp(_startPosition, targetScale, t);
            
            if(_point.Movingg(startPointObj.transform)) 
            {
                // Debug.Log("We are moving");
                player.playerRenderer.SetActive(false);
            }
            
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }            

        // Ensure the scale is exactly the target scale when done
        pivotPoint.transform.localScale = targetScale;

        if(pivotPoint.transform.localScale == targetScale) 
        {
            Debug.Log("Reached the endpoint");
            player.playerRenderer.SetActive(true);
            player.transform.position = detectObj.transform.position + teleportOffset;
        }

        if(DestroyGameObject(_instantiateObj)) 
        {
            DestroyGameObject(detectObj);
            DestroyGameObject(startPointObj);

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

    // SCALE BACK TO THE INITIAL POINT
    protected IEnumerator ScaleBack(Vector3 _startScale, Vector3 _targetScale)
    {
        // isExpandingBack = false;
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(_startScale, _targetScale);           

        while (_instantiateObj.transform.localScale != _targetScale)
        {
            float journeyTime = Time.time - startTime;
            float fracJourney = journeyTime * scaleFactor / journeyLength;
            _instantiateObj.transform.localScale = Vector3.Lerp(_startScale, _targetScale, Mathf.Clamp01(fracJourney));

            isExpandingBack = true;
            isExpanding = false;

            yield return null;
        }

        // Ensure the scale is exactly the target scale when done
        _instantiateObj.transform.localScale = _targetScale;
        isExpandingBack = false;

        // Reset stopScaling flag after scaling back
        stopScalingCuzEndPointReached = false;
    }

    private void FreezeScaling(Vector3 _currentScale) 
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        _instantiateObj.transform.localScale = _currentScale;
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
