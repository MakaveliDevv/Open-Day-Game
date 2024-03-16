// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;

// public class Point : MonoBehaviour
// {
//     public GameObject point;
//     public BridgeManager bridgeManager;
//     [SerializeField] private float detectRadius;

//     void Start() 
//     {
//         bridgeManager = FindFirstObjectByType<BridgeManager>();
//         transform.position = point.transform.position;

//         // Create a new GameObject for visualization
//         GameObject sphereVisualizer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        
//         // Remove the collider component from the visualization sphere
//         Destroy(sphereVisualizer.GetComponent<Collider>());

//         // Set the position of the visualization sphere to match the transform position
//         sphereVisualizer.transform.position = transform.position;

//         // Scale the visualization sphere based on the detection radius
//         sphereVisualizer.transform.localScale = new Vector3(detectRadius * 2f, detectRadius * 2f, detectRadius * 2f);
        
//         // Set the visualization sphere as a child of the current GameObject
//         sphereVisualizer.transform.parent = transform;
        
//     }

//   void Update() 
//     {
//         transform.position = point.transform.position;
//         PointDetected();
//     }

//     public bool EndPointDetected() 
//     {
//         bridgeManager.endPointsDetected.Clear();
//         Collider2D hitPoint = Physics2D.OverlapCircle(transform.position, detectRadius);
//         ConnectingPoints connectPoint = hitPoint.GetComponent<ConnectingPoints>();

//         if(connectPoint != null && connectPoint.connectPointType == ConnectingPoints.ConnectPointType.BRIDGE_POINT 
//         && connectPoint.inRange && connectPoint.connectPoint == ConnectingPoints.ConnectPoint.END_POINT)
//         {
//             bridgeManager.endPointsDetected.Add(connectPoint.transform);
//             Debug.Log("Found an end point");
//             return true;
            
//         } else 
//         {
//             return false;
//         }
//     }

//     public bool PointDetected() 
//     {
//         bridgeManager.endPointsDetected.Clear();
//         Collider2D hitPoint = Physics2D.OverlapCircle(transform.position, detectRadius);

//         if (hitPoint != null)
//         {
//             ConnectingPoints connectPoint = hitPoint.GetComponent<ConnectingPoints>();

//             if (connectPoint != null && connectPoint.connectPointType == ConnectingPoints.ConnectPointType.BRIDGE_POINT
//             && connectPoint.connectPoint == ConnectingPoints.ConnectPoint.END_POINT)
//             {
//                 bridgeManager.endPointsDetected.Add(connectPoint.transform);
//                 Debug.Log("Found an end point");
//                 return true;
//             }
//             else 
//             {
//                 Debug.Log("Not found");
//             }
//             return false;
//         }

//         return true;
//     }

//     public void OnDrawGizmosSelected() 
//     {
//         Gizmos.color = Color.red;
//         Gizmos.DrawWireSphere(transform.position, detectRadius);
//     }
// }
