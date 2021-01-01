using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyMovement : MonoBehaviour
{

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
       light.color = Color.white;

    }

    // Update is called once per frame
    void Update()
    {
        // Update velocity after each frame as we can't predict a collision
        velocity = rb.velocity;

        if(lightOn)
        {
            lightTimer -= Time.smoothDeltaTime;
            if(lightTimer<=0)
            {
                lightOn = false;
                lightTimer = 0f;
                light.enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        HandleReflection(other);

        TurnLightOnForXSeconds();
    }

    private void HandleReflection(Collision2D other)
    {
        var speed = velocity.magnitude;
        var reflectedDirection = Vector2.Reflect(velocity.normalized, other.contacts[0].normal);
        rb.velocity = reflectedDirection * speed;
    }

    private void TurnLightOnForXSeconds()
    {
        lightOn = true;
        lightTimer = timerLength;
        light.enabled = true;
    }
}
