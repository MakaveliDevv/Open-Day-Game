using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    private Transform startPoint; // Point A
    public Transform endPoint;   // Point B

    private Vector3 initialScale;
    private Quaternion initialRotation;
    public Vector3 currentEndPointPos;
    private Coroutine coroutine; // To keep track of scaling coroutine
    private PlayerMovement player;

    public float speed = 3f;
    public float resetDuration = 3f;
    public bool bridgeIsCreated = false;
    [SerializeField] private bool isExpanding = false; 
    [SerializeField] private bool isExpandingBack = false;

    void Awake() 
    {
        player = GetComponentInParent<PlayerMovement>();
    }

    void Start() 
    {
        // Scale
        initialScale = transform.localScale;

        // Rotation
        initialRotation= transform.rotation;
    }

    void Update() 
    {
        startPoint = transform;
        Vector2 startPointPos = new(startPoint.position.x, startPoint.position.y);

        // Before getting end point, expand
        // Input get key down, expand towards direction player is facing
        // Check if we are moving
        if (Input.GetKeyDown(KeyCode.G) && !player.isMoving)
        {
            // If scaling coroutine is running, stop it
            if (coroutine != null)
                StopCoroutine(coroutine);
            
            // Scale the object based on the distance between the points
            coroutine = StartCoroutine(ScaleOverTimee(transform.localScale, Vector2.right));
        
        } else if (Input.GetKeyUp(KeyCode.G)) 
        {
            // If scaling coroutine is running, stop it and start scaling back
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(ResetOverTimee(transform.localScale, initialScale, transform.rotation, initialRotation));
        }
        else if(player.isMoving) 
        {
            return;
        }
        

        // Then check if an end point is detected

        // Then calculate the distance

        // Get direction

        // Angle
        Vector2 endPointPos = endPoint.transform.position;

        // Get distance between the two points
        float distanceBetweenPoints = Vector2.Distance(startPointPos, endPointPos);

        // Calculate the direction towards the end point
        Vector2 direction = (endPointPos - startPointPos).normalized;

        // Calculate the angle towards the end point
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    
    }
    public float elapsedTime = 0f;
    public float scaleFactor = 3f; // Adjust this factor to control the rate of expansion

    IEnumerator ScaleOverTimee(Vector3 _Vstart, Vector3 _Vtarget)
    {
        float newElapsedTime = elapsedTime;
        float newScaleFactor = scaleFactor;
        Vector3 targetScale = _Vstart + _Vtarget * newScaleFactor;

        while (newElapsedTime < resetDuration)
        {
            newElapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(newElapsedTime / resetDuration);

            // Interpolate towards the target scale
            transform.localScale = Vector3.Lerp(_Vstart, targetScale, t);
            isExpanding = true;

            if(isExpanding) 
            {
                isExpandingBack = false;

                // Specify that the player cant move
                Debug.Log("Player cant move");
                player.rb.velocity = new(0f, 0f);
                player.isMoving = false;
            }
            yield return null;
        }

        // Ensure the scale is exactly the target scale when done
        transform.localScale = _Vtarget;
        if(transform.localScale == _Vtarget) 
        {   
            bridgeIsCreated = true;
            isExpanding = false;
        }

        coroutine = null; // Reset scaling coroutine reference
    }

    IEnumerator ResetOverTimee(Vector3 _Vcurrent, Vector3 _Vinitial, Quaternion _Qcurrent, Quaternion _Qinitial)
    {
        isExpandingBack = false;
        float elapsedTime = 0f;        
        while (elapsedTime < resetDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / resetDuration);

            // Interpolate towards the target scale
            transform.localScale = Vector3.Lerp(_Vcurrent, _Vinitial, t);

            bridgeIsCreated = false;

            isExpandingBack = true;

            if(isExpandingBack) 
            {
                isExpanding = false;
                // Specify that the player cant move
                Debug.Log("Player cant move");
                player.rb.velocity = new(0f, 0f);
                player.isMoving = false;
            }

            // CalculateCubeEndPosition();

            yield return null;
        }

        // Ensure the scale is exactly the target scale when done
        transform.localScale = _Vinitial;
        if(transform.localScale == _Vinitial) 
        {
            bridgeIsCreated = false;
            isExpandingBack = false;
        }

        // Now start rotating back to the initial rotation
        elapsedTime = 0f; // Reset elapsedTime for rotation
        while (elapsedTime < resetDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / resetDuration);

            // Interpolate towards the initial rotation
            transform.rotation = Quaternion.Slerp(_Qcurrent, _Qinitial, t);
            player.rb.velocity = new(0f, 0f);
            player.isMoving = false;

            // CalculateCubeEndPosition();

            yield return null;
        }

        // Ensure the rotation is exactly the initial rotation when done
        transform.rotation = _Qinitial;
        if(transform.rotation == _Qinitial) 
        {
            // Specify that the player cant move
            Debug.Log("Player can move again");
            player.rb.velocity = new(player.inputDirection.x, player.rb.velocity.y);
            player.isMoving = true;
        }

        coroutine = null; // Reset scaling coroutine reference
    }

}










