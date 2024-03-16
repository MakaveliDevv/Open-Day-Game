using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : MonoBehaviour
{
    #region Singleton
    [HideInInspector] public static BridgeManager instance;

    void Awake() 
    {
        if(instance != null) 
            Destroy(this);
        
        else
            instance = this;

        DontDestroyOnLoad(instance);    
    }
    #endregion
    

    // // Function to check if there is already a bridge between two points
    // public bool IsBridgePresent(Transform point1, Transform point2)
    // {
    //     foreach (Bridge bridge in createdBridges)
    //     {
    //         if ((bridge.startPoint == point1 && bridge.endPoint == point2) || (bridge.startPoint == point2 && bridge.endPoint == point1))
    //         {
    //             return true;
    //         }
    //     }
    //     return false;
    // }
}
