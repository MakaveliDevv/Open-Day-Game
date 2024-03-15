using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    public Transform startPoint; // Point A
    public Transform endPoint;   // Point B
    private GameObject cube;
    private GameObject endPointCube;
    private Vector3 initialScale;
    private Quaternion initialRotation;

    public float speed = 3f;
    public float resetDuration = 3f;

    private Coroutine coroutine; // To keep track of scaling coroutine


    void Start() 
    {
        cube = transform.GetChild(0).gameObject;

        // Reference to the child object of the cube
        endPointCube = cube.transform.GetChild(0).gameObject;

        // Position
        transform.position = startPoint.position;

        // Scale
        initialScale = transform.localScale;

        // Rotation
        initialRotation= transform.rotation;
    }

    void Update() 
    {
        Vector2 startPointPos = startPoint.transform.position;
        Vector2 endPointPos = endPoint.transform.position;

        // Get distance between the two points
        float distanceBetweenPoints = Vector2.Distance(startPointPos, endPointPos);

        // Calculate the direction towards the end point
        Vector2 direction = (endPointPos - startPointPos).normalized;

        // Calculate the angle towards the end point
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (Input.GetKeyDown(KeyCode.G))
        {
            // If scaling coroutine is running, stop it
            if (coroutine != null)
                StopCoroutine(coroutine);
            
            // Apply rotation
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

            // Calculate the target scale based on the distance between the points
            Vector3 targetScale = new(distanceBetweenPoints, transform.localScale.y, transform.localScale.z);

            // Scale the object based on the distance between the points
            coroutine = StartCoroutine(ScaleOverTimee(transform.localScale, targetScale, targetRotation));
        
        } else if (Input.GetKeyUp(KeyCode.G)) 
        {
            // If scaling coroutine is running, stop it and start scaling back
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(ResetOverTimee(initialScale, initialRotation));
        }
    }

    IEnumerator ResetOverTimee(Vector3 _initialScale, Quaternion _initialRotation)
    {
        float elapsedTime = 0f;
        Vector3 currentScale = transform.localScale;
        
        while (elapsedTime < resetDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / resetDuration);

            // Interpolate towards the target scale
            transform.localScale = Vector3.Lerp(currentScale, _initialScale, t);

            yield return null;
        }

        // Ensure the scale is exactly the target scale when done
        transform.localScale = _initialScale;

        // Now start rotating back to the initial rotation
        elapsedTime = 0f; // Reset elapsedTime for rotation
        Quaternion currentRotation = transform.rotation;

        while (elapsedTime < resetDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / resetDuration);

            // Interpolate towards the initial rotation
            transform.rotation = Quaternion.Slerp(currentRotation, _initialRotation, t);

            yield return null;
        }

        // Ensure the rotation is exactly the initial rotation when done
        transform.rotation = _initialRotation;

        coroutine = null; // Reset scaling coroutine reference
    }

    IEnumerator ScaleOverTimee(Vector3 _startScale, Vector3 _targetScale, Quaternion _targetRotation)
    {
        float elapsedTime = 0f;
        Quaternion currentRotation = transform.rotation;
        
        while (elapsedTime < resetDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / resetDuration);

            // Interpolate towards the target scale
            transform.localScale = Vector3.Lerp(_startScale, _targetScale, t);

            // Interpolate towards the target rotation
            transform.rotation = Quaternion.Slerp(currentRotation, _targetRotation, t);

            yield return null;
        }

        // Ensure the scale is exactly the target scale when done
        transform.localScale = _targetScale;

        coroutine = null; // Reset scaling coroutine reference
    }

}
