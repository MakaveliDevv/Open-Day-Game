using UnityEngine;

public class KeyInputArtist : InputController
{
    void Update() 
    {
        if(startPointObj != null && startPoint != null 
        && detectObj != null && endPoint != null) 
        {
            startPointObj.transform.position = startPoint.transform.position;
            detectObj.transform.position = endPoint.transform.position;
        }
        // Space: Extend
        // Q: Scale_Back_From_Start_To_End_Point
        // E: Release


        if(objectCreated  && extendPoint1 != null && extendPoint2 != null && toExtandBack != null) 
        {
            ScaleInput(extendPoint1.gameObject, extendPoint2.gameObject, Vector2.right, KeyCode.Space, KeyCode.E, KeyCode.Q, _instantiateObj);
        }
    }
}
