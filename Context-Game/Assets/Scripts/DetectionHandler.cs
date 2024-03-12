using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class DetectionHandler : MonoBehaviour
{
    public BridgeSO bridgeParameters;
    public float detectionRadius = 3f;
    public bool inRangeForEndPoint = false;
    private Vector2 startPoint, endPoint;

    // public bool inRangeForConnectPoint = false;
    private PlayerManager player;
    private BridgeManager bridgeManager;
    public LayerMask layerMask;

    void Start() 
    {
        player = GetComponent<PlayerManager>();
        bridgeManager = FindObjectOfType<BridgeManager>();
    }

    public void Update()
    {
        // CheckForEndPoint();
        // if (Input.GetKeyDown(KeyCode.P) && inRangeForEndPoint)
        // {
        //     CreateBridge();
        // }
        CheckForConnectPoint();
    }

    private void CheckForConnectPoint() 
    {
        // inRangeForConnectPoint = false;
        Collider[] connectPoints = Physics.OverlapSphere(transform.position, detectionRadius);
        
        foreach (var _point in connectPoints)
        {
            ConnectingPoints point = _point.GetComponent<ConnectingPoints>();

            // Check if the player is in range
            if(point.inRange) 
            {
                // Check connection type and player type
                if (point.connectPointType == ConnectingPoints.ConnectPointType.BRIDGE_POINT &&
                    player.playerType == PlayerManager.PlayerType.ARTIST) 
                {
                    // Artist can do stuff
                    // inRangeForEndPoint = false;
                    Vector2 rayCastPoint = new Vector2(transform.position.x, transform.position.y - .5f);
                    
                    RaycastHit2D hit = Physics2D.Raycast(rayCastPoint, Vector2.right, 100f, layerMask);
                    if (hit.collider != null)
                    {
                        // Draw a line from the start position to the hit point
                        Debug.DrawLine(rayCastPoint, hit.point, Color.green);
                        Debug.Log("Bridge Hit");
                    } 
                    else 
                    {
                        // Draw a line indicating the direction of the ray if nothing was hit
                        Debug.DrawRay(rayCastPoint, Vector2.right * 100f, Color.red);
                        Debug.Log("Not Hit");
                    }

                }


                // Check what type of connecting point
                if(point.connectPointType == ConnectingPoints.ConnectPointType.GRAPPLER_POINT &&
                player.playerType == PlayerManager.PlayerType.DESIGNER) 
                {
                    // Designer can do stuff
                    Debug.Log("This is an Designer player");

                }

                // Check what type of connecting point
                if(point.connectPointType == ConnectingPoints.ConnectPointType.LADDER_POINT &&
                player.playerType == PlayerManager.PlayerType.DEVELOPER) 
                {
                    // Developer can do stuff
                    Debug.Log("This is an Developer player");

                }
            }
        }
    }

    // public void CheckForEndPoint()
    // {
    //     inRangeForEndPoint = false;
    //     Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);

    //     BridgeManager bridgeManager = FindObjectOfType<BridgeManager>();
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

    private void CreateBridge()
    {
        // Check if a bridge already exists between the points
        // BridgeManager bridgeManager = FindObjectOfType<BridgeManager>();
        if (bridgeManager == null)
        {
            Debug.LogError("No BridgeManager found in the scene.");
            return;
        }

        Transform startTransform = null;
        Transform endTransform = null;

        foreach (Transform point in bridgeManager.pointsThatCanBeConnected)
        {
            ConnectingPoints connectPoint = point.GetComponent<ConnectingPoints>();
            if (connectPoint.connectPoint == ConnectingPoints.ConnectPoint.STARTING_POINT)
            {
                startTransform = point;
            }
            else if (connectPoint.connectPoint == ConnectingPoints.ConnectPoint.END_POINT)
            {
                endTransform = point;
            }
        }

        if (startTransform == null || endTransform == null)
        {
            Debug.LogWarning("Could not find both starting and ending points.");
            return;
        }

        // Check if there's already a bridge between these points
        if (!bridgeManager.IsBridgePresent(startTransform, endTransform))
        {
            Vector3 bridgeScale = new Vector3(Vector2.Distance(startPoint, endPoint), bridgeParameters.height, bridgeParameters.depth);
            GameObject newBridge = Instantiate(bridgeParameters.bridgePrefab, (startPoint + endPoint) / 2f, Quaternion.identity) as GameObject;

            // Get animation

            // Play animation

            // If new bridge endpoint is equal to the end connect point, stop animation
            
            // Rename the new bridge to "Bridge" 
            newBridge.name = "Bridge";

            // Assign a layer to the new bridge
            newBridge.layer = LayerMask.NameToLayer("Platform");

            // Create bridge with the same width as the distance between the two points
            newBridge.transform.localScale = bridgeScale;

            // Remove points from the list as they are now connected
            bridgeManager.pointsThatCanBeConnected.Remove(startTransform);
            bridgeManager.pointsThatCanBeConnected.Remove(endTransform);

            // Create bridge record
            bridgeManager.createdBridges.Add(new Bridge(startTransform, endTransform));
        }
        else
        {
            Debug.Log("Bridge already exists between these points!");
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}










// public class DetectionHandler : MonoBehaviour
// {
//     public BridgeSO bridgeParameters;

//     [SerializeField] private float detectionRadius = 3f;
//     public bool inRangeForEndPoint = false;
//     private float distanceBetweenPoints;
//     private Vector2 startPoint, endPoint;

//     void Start() 
//     {
//         // Create a new GameObject for visualization
//         // GameObject sphereVisualizer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        
//         // // Remove the collider component from the visualization sphere
//         // Destroy(sphereVisualizer.GetComponent<Collider>());

//         // // Set the position of the visualization sphere to match the transform position
//         // sphereVisualizer.transform.position = transform.position;

//         // // Scale the visualization sphere based on the detection radius
//         // sphereVisualizer.transform.localScale = new Vector3(detectionRadius * 2f, detectionRadius * 2f, detectionRadius * 2f);
        
//         // // Set the visualization sphere as a child of the current GameObject
//         // sphereVisualizer.transform.parent = transform;
//     }

//     public void Update() 
//     {
//         CheckForEndPoint();
//         if(Input.GetKeyDown(KeyCode.P) && inRangeForEndPoint) 
//         {
//             CreateBridge();
//         }    
//     }

//     public void CheckForEndPoint() 
//     {
//         inRangeForEndPoint = false;

//         Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
//         foreach (var point in hitColliders)
//         {
//             ConnectingPoints connectPoint = point.GetComponent<ConnectingPoints>();
//             if(connectPoint.pointType == ConnectingPoints.ConnectPoint.END_POINT) 
//             {
//                 inRangeForEndPoint = true;
//                 endPoint = connectPoint.gameObject.transform.position;
//             } 

//             if(connectPoint.pointType == ConnectingPoints.ConnectPoint.STARTING_POINT) 
//             {
//                 startPoint = connectPoint.gameObject.transform.position;
//             }

//             distanceBetweenPoints = Vector2.Distance(startPoint, endPoint);
//         }
//     }

//     private void CreateBridge() 
//     {
//         // Check if a bridge already exists in the scene
//         GameObject existingBridge = GameObject.Find("Bridge"); // Replace "Bridge" with the name of your bridge game object

//         if(existingBridge == null) 
//         {
//             // Place the bridge between the two connecting points
//             Vector3 bridgeScale = new(distanceBetweenPoints, bridgeParameters.height, bridgeParameters.depth);
//             GameObject newBridge = Instantiate(bridgeParameters.bridgePrefab, (startPoint + endPoint) / 2f, Quaternion.identity) as GameObject;

//             // Rename the new bridge to "Bridge" (or any desired name)
//             newBridge.name = "Bridge";

//             // Assign a layer to the new bridge
//             newBridge.layer = LayerMask.NameToLayer("Platform");

//             // Create bridge with the same width as the distance between the two points
//             newBridge.transform.localScale = bridgeScale; 
//         }
//         else 
//         {
//             Debug.Log("Bridge already exists in the scene.");
//         }
//     }

//     public void OnDrawGizmosSelected() 
//     {
//         Gizmos.color = Color.black;
//         Gizmos.DrawWireSphere(transform.position, detectionRadius);
//     }   
// }


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