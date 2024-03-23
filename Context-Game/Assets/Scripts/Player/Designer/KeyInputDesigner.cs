using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputDesigner : InputController
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
            if(_playerManag.playerType == PlayerManager.PlayerType.DESIGNER)
            {
                Debug.Log("Input done by the: " + _playerManag.playerType);
                ScaleInput(extendPoint1.gameObject, extendPoint2.gameObject, Vector2.right, KeyCode.V, KeyCode.B, KeyCode.N, _instantiateObj);
            }
        }
    }
}
