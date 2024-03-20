using UnityEngine;

public class KeyInputArtist : InputController
{
    protected int myVariable = 10; // Example variable

    void Update() 
    {
        if(startPointObj != null && startPoint != null 
        && detectObj != null && endPoint != null) 
        {
            startPointObj.transform.position = startPoint.transform.position;
            detectObj.transform.position = endPoint.transform.position;
        }

        if(Input.GetKeyDown(KeyCode.G)) // Check also which player it is 
        {
            CreateObject();
        }

        // P: Extend positive
        // O: Release
        // K: Scale_Back_From_Start_To_End_Point
        playerManag.WhichPlayer();

        // Extend positive
        if(objectCreated  && extendPoint1 != null && extendPoint2 != null && toExtandBack != null) 
        {
            ScaleInput(extendPoint1.gameObject, extendPoint2.gameObject, Vector2.right, KeyCode.P, KeyCode.O, KeyCode.K, _instantiateObj);
            // ScaleInput(extendPoint2.gameObject, toExtandBack.gameObject, Vector2.right, KeyCode.L, KeyCode.O, KeyCode.K, _instantiateObj);
        }
    }
}
