using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public enum PointName 
    {
        BRIDGE_POINT, 
        LADDER_POINT, 
        GRAPPLER_POINT,
        PIVOT_POINT,
        UNASSIGNED
    }

    public enum PointType 
    {
        START_POINT,
        END_POINT,
        UNASSIGNED
    }

    public enum PivotPoint 
    {
        START_PIVOTPOINT,
        END_PIVOTPOINT,
        UNASSIGNED
    }


    public PointName pointName;
    public PointType pointType;
    public PivotPoint pivotPoint;

    Vector3 previousPosition;
    Vector3 currentPosition;
    public bool isMoving;

    void Start() 
    {
        // previousPosition = transform.position;
    }

    void Update() 
    {
        // Moving(transform);
    }

    public bool Moving(Transform _point) 
    {
        previousPosition = _point.transform.position;
        
        if(pointName == PointName.PIVOT_POINT 
        && pivotPoint == PivotPoint.START_PIVOTPOINT) 
        {
            currentPosition = _point.transform.position;
            if(currentPosition != previousPosition) 
            {
                previousPosition = currentPosition;
                Debug.Log(gameObject.name + " : " + previousPosition);
                
                isMoving = true;
                return true;
            
            } else 
            {
                isMoving = false;
                return false;
            }
        }

        return false;
    }

    
    public bool Movingg(Transform _point) 
    {
        // Check if this is the first time moving
        if (previousPosition == Vector3.zero)
        {
            previousPosition = _point.transform.position;
            return false; // No movement yet
        }
        
        Vector3 currentPosition = _point.transform.position;
        
        // Compare current position with the previous position
        if (currentPosition != previousPosition) 
        {
            previousPosition = currentPosition;
            isMoving = true;
            // Debug.Log(gameObject.name + " : " + previousPosition);
            return true; // Object is moving
        } 
        else 
        {
            isMoving = false;
            return false; // Object is not moving
        }
    }
}
