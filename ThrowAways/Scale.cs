using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    private Transform startPoint; // Point A
    public Transform endPoint;   // Point B

    private Vector3 initialScale;
    private Quaternion initialRotation;
    private PlayerController player;

    [SerializeField] private bool isExpanding = false; 
    [SerializeField] private bool isExpandingBack = false;
    public float scaleFactor = 3f; // Adjust this factor to control the rate of expansion
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

        if (Input.GetKeyDown(KeyCode.G) && !player.isMoving)
        {
            // If scaling coroutine is running, stop it
            if (coroutine != null)
                StopCoroutine(coroutine);
            
            // Scale the object based on the distance between the points
            coroutine = StartCoroutine(Scaling(Vector2.right, true));
        }
        else if (Input.GetKeyUp(KeyCode.G)) 
        {
            // If scaling coroutine is running, stop it and start scaling back
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(ScaleBack(transform.localScale, initialScale, transform.rotation, initialRotation));
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
            Vector3 targetScale = transform.localScale + scaleFactor * Time.deltaTime * targetDirection;
            transform.localScale = targetScale;

            // Handle player movement
            if (expanding)
            {
                isExpanding = true;
                isExpandingBack = false;
                player.rb.velocity = Vector2.zero;
                player.isMoving = false;
            }
            else
            {
                isExpanding = false;
                isExpandingBack = true;
                player.rb.velocity = new Vector2(player.inputDirection.x, player.rb.velocity.y);
                player.isMoving = true;
            }

            yield return null;
        }
    }

    IEnumerator ScaleBack(Vector3 startScale, Vector3 targetScale, Quaternion startRotation, Quaternion targetRotation)
    {
        isExpandingBack = false;
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(startScale, targetScale);

        while (transform.localScale != targetScale)
        {
            float journeyTime = Time.time - startTime;
            float fracJourney = journeyTime * scaleFactor / journeyLength;
            transform.localScale = Vector3.Lerp(startScale, targetScale, Mathf.Clamp01(fracJourney));

            // bridgeIsCreated = false;

            isExpandingBack = true;

            // Handle player movement
            isExpanding = false;
            player.rb.velocity = Vector2.zero;
            player.isMoving = false;

            yield return null;
        }

        // Ensure the scale is exactly the target scale when done
        transform.localScale = targetScale;
        isExpandingBack = false;
        // bridgeIsCreated = false;

        // Now start rotating back to the initial rotation
        startTime = Time.time;
        journeyLength = Quaternion.Angle(startRotation, targetRotation);

        while (transform.rotation != targetRotation)
        {
            float journeyTime = Time.time - startTime;
            float fracJourney = journeyTime * scaleFactor / journeyLength;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, Mathf.Clamp01(fracJourney));

            player.rb.velocity = Vector2.zero;
            player.isMoving = false;

            yield return null;
        }

        // Ensure the rotation is exactly the initial rotation when done
        transform.rotation = targetRotation;
        if (transform.rotation == targetRotation) 
        {
            // Allow player to move again
            player.rb.velocity = new Vector2(player.inputDirection.x, player.rb.velocity.y);
            player.isMoving = true;
        }
    }
}










