using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BridgeParameters_", menuName = "Bridge/BridgeSO")]
public class BridgeSO : ScriptableObject
{
    public GameObject objectPrefab;
    public float height = .5f, depth = 1f;

    void Start() 
    {
        // Test();
    }

    // private void Test() 
    // {
    //     objectPrefab.transform.localScale = new Vector3(objectPrefab.transform.localScale.x, height, depth);
    // }
}
