using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : MonoBehaviour
{
    #region Singleton
    public static BridgeManager instance;

    void Awake() 
    {
        instance = this;
    }
    #endregion
    
    public List<Transform> endPointsDetected = new();
    public List<Bridge> createdBridges = new();

    // Function to check if there is already a bridge between two points
    public bool IsBridgePresent(Transform point1, Transform point2)
    {
        foreach (Bridge bridge in createdBridges)
        {
            if ((bridge.startPoint == point1 && bridge.endPoint == point2) || (bridge.startPoint == point2 && bridge.endPoint == point1))
            {
                return true;
            }
        }
        return false;
    }
}

// Class for Bridge
public class Bridge
{
    public Transform startPoint;
    public Transform endPoint;

    public Bridge(Transform start, Transform end)
    {
        startPoint = start;
        endPoint = end;
    }
}
