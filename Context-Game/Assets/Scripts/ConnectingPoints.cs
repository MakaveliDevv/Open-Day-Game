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

    public ConnectPoint pointType;
    public Transform target;
    [SerializeField] public float detectionRadius;
    public float distance;
    public bool inRangeForStartPoint = false;
    

    void Update()
    {                
        // Check if the player is close
        if(pointType == ConnectPoint.STARTING_POINT) 
        {
            distance = Vector2.Distance(target.transform.position, transform.position);
            if(distance < detectionRadius) 
            {
                inRangeForStartPoint = true;
                // targetScript.CheckForEndPoint();

            } else 
            {
                inRangeForStartPoint = false;
            }
        }         
    }

    public void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
