using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingController : MonoBehaviour
{
    // Points
    private Transform startPoint; // Own transform
    public ConnectPoint endPoint;   // Point that's detected

    // Scale
    private Vector3 initialScale;
    private Quaternion initialRotation;
    [SerializeField] private bool isExpanding; 
    [SerializeField] private bool isExpandingBack;
    [SerializeField] private bool stopScaling;

    private PlayerController player;
    private Coroutine coroutine;

    void Awake() 
    {
        player = GetComponentInParent<PlayerController>();
    }

    void Start() 
    {
        // Scale
        initialScale = transform.localScale;

        // Rotation
        initialRotation = transform.rotation;
    }

    void Update() 
    {
        startPoint = transform;

        if (Input.GetKeyDown(KeyCode.Space) && !player.isMoving)
        {
            // If scaling coroutine is running, stop it
            if (coroutine != null)
                StopCoroutine(coroutine);
            
            stopScaling = false;
            coroutine = StartCoroutine(Scaling(Vector2.right, true));
        } 
        
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            // If scaling coroutine is running, stop it and start scaling back
            if (coroutine != null) 
                StopCoroutine(coroutine);

                // If endpoint is detected cant scale back
                if(!stopScaling) 
                {
                    coroutine = StartCoroutine(ScaleBack(transform.localScale, initialScale, transform.rotation, initialRotation));
                }
        
        }
        else if (player.isMoving) 
        {
            return;
        }
    }

    IEnumerator Scaling(Vector3 targetDirection, bool expanding)
    {
        while (true) // Continuously scale
        {
            Vector3 targetScale = transform.localScale + player.scaleFactor * Time.deltaTime * targetDirection;
            transform.localScale = targetScale;

            // Check for connect points while scaling
            if (DetectionPoint.instance.PointDetected())
            {
                StopScaling(transform.localScale);
                isExpanding = false;
                yield break;
            }

            // Handle player movement
            if (expanding)
            {
                isExpanding = true;
                isExpandingBack = false;
                player.rb.velocity = Vector2.zero;
                player.isMoving = false;
            
            } else if(!expanding)
            {
                isExpanding = false;
                isExpandingBack = true;
            
            } else 
            {
                StopScaling(transform.localScale);
            }

            yield return null;
        }
    }

    private void StopScaling(Vector3 currentScale) 
    {
        transform.localScale = currentScale;

        if(coroutine != null)
            StopCoroutine(coroutine);

        player.rb.velocity = Vector2.zero;
        player.isMoving = false;
        stopScaling = true;
    }

    IEnumerator ScaleBack(Vector3 startScale, Vector3 targetScale, Quaternion startRotation, Quaternion targetRotation)
    {
        isExpandingBack = false;
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(startScale, targetScale);

        while (transform.localScale != targetScale)
        {
            float journeyTime = Time.time - startTime;
            float fracJourney = journeyTime * player.scaleFactor / journeyLength;
            transform.localScale = Vector3.Lerp(startScale, targetScale, Mathf.Clamp01(fracJourney));

            isExpandingBack = true;
            isExpanding = false;
            player.rb.velocity = Vector2.zero;
            player.isMoving = false;

            yield return null;
        }

        // Ensure the scale is exactly the target scale when done
        transform.localScale = targetScale;
        isExpandingBack = false;

        // Now start rotating back to the initial rotation
        startTime = Time.time;
        journeyLength = Quaternion.Angle(startRotation, targetRotation);

        while (transform.rotation != targetRotation)
        {
            float journeyTime = Time.time - startTime;
            float fracJourney = journeyTime * player.scaleFactor / journeyLength;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, Mathf.Clamp01(fracJourney));

            yield return null;
        }

        // Ensure the rotation is exactly the initial rotation when done
        transform.rotation = targetRotation;
    
        // Allow player to move again
        // player.rb.velocity = new Vector2(player.inputDirection.x, player.rb.velocity.y);
        // player.isMoving = true;
    }
}
