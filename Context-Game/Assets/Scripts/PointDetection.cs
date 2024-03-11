using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointDetection : MonoBehaviour
{
    public enum ConnectingPoints 
    {
        START,
        END
    }

    public ConnectingPoints point;
    public Transform target;
    [SerializeField] public float detectionRadius;
    public float distance;
    public bool inRangeForStartPoint = false;
    

    void Update()
    {        
        // Detection targetScript = target.GetComponent<Detection>();
        // targetScript.inRangeForEndPoint = false;
        
        // Check if the player is close
        if(point == ConnectingPoints.START) 
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

