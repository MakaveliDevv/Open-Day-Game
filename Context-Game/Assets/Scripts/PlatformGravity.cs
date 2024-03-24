using UnityEngine;

public class PlatformGravity : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Disable gravity or any other downward forces on the player
            other.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Re-enable gravity or any other downward forces on the player
            other.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }
}