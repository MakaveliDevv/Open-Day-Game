using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object_", menuName = "Scriptables/Scriptables")]
public class ScriptObject : ScriptableObject
{
    public GameObject @object, detectObj, startObj;
    public Vector3 initialScale = new(0, 0, 0);
    private Quaternion initialRotation = Quaternion.identity;
    public Quaternion zRotation = Quaternion.Euler(0, 0, 0);

    [HideInInspector] public Quaternion finalRotation;

    // Constructor
    public ScriptObject()
    {
        // Combine initial rotation with the Z-axis rotation
        finalRotation = initialRotation * zRotation;
    }
}
