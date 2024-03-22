using UnityEngine;

public class TabletDrawing : MonoBehaviour
{
    // Texture to hold the drawing
    private Texture2D drawingTexture;
    // Texture size
    public int textureWidth = 256;
    public int textureHeight = 256;
    // Brush size
    public int brushSize = 10;
    // Reference to the tablet input
    private TabletInput tabletInput;

    void Start()
    {
        // Create a new texture for drawing
        drawingTexture = new Texture2D(textureWidth, textureHeight);

        // Find and assign the TabletInput script
        tabletInput = Object.FindFirstObjectByType<TabletInput>();
        if (tabletInput == null)
        {
            Debug.LogError("TabletInput script not found in the scene.");
        }
    }

    void Update()
    {
        // Check for tablet input
        if (tabletInput != null && tabletInput.IsDrawing)
        {
            // Get the position of the input on the texture
            Vector2Int pixelPos = tabletInput.GetDrawingPosition(drawingTexture.width, drawingTexture.height);
            // Draw on the texture
            DrawOnTexture(pixelPos.x, pixelPos.y);
        }
    }

    void DrawOnTexture(int x, int y)
    {
        // Set the color of the brush
        drawingTexture.SetPixel(x, y, Color.red);
        // Increase the brush size
        for (int i = 1; i < brushSize; i++)
        {
            drawingTexture.SetPixel(x + i, y, Color.red);
            drawingTexture.SetPixel(x - i, y, Color.red);
            drawingTexture.SetPixel(x, y + i, Color.red);
            drawingTexture.SetPixel(x, y - i, Color.red);
        }
        // Apply changes to the texture
        drawingTexture.Apply();
    }
}
