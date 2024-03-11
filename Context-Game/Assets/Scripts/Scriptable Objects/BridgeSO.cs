using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BridgeParameters_", menuName = "Bridge/BridgeSO")]
public class BridgeSO : ScriptableObject
{
    public GameObject bridgePrefab;
    public float height = .5f, depth = 1f;
}
