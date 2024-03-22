using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D _rb;
    public float moveSpeed = 5f;

    // void Awake()
    // {
    //     _rb = GetComponent<Rigidbody2D>();
    // }

    public void Move(Vector2 direction)
    {
        Vector2 movement = direction * moveSpeed * Time.deltaTime;
        _rb.MovePosition(_rb.position + movement);
    }
}
