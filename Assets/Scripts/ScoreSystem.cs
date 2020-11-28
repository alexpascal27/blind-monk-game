using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        // Decrement speed
        DecrementVelocity(2f);
    }

    private void DecrementVelocity(float factor)
    {
        _rb = GetComponent<Rigidbody2D>();
        Vector2 velocity = _rb.velocity;
        float newX = velocity.x + factor;
        float newY = velocity.y + factor;
        _rb.velocity = new Vector2(newX, newY);
    }
}
