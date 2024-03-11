using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.Mathematics;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public BridgeSO bridgeParameters;

    [SerializeField] private float detectionRadius = 3f;
    public bool inRangeForEndPoint = false;
    private float distanceBetweenPoints;
    private Vector2 startPoint, endPoint;

    void Start() 
    {
        // Create a new GameObject for visualization
        // GameObject sphereVisualizer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        
        // // Remove the collider component from the visualization sphere
        // Destroy(sphereVisualizer.GetComponent<Collider>());

        // // Set the position of the visualization sphere to match the transform position
        // sphereVisualizer.transform.position = transform.position;

        // // Scale the visualization sphere based on the detection radius
        // sphereVisualizer.transform.localScale = new Vector3(detectionRadius * 2f, detectionRadius * 2f, detectionRadius * 2f);
        
        // // Set the visualization sphere as a child of the current GameObject
        // sphereVisualizer.transform.parent = transform;
    }

    public void Update() 
    {
        CheckForEndPoint();
        if(Input.GetKeyDown(KeyCode.P) && inRangeForEndPoint) 
        {
            CreateBridge();
        }    
    }

    public void CheckForEndPoint() 
    {
        // canCreateBridge = false;
        inRangeForEndPoint = false;
        // foundStartPoint = false;
        // foundEndPoint = false;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hit in hitColliders)
        {
            PointDetection hitPoint = hit.GetComponent<PointDetection>();
            if(hitPoint.point == PointDetection.ConnectingPoints.END) 
            {
                inRangeForEndPoint = true;
                endPoint = hitPoint.gameObject.transform.position;
            } 

            if(hitPoint.point == PointDetection.ConnectingPoints.START) 
            {
                startPoint = hitPoint.gameObject.transform.position;
            }

            distanceBetweenPoints = Vector2.Distance(startPoint, endPoint);
        }
    }

    private void CreateBridge() 
    {
        // Check if a bridge already exists in the scene
        GameObject existingBridge = GameObject.Find("Bridge"); // Replace "Bridge" with the name of your bridge game object

        if(existingBridge == null) 
        {
            // Place the bridge between the two connecting points
            Vector3 bridgeScale = new Vector3(distanceBetweenPoints, bridgeParameters.height, bridgeParameters.depth);
            GameObject newBridge = Instantiate(bridgeParameters.bridgePrefab, (startPoint + endPoint) / 2f, Quaternion.identity) as GameObject;

            // Rename the new bridge to "Bridge" (or any desired name)
            newBridge.name = "Bridge";

            // Assign a layer to the new bridge
            newBridge.layer = LayerMask.NameToLayer("Platform");

            // Create bridge with the same width as the distance between the two points
            newBridge.transform.localScale = bridgeScale; 
        }
        else 
        {
            Debug.Log("Bridge already exists in the scene.");
        }
    }

    public void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }   
}
