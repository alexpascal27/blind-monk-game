using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyMovement : MonoBehaviour
{

    private int lifePoints;
    int initalLifePoints;
    public EnemySpawn m_enemySpawnScript;

    private Vector2 velocity;
    private Rigidbody2D rb;

    public GameObject lightObject;
    private Light2D light;
    bool lightOn = false;
    float lightTimer = 0f;
    [Range(0f, 1f)][SerializeField] private float timerLength;
    [Range(0f, 5f)][SerializeField] private float lifePointsScaling;

    [Range(0.01f, 0.5f)][SerializeField] private float scaleDecreaseFactor;
    [Range(0.1f, 2f)][SerializeField] private float velocityScaleFactor;

    void Start() 
    {
       rb = GetComponent<Rigidbody2D>();
       light = lightObject.GetComponent<Light2D>();
       light.enabled = false;
       light.color = Color.green;

       // Assign life points based on scale
        lifePoints = (int)(gameObject.transform.localScale.x) * 10;
        lifePoints = (int)(lifePoints * lifePointsScaling);
        initalLifePoints = lifePoints;
    }

    // Update is called once per frame
    void Update()
    {
        // Update velocity after each frame as we can't predict a collision
        velocity = rb.velocity;
        // Ran out of life points, so die
        if(lifePoints==0)
        {
            m_enemySpawnScript.DoTheSpawn(1);
            Destroy(gameObject);
        }

        if(lightOn)
        {
            SetColourAccordingToLife();
            
            lightTimer -= Time.smoothDeltaTime;
            if(lightTimer<=0)
            {
                lightOn = false;
                lightTimer = 0f;
                light.enabled = false;
            }
        }
    }

    private void SetColourAccordingToLife()
    {
        // 3/3 green
        // 2/3 yellow
        // 1/3 red
        // 0/3 black

        if(lifePoints<=0)
        {
            light.color = Color.black;
        }
        else
        {
            float thirdValue = initalLifePoints / lifePoints;
            if(lifePoints > 0 && lifePoints <= thirdValue)
            {
                light.color = Color.red;
            }
            else if(lifePoints > thirdValue && lifePoints < 2*thirdValue)
            {
                light.color = Color.yellow;
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
        light.enabled = true;;
    }
}
