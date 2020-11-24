using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    private int lifePoints;

    private Vector2 velocity;
    private Rigidbody2D rb;
    public GameObject light;
    public Light lightNew;

    bool lightOn = false;
    float lightTimer = 0f;
    [Range(0f, 1f)][SerializeField] private float timerLength;
    [Range(0.01f, 0.5f)][SerializeField] private float scaleDecreaseFactor;
    [Range(0.1f, 2f)][SerializeField] private float velocityScaleFactor;

    void Start() 
    {
       rb = GetComponent<Rigidbody2D>();
       light.GetComponent<Light2D>().color = Color.green;
       light.SetActive(false);

       // Assign life points based on scale
        lifePoints = (int)(gameObject.transform.localScale.x) * 10;
        lifePoints = (int)(lifePoints / 1.5);
    }

    // Update is called once per frame
    void Update()
    {
        // Update velocity after each frame as we can't predict a collision
        velocity = rb.velocity;
        // Ran out of life points, so die
        if(lifePoints==0)
        {
            Destroy(gameObject);
        }

        if(lightOn)
        {
            lightTimer -= Time.smoothDeltaTime;
            if(lightTimer<=0)
            {
                lightOn = false;
                lightTimer = 0f;
                light.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        HandleReflection(other);
       
        IncreaseVelocityAfterBounce();
        
        ScaleAndLifePointChanges();

        TurnLightOnForXSeconds();
    }

    private void HandleReflection(Collision2D other)
    {
        var speed = velocity.magnitude;
        var reflectedDirection = Vector2.Reflect(velocity.normalized, other.contacts[0].normal);
        rb.velocity = reflectedDirection * speed;
    }

    private void IncreaseVelocityAfterBounce()
    {
        float newX = rb.velocity.x;
        float newY = rb.velocity.y;
        if(rb.velocity.x < 0)
        {
            newX -= velocityScaleFactor;
        }
        else
        {
            newX += velocityScaleFactor;
        }

        if(rb.velocity.y < 0)
        {
            newY -= velocityScaleFactor;
        }
        else
        {
            newY += velocityScaleFactor;
        }
        rb.velocity = new Vector2(newX, newY);
    }

    private void ScaleAndLifePointChanges()
    {
        float newScale = gameObject.transform.localScale.x - scaleDecreaseFactor;
        // Change scale and life points
        gameObject.transform.localScale = new Vector2(newScale, newScale); 
        lifePoints--;
    }

    private void TurnLightOnForXSeconds()
    {
        lightOn = true;
        lightTimer = timerLength;
        light.SetActive(true);
    }
}
