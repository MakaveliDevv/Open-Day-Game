using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject point;

    void Update() 
    {
        transform.position = point.transform.position;
    }
    
    private void OnTriggerEnter2D(Collider2D collider2D) 
    {
        ConnectingPoints point = collider2D.GetComponent<ConnectingPoints>();
        if(point.connectPointType == ConnectingPoints.ConnectPointType.BRIDGE_POINT
        && point.connectPoint == ConnectingPoints.ConnectPoint.END_POINT) 
        {
            Debug.Log("Made contact");
        }
    }
}
