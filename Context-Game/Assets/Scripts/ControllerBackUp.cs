using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBackUp : MonoBehaviour
{
    public BridgeSO bridgeSO;
    protected PlayerController player;
    protected Coroutine coroutine;

    // References
    // protected GameObject newBridge; 

    // Points
    public GameObject detectPoint;
    protected GameObject newDetectPoint;

    public GameObject startingPoint;
    protected GameObject newStartingPoint;
    
    // public Transform instantiatePoint; 
    protected Transform pivotStartPoint;
    protected Transform pivotEndPoint;
    protected GameObject pivotPoint;

    // Scale
    protected Vector3 initialScale;
    protected Quaternion initialRotation;
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

        initialScale = bridgeSO.initialScale;
        initialRotation = bridgeSO.finalRotation;
    }


    protected IEnumerator Scalingg(BridgeSO _scriptObj, GameObject _object, Vector3 _targetDirection)
    {
        while (true) // Continuously scale
        {
            Vector3 initialScale = new(_object.transform.localScale.x, _scriptObj.height, _scriptObj.depth);
            Vector3 targetScale = initialScale + scaleFactor * Time.deltaTime * _targetDirection;
            _object.transform.localScale = targetScale;
            isExpanding = true;

            // Check for connect points while scaling
            if (DetectionPoint.instance.PointDetected())
            {
                // Stop scaling
                // FreezeScaling(_object.transform.localScale);
            } 
            yield return null;
        }
    }

    // EXTEND TOWARDS THE GIVEN POINT
    // protected IEnumerator Scaling(Vector3 _targetDirection)
    // {
    //     while (true) // Continuously scale
    //     {
    //         Vector3 initialScale = new(newBridge.transform.localScale.x, bridgeSO.height, bridgeSO.depth);
    //         Vector3 targetScale = initialScale + scaleFactor * Time.deltaTime * _targetDirection;
    //         newBridge.transform.localScale = targetScale;
    //         isExpanding = true;

    //         // Check for connect points while scaling
    //         if (DetectionPoint.instance.PointDetected())
    //         {
    //             // Stop scaling
    //             FreezeScaling(newBridge.transform.localScale);
    //         } 
    //         yield return null;
    //     }
    // }

    // // SCALE BACK TOWARDS THE ENDPOINT
    // protected IEnumerator ExpandBackTowardsEndPoint(BridgeSO _scriptObj, Vector3 _startPosition)
    // {
    //     float elapsedTime = 0f;
    //     float duration = 2f;
    //     Vector3 targetScale = new(0, 1, 1);
    //     Point _point = newStartingPoint.GetComponent<Point>();

    //     while (elapsedTime < duration)
    //     {
    //         // Interpolate between start and target scale
    //         float t = elapsedTime / duration;
    //         pivotPoint.transform.localScale = Vector3.Lerp(_startPosition, targetScale, t);
            
    //         if(_point.Movingg(newStartingPoint.transform)) 
    //         {
    //             Debug.Log("We are moving");
    //             player.playerRenderer.SetActive(false);
    //         }
            
    //         // Increment elapsed time
    //         elapsedTime += Time.deltaTime;

    //         yield return null;
    //     }            


    //     // Ensure the scale is exactly the target scale when done
    //     pivotPoint.transform.localScale = targetScale;

    //     // If the pivoitPoint is at the target position (detection position)
    //     if(pivotPoint.transform.position == newDetectPoint.transform.position) 
    //     {
    //         // Set player active at that position
    //         Debug.Log("Reached the endpoint");
    //         player.playerRenderer.SetActive(true);
    //         player.transform.position = pivotEndPoint.transform.position + teleportOffset;
    //     }

    //     if(DestroyGameObject(newBridge)) 
    //     {
    //         DestroyGameObject(newDetectPoint);
    //         DestroyGameObject(newStartingPoint);

    //         stopScalingCuzEndPointReached = false;
    //         _point.isMoving = false;
            
    //         player.rb.velocity = player.inputDirection;
    //     }
    // }


    // public bool DestroyGameObject(GameObject _object) 
    // {
    //     Destroy(_object);

    //     return true;
    // }

    // // SCALE BACK TO THE INITIAL POINT
    // protected IEnumerator ScaleBack(Vector3 _startScale, Vector3 _targetScale)
    // {
    //     // isExpandingBack = false;
    //     float startTime = Time.time;
    //     float journeyLength = Vector3.Distance(_startScale, _targetScale);           

    //     while (newBridge.transform.localScale != _targetScale)
    //     {
    //         float journeyTime = Time.time - startTime;
    //         float fracJourney = journeyTime * scaleFactor / journeyLength;
    //         newBridge.transform.localScale = Vector3.Lerp(_startScale, _targetScale, Mathf.Clamp01(fracJourney));

    //         isExpandingBack = true;
    //         isExpanding = false;

    //         yield return null;
    //     }

    //     // Ensure the scale is exactly the target scale when done
    //     newBridge.transform.localScale = _targetScale;
    //     isExpandingBack = false;

    //     // Reset stopScaling flag after scaling back
    //     stopScalingCuzEndPointReached = false;
    // }

    // private void FreezeScaling(Vector3 _currentScale) 
    // {
    //     if(coroutine != null)
    //         StopCoroutine(coroutine);

    //     newBridge.transform.localScale = _currentScale;
    //     stopScalingCuzEndPointReached = true;

    //     // Reset expansion state flags
    //     isExpanding = false;
    //     isExpandingBack = false;
    // }

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
