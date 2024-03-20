using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] protected ScriptObject _scriptObj;
    protected PlayerController player;
    protected Coroutine coroutine;
    protected PlayerManager playerManag;


    // Object Points
    protected GameObject _instantiateObj;
    protected GameObject startPointObj, detectObj;

    // Transform
    protected Transform extendPoint1, extendPoint2, toExtandBack;
    [SerializeField] protected Transform instantiatePoint, startPoint, endPoint;


    [SerializeField] public bool isExpanding; 
    [SerializeField] public bool isExpandingBack;
    [SerializeField] public bool stopScalingCuzEndPointReached;
    protected bool objectCreated;

    protected float timer;
    [SerializeField] protected float timeUntilScaleBack = 4f;
    [SerializeField] private float scaleFactor = 10f;
    [SerializeField] private Vector3 teleportOffset;


    void Awake() 
    {
        if(!TryGetComponent<PlayerManager>(out playerManag)) 
        {
            Debug.LogError("PlayerManager component not found!");
        }
    }

    void Start() 
    {
        player = GetComponentInParent<PlayerController>();
    }

    protected void CreateObject() 
    {
        objectCreated = false;

        if (_instantiateObj == null)
        {   
            // Game Object
            _instantiateObj = Instantiate(_scriptObj.@object, instantiatePoint.transform.position, _scriptObj.finalRotation);
            _instantiateObj.name = "Expandable object";

            // Find game objects on the @object
            extendPoint1 = GameObject.FindGameObjectWithTag("ExtendPoint1").transform;
            extendPoint2 = GameObject.FindGameObjectWithTag("ExtendPoint2").transform;
            toExtandBack = GameObject.FindGameObjectWithTag("ToExtandBack").transform; 


            // Set the extendPoint1 scale to the scale of the @object
            extendPoint1.localScale = _scriptObj.initialScale;
            
            // The start and end point of the @object
            startPoint = GameObject.FindGameObjectWithTag("StartPoint").transform;
            endPoint = GameObject.FindGameObjectWithTag("EndPoint").transform;

            // Start Pivot Point
            startPointObj = Instantiate(_scriptObj.startObj, extendPoint1.position, Quaternion.identity);
            startPointObj.name = "NewStartPointPrefab";

            // Detect point Game Object (End Pivot Point) 
            detectObj = Instantiate(_scriptObj.detectObj, extendPoint2.position, Quaternion.identity) as GameObject;
            detectObj.name = "Detect Point";

            objectCreated = true;

        }

        if (_instantiateObj != null && player != null)
            _instantiateObj.transform.SetParent(player.transform);
    }


    protected IEnumerator Scalingg(GameObject _scaleObj, Vector3 _targetDirection)
    {
        while (true) // Continuously scale
        {
            Vector3 initialScale = new(_scaleObj.transform.localScale.x, _scaleObj.transform.localScale.y, _scaleObj.transform.localScale.z);
            Vector3 targetScale = initialScale + scaleFactor * Time.deltaTime * _targetDirection;
            _scaleObj.transform.localScale = targetScale;
            isExpanding = true;

            // Check for connect points while scaling
            if (DetectionPoint.instance.PointDetected())
            {
                // Stop scaling
                FreezeScaling(_scaleObj, _scaleObj.transform.localScale);
            } 
            yield return null;
        }
    }

    // SCALE BACK TO THE INITIAL POINT
    protected IEnumerator ScaleBack(GameObject _scaleObj, Vector3 _startScale, Vector3 _targetScale)
    {
        // isExpandingBack = false;
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(_startScale, _targetScale);           

        while (_scaleObj.transform.localScale != _targetScale)
        {
            float journeyTime = Time.time - startTime;
            float fracJourney = journeyTime * scaleFactor / journeyLength;
            _scaleObj.transform.localScale = Vector3.Lerp(_startScale, _targetScale, Mathf.Clamp01(fracJourney));

            isExpandingBack = true;
            isExpanding = false;

            yield return null;
        }

        // Ensure the scale is exactly the target scale when done
        _scaleObj.transform.localScale = _targetScale;
        isExpandingBack = false;

        // Reset stopScaling flag after scaling back
        stopScalingCuzEndPointReached = false;
}

    // SCALE BACK TOWARDS THE ENDPOINT
    protected IEnumerator ExpandBackTowardsEndPoint(GameObject _scaleObj, Vector3 _startPosition)
    {
        float elapsedTime = 0f;
        float duration = 2f;
        Vector3 targetScale = new(0, 1, 1);
        Point _point = startPointObj.GetComponent<Point>();

        while (elapsedTime < duration)
        {
            // Interpolate between start and target scale
            float t = elapsedTime / duration;
            _scaleObj.transform.localScale = Vector3.Lerp(_startPosition, targetScale, t);
            
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
        _scaleObj.transform.localScale = targetScale;

        if(_scaleObj.transform.localScale == targetScale) 
        {
            Debug.Log("Reached the endpoint");
            player.playerRenderer.SetActive(true);
            player.transform.position = detectObj.transform.position + teleportOffset;
        }

        if(DestroyGameObject(_instantiateObj)) 
        {
            DestroyGameObject(detectObj);
            DestroyGameObject(startPointObj);
            
            objectCreated = false;
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

    private void FreezeScaling(GameObject _scaleObj, Vector3 _currentScale) 
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        _scaleObj.transform.localScale = _currentScale;
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
