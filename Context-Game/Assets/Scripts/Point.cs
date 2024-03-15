using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject point;
    public BridgeManager bridgeManager;
    private Vector2 currentPosition;
    [SerializeField] private float detectRadius;

    // void Awake () 
    // {
    //     bridgeManager = BridgeManager.instance;
    // }

    void Start() 
    {
        bridgeManager = FindFirstObjectByType<BridgeManager>();
        // currentPosition = transform.position;
        transform.position = point.transform.position;

        
    }

    void Update() 
    {
        transform.position = point.transform.position;
        // currentPosition = point.transform.position;
        // EndPointDetected();
    }
    
    // private void OnTriggerEnter2D(Collider2D collider2D) 
    // {
    //     ConnectingPoints point = collider2D.GetComponent<ConnectingPoints>();
    //     if(point.connectPointType == ConnectingPoints.ConnectPointType.BRIDGE_POINT
    //     && point.connectPoint == ConnectingPoints.ConnectPoint.END_POINT) 
    //     {
    //         Debug.Log("Made contact");
    //     }
    // }

    public bool EndPointDetected() 
    {
        bridgeManager.endPointsDetected.Clear();
        Collider2D hitPoint = Physics2D.OverlapCircle(transform.position, detectRadius);
        ConnectingPoints connectPoint = hitPoint.GetComponent<ConnectingPoints>();

        if(connectPoint != null && connectPoint.connectPointType == ConnectingPoints.ConnectPointType.BRIDGE_POINT 
        && connectPoint.inRange && connectPoint.connectPoint == ConnectingPoints.ConnectPoint.END_POINT)
        {
            bridgeManager.endPointsDetected.Add(connectPoint.transform);
            Debug.Log("Found an end point");
            return true;
            
        } else 
        {
            return false;
        }
    }
}
