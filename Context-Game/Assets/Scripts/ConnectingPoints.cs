using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingPoints : MonoBehaviour
{
    public enum ConnectPoint 
    {
        STARTING_POINT,
        END_POINT
    }

    public enum ConnectPointType 
    {
        BRIDGE_POINT,
        LADDER_POINT,
        GRAPPLER_POINT
    }

    public ConnectPoint connectPoint;
    public ConnectPointType connectPointType;
    // public LayerMask layerMask;

    private Transform player;
    [SerializeField] private float detectRadius;
    public float distance;
    public bool inRangeForStartPoint = false;

    public bool inRange = false;
    
    void Start() 
    {
        player = PlayerManager.instance.transform;

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

    void Update()
    {          
        distance = Vector2.Distance(player.position, transform.position);
        if(distance < detectRadius) 
            inRange = true;

        else inRange = false;        
    }

    public void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

    
    // public void CheckForEndPoint()
    // {
    //     inRangeForEndPoint = false;
    //     Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);

    //     BridgeManager bridgeManager = Object.FindFirstObjectByType<BridgeManager>();
    //     bridgeManager.pointsThatCanBeConnected.Clear();

    //     foreach (var point in hitColliders)
    //     {
    //         ConnectingPoints connectPoint = point.GetComponent<ConnectingPoints>();
    //         if (connectPoint.connectPoint == ConnectingPoints.ConnectPoint.END_POINT)
    //         {
    //             inRangeForEndPoint = true;
    //             endPoint = connectPoint.gameObject.transform.position;
    //         }

    //         if (connectPoint.connectPoint == ConnectingPoints.ConnectPoint.STARTING_POINT)
    //         {
    //             startPoint = connectPoint.gameObject.transform.position;
    //         }

    //         bridgeManager.pointsThatCanBeConnected.Add(point.transform);
    //     }
    // }



    // private void CheckForConnectPoint() 
    // {
    //     endPointDetected = false;
    //     // inRangeForConnectPoint = false;
    //     Collider[] connectPoints = Physics.OverlapSphere(transform.position, detectionRadius);
        
    //     foreach (var _point in connectPoints)
    //     {
    //         ConnectingPoints point = _point.GetComponent<ConnectingPoints>();

    //         // Check if the player is in range
    //         if(point.inRange) 
    //         {
    //             // Check connection type and player type
    //             if (point.connectPointType == ConnectingPoints.ConnectPointType.BRIDGE_POINT &&
    //                 player.playerType == PlayerManager.PlayerType.ARTIST) 
    //             {
    //                 // Artist can do stuff
    //                 Vector2 rayCast = new Vector2(transform.position.x, transform.position.y - offsetY);
    //                 // Vector2 rayCast = new(point.transform.position.x, point.transform.position.y);
                    
    //                 RaycastHit2D hit = Physics2D.Raycast(rayCast, Vector2.right, 100f, layerMask);
    //                 if (hit.collider != null)
    //                 {
    //                     // Draw a line from the start position to the hit point
    //                     Debug.DrawLine(rayCast, hit.point, Color.green);
    //                     endPointDetected = true;
                        
                        
    //                     // bridgeManager.pointsThatCanBeConnected.Add(point.transform);
    //                     // if (Input.GetKeyDown(KeyCode.P))
    //                     // {
    //                     //     CreateBridge();
    //                     // }

    //                 } 
    //                 else 
    //                 {
    //                     // Draw a line indicating the direction of the ray if nothing was hit
    //                     Debug.DrawRay(rayCast, Vector2.right * 100f, Color.red);
    //                     Debug.Log("Not Hit");
    //                     endPointDetected = false;
    //                 }
    //             }


    //             // Check what type of connecting point
    //             if(point.connectPointType == ConnectingPoints.ConnectPointType.GRAPPLER_POINT &&
    //             player.playerType == PlayerManager.PlayerType.DESIGNER) 
    //             {
    //                 // Designer can do stuff
    //                 Debug.Log("This is an Designer player");

    //             }

    //             // Check what type of connecting point
    //             if(point.connectPointType == ConnectingPoints.ConnectPointType.LADDER_POINT &&
    //             player.playerType == PlayerManager.PlayerType.DEVELOPER) 
    //             {
    //                 // Developer can do stuff
    //                 Debug.Log("This is an Developer player");

    //             }
    //         }
    //     }
    // }
}








// Check if there is already a bridge between the points

// Therefore I have to place the points in a list "points that can be connected" and delete them from the list after the bridge is placed

// So when points are detected then place the points in the list
// THEN check if there is an existing bridge between the two points
// IF so then the player cant create another bridge

// The way to check if there is a bridge between the points, is to create a list for the bridges that are created
// THEN check

// IF not then the player can create a bridge
// After the bridge is created, put the bridge in a list "Created bridge"
// 