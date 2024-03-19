using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float speed = 6f;
    private void OnTriggerStay2D(Collider2D collider) 
    {
        if(collider.GetComponent<PlayerManager>()) 
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            
            if(Input.GetKey(KeyCode.P)) 
            {
                rb.velocity = new(rb.velocity.x, speed); // Up
            
            } else if(Input.GetKey(KeyCode.L)) 
            {
                rb.velocity = new(rb.velocity.x, -speed); // Down
            
            } else 
            {
                rb.velocity = new(0, 1);
            }

        }
    }
}
