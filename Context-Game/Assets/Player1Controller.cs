using System.Collections;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public GameObject bridgePrefab;
    public float maxBridgeLength = 2f;
    public float bridgeBuildDuration = 2f; // Duration in seconds
    public float bridgeRetractDuration = 2f; // Duration in seconds to retract the bridge
    public LayerMask groundLayer;
    public float moveSpeed = 5f; // Movement speed
    public KeyCode retractBridgeKey = KeyCode.R; // Key to retract the bridge

    private bool isBuildingBridge = false;
    private bool isBridgeBuilt = false;
    private GameObject currentBridge;

    void Update()
    {
        // Toggle bridge building mode
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleBridgeBuilding();
        }

        // Retract bridge
        if (Input.GetKeyDown(retractBridgeKey) && isBridgeBuilt)
        {
            RetractBridge();
        }

        // Move player
        float horizontalInput = Input.GetAxis("Horizontal");
        float movement = horizontalInput * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector2(movement, 0f));

        // Clamp player position to stay within screen bounds
        Vector2 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -8f, 8f); // Adjust as needed for your screen bounds
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -4.5f, 4.5f); // Adjust as needed for your screen bounds
        transform.position = clampedPosition;
    }

    void ToggleBridgeBuilding()
    {
        if (!isBuildingBridge)
        {
            isBuildingBridge = true;
            StartCoroutine(BuildBridgeCoroutine());
        }
    }

    IEnumerator BuildBridgeCoroutine()
    {
        float elapsedTime = 0f;
        float currentBridgeLength = 0f;

        // Store initial position of the player
        Vector2 initialPlayerPosition = transform.position;

        // Create bridge
        currentBridge = Instantiate(bridgePrefab, initialPlayerPosition, Quaternion.identity);

        while (elapsedTime < bridgeBuildDuration)
        {
            currentBridgeLength = Mathf.Lerp(0, maxBridgeLength, elapsedTime / bridgeBuildDuration);
            Vector2 bridgeEndPosition = initialPlayerPosition + Vector2.right * currentBridgeLength;
            BuildBridge(currentBridge, initialPlayerPosition, bridgeEndPosition);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isBridgeBuilt = true;
    }

    void BuildBridge(GameObject bridge, Vector2 startPosition, Vector2 endPosition)
    {
        Vector2 midPoint = (startPosition + endPosition) / 2;
        bridge.transform.position = midPoint;
        bridge.transform.right = endPosition - startPosition;
        bridge.transform.localScale = new Vector3(Vector2.Distance(startPosition, endPosition), 1f, 1f);
    }

    void RetractBridge()
    {
        if (currentBridge != null)
        {
            StartCoroutine(RetractBridgeCoroutine());
        }
    }

    IEnumerator RetractBridgeCoroutine()
    {
        float elapsedTime = 0f;
        float currentBridgeLength = maxBridgeLength;

        while (elapsedTime < bridgeRetractDuration)
        {
            currentBridgeLength = Mathf.Lerp(maxBridgeLength, 0f, elapsedTime / bridgeRetractDuration);
            Vector2 bridgeEndPosition = (Vector2)transform.position + Vector2.right * currentBridgeLength;
            BuildBridge(currentBridge, transform.position, bridgeEndPosition);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(currentBridge);
        isBridgeBuilt = false;
    }
}
