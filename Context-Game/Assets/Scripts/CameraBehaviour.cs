using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 cameraPosition;
    public GameObject player;
    public float cameraSpeed;
    
    void Start()
    {
        ResetCamera();
        SetPosition(cameraPosition);
    }

    void Update()
    {
        FollowPlayer();
    }

    private void ResetCamera() 
    {
        transform.position = new(0f, 0f, -10f);
    }

    private void UpdateCameraPosition() 
    {
        cameraPosition.
    }

    private void SetPosition(Vector2 _position) 
    {
        transform.position = _position;
    }

    private void FollowPlayer() 
    {
        if(player != null) 
        {
            Vector3 targetPosition = player.transform.position + cameraPosition;
            transform.position = Vector2.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
        }
    }
}
