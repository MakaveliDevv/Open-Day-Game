using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPoint : MonoBehaviour
{
    public enum ConnectPointType 
    {
        BRIDGE_POINT,
        LADDER_POINT,
        GRAPPLER_POINT
    }

    public ConnectPointType connectPointType;
}