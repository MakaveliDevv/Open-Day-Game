using UnityEngine;

public class KeyInputDeveloper : InputController
{
    void Update()
    {
        if(startPointObj != null && startPoint != null 
        && detectObj != null && endPoint != null) 
        {
            startPointObj.transform.position = startPoint.transform.position;
            detectObj.transform.position = endPoint.transform.position;
        }
        
        // Z: Extend
        // X: Scale_Back_From_Start_To_End_Point
        // C: Release
        

        // MAKE A CONDITION THAT ONLY THE SELECTED PLAYER CAN DO THIS INPUT
        // Extend positive
        if(objectCreated  && extendPoint1 != null && extendPoint2 != null && toExtandBack != null) 
        {
            if(_playerManag.playerType == PlayerManager.PlayerType.DEVELOPER)
            {
                Debug.Log("Input done by the: " + _playerManag.playerType);
                ScaleInput(extendPoint1.gameObject, extendPoint2.gameObject, Vector2.right, KeyCode.Z, KeyCode.X, KeyCode.C, _instantiateObj);
            }
        }
    }
}
