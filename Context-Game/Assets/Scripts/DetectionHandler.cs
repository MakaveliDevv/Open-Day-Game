
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class DetectionHandler : MonoBehaviour
{
    public BridgeSO bridgeParameters;
    public float detectionRadius = 3f;
    private Vector2 startPoint, endPoint;
    private PlayerManager player;
    private BridgeManager bridgeManager;
    public LayerMask layerMask;
    public float rayCastOffsetY;
    public bool endPointDetected = false;

    void Start() 
    {
        player = GetComponent<PlayerManager>();
        bridgeManager = FindFirstObjectByType<BridgeManager>();
    }

    public void Update()
    {
        CheckForEndPoint();
        if (Input.GetKeyDown(KeyCode.P) && endPointDetected)
        {
            CreateBridge();
        }
    }

    public void CheckForEndPoint()
    {
        endPointDetected = false;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        BridgeManager bridgeManager = Object.FindFirstObjectByType<BridgeManager>();
        bridgeManager.pointsThatCanBeConnected.Clear();

        foreach (var point in hitColliders)
        {   
            ConnectingPoints connectPoint = point.GetComponent<ConnectingPoints>();

            if(connectPoint != null && connectPoint.connectPointType == ConnectingPoints.ConnectPointType.BRIDGE_POINT 
            && connectPoint.inRange && connectPoint.connectPoint == ConnectingPoints.ConnectPoint.STARTING_POINT)
            {
                bridgeManager.pointsThatCanBeConnected.Add(connectPoint.transform);
                
                Vector2 rayCastPos = new(connectPoint.transform.position.x, connectPoint.transform.position.y - rayCastOffsetY);
                RaycastHit2D hit = Physics2D.Raycast(rayCastPos, Vector2.right, 100f, layerMask);

                if(hit.collider != null) 
                {
                    Debug.DrawLine(rayCastPos, hit.point, Color.green);
                    endPointDetected = true;
                    bridgeManager.pointsThatCanBeConnected.Add(hit.transform);
                
                } else 
                {
                    Debug.DrawLine(rayCastPos, hit.point, Color.red);
                    endPointDetected = true;
                } 
            } 
        }
    }

    private void CreateBridge()
    {
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
            Debug.LogWarning("Could not find both starting and ending points. " );
            return;
        }

        // Check if there's already a bridge between these points
        if (!bridgeManager.IsBridgePresent(startTransform, endTransform))
        {
            Vector3 bridgeScale = new(Vector2.Distance(startTransform.position, endTransform.position), bridgeParameters.height, bridgeParameters.depth);
            GameObject newBridge = Instantiate(bridgeParameters.bridgePrefab, (startTransform.position + endTransform.position) / 2f, Quaternion.identity) as GameObject;

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
            Debug.Log("Bridge already exists between these points! ");
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}