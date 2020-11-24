using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        
        // Decrement speed
        DecrementVelocity(2f);
    }

    private void DecrementVelocity(float factor)
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Magnitude Before: " + rb.velocity.magnitude);
        float newX = rb.velocity.x + factor;
        float newY = rb.velocity.y + factor;
        rb.velocity = new Vector2(newX, newY);
        Debug.Log("Magnitude After: " + rb.velocity.magnitude);
    }
}
