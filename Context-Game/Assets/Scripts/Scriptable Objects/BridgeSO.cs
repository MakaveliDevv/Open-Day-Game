using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BridgeParameters_", menuName = "Bridge/BridgeSO")]
public class BridgeSO : ScriptableObject
{
    public GameObject objectPrefab;
    public Vector3 initialScale = new(.2f, .2f, 1f);
    public float height = .2f, depth = 1f;
}
