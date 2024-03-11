using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Swing : MonoBehaviour
{
    public float speed = 2.0f;
    public float angle = 20.0f;

    float currentAngle = 0f;
    float timer;

    void Update() 
    {
        timer += Time.deltaTime * speed;
        float angle = Mathf.Sin(timer) * this.angle;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + currentAngle));
    }
}
