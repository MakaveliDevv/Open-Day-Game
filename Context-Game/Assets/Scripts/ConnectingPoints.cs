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

    private Transform player;
    [SerializeField] private float detectRadius;
    public float distance;
    public bool inRangeForStartPoint = false;

    public bool inRange = false;
    
    void Awake() 
    {
        player = PlayerManager.instance.transform;
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
}