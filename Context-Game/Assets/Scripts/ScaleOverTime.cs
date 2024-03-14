using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    // move the cube .5f on start

    // Get the end point

    // Scale the parent object of the cube to the direction of the endpoint
    // public Transform startPoint; // Point A
    public Transform endPoint;   // Point B
    public float duration = 2.0f;
    public float delay = 1.0f;

    void Start() 
    {
        GameObject cube = transform.GetChild(0).gameObject;
        cube.transform.position = new Vector2(cube.transform.position.x + .5f, cube.transform.position.y);
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.F)) 
        {
            transform.localScale = endPoint.position;   
        }
    }

    // private Vector3 initialScale;
    // private float distance;

    // void Start()
    // {
    //     initialScale = transform.localScale;
    //     distance = Vector3.Distance(startPoint.position, endPoint.position);
    //     StartCoroutine(ScaleOverTimee());
    // }

    // IEnumerator ScaleOverTimee()
    // {
    //     yield return new WaitForSeconds(delay);

    //     float startTime = Time.time;

    //     while (Time.time - startTime < duration)
    //     {
    //         float t = (Time.time - startTime) / duration;
    //         float currentDistance = distance * t;
    //         float scaleRatio = currentDistance / distance;
    //         transform.localScale = initialScale * scaleRatio;
    //         yield return null;
    //     }

    //     // Ensure final scale is exactly what we want
    //     transform.localScale = initialScale;
    // }
    


}
