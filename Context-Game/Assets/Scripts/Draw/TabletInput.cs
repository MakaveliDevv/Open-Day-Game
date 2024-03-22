using UnityEngine;

public class TabletInput : MonoBehaviour
{
    // Flag to track if the player is drawing
    public bool isDrawing = false;

    // Update is called once per frame
    void Update()
    {
        // Check for tablet input
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Player is drawing
            isDrawing = true;
        }
        else
        {
            // Player has stopped drawing
            isDrawing = false;
        }
    }

    // Method to check if the player is drawing
    public bool IsDrawing
    {
        get { return isDrawing; }
    }

    // Method to get the position of the drawing on the texture
    public Vector2Int GetDrawingPosition(int textureWidth, int textureHeight)
    {
        // Convert mouse position to texture coordinates
        Vector2 mousePos = Input.mousePosition;
        Vector2Int texturePos = new Vector2Int(
            Mathf.RoundToInt(mousePos.x / Screen.width * textureWidth),
            Mathf.RoundToInt(mousePos.y / Screen.height * textureHeight)
        );

        // Ensure position is within texture bounds
        texturePos.x = Mathf.Clamp(texturePos.x, 0, textureWidth - 1);
        texturePos.y = Mathf.Clamp(texturePos.y, 0, textureHeight - 1);

        return texturePos;
    }
}
