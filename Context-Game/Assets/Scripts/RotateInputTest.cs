using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInputTest : MonoBehaviour
{
    public GameObject testObj;
    void Start() 
    {
        if(testObj == null)
            return;
            
        testObj = GameObject.FindGameObjectWithTag("DesignersObject");
    }

    void Update() 
    {
        // Get the instantiated object
        if (testObj == null)
            return;

            testObj = GameObject.FindGameObjectWithTag("DesignersObject");
            if (testObj == null)
            {
                Debug.Log("No object found with tag 'DesignersObject'");
                return;
            }
        

        // If left mouse button pressed, rotate -10 degrees around Z-axis
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            testObj.transform.Rotate(Vector3.forward, -5f);
            Debug.Log("minus 10 on the Z axis");
        }

        // If right mouse button pressed, rotate +10 degrees around Z-axis
        if (Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            testObj.transform.Rotate(Vector3.forward, 5f);
            Debug.Log("plus 10 on the Z axis");
        }
    }
}
