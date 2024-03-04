using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingPoint : MonoBehaviour
{
    private Transform targetPlayer;
    public float detectionRadius;
    private float distance;

    void Awake() 
    {
        targetPlayer = PlayerManager.instance.gameObject.transform;
    }

    void Update() 
    {
        distance = Vector2.Distance(targetPlayer.position, transform.position);
    }

    public void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
