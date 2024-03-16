using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectionPoint : MonoBehaviour
{
    public static DetectionPoint instance;
    
    #region Singleton
    void Awake() 
    {
        if(instance != null) 
            Destroy(this);
        
        else
            instance = this;
    }
    #endregion

    public GameObject point;
    [HideInInspector] public GameManager gameManager;
    [SerializeField] private float detectRadius;

    void Start() 
    {
        gameManager = GameManager.instance;
        transform.position = point.transform.position;

        // // Create a new GameObject for visualization
        // GameObject sphereVisualizer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        
        // // Remove the collider component from the visualization sphere
        // Destroy(sphereVisualizer.GetComponent<Collider>());

        // // Set the position of the visualization sphere to match the transform position
        // sphereVisualizer.transform.position = transform.position;

        // // Scale the visualization sphere based on the detection radius
        // sphereVisualizer.transform.localScale = new Vector3(detectRadius * 2f, detectRadius * 2f, detectRadius * 2f);
        
        // // Set the visualization sphere as a child of the current GameObject
        // sphereVisualizer.transform.parent = transform;
        
    }

    void Update() 
    {
        transform.position = point.transform.position;
        PointDetected();
    }

    public bool PointDetected() 
    {
        gameManager.connectPointList.Clear();
        Collider2D hitPoint = Physics2D.OverlapCircle(transform.position, detectRadius);

        if (hitPoint != null)
        {
            ConnectPoint connectPoint = hitPoint.GetComponent<ConnectPoint>();

            if (connectPoint != null && connectPoint.connectPointType == ConnectPoint.ConnectPointType.BRIDGE_POINT)
            {
                gameManager.connectPointList.Add(connectPoint);
                return true;
            }
        }

        return false;
    }

    public void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
