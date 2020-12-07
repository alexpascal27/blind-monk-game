using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class R : MonoBehaviour
{
    [Range(0, 30)][SerializeField]public int cooldownTime = 5;
    [Range(0, 5)][SerializeField]public int activeTime = 2;
    private float nextFireTime = 0f;
    private bool currentlyUlting  = false;
    
    public Light2D innerLight;
    private Color initialColor;

    private void Start()
    {
        initialColor = innerLight.color;
    }

    // Update is called once per frame
    void Update()
    {
        // If our ability is currently not on cooldown
        if (nextFireTime < Time.time)
        {
            // If we pressed E
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Start cooldown
                nextFireTime = Time.time + cooldownTime + activeTime;
            }
        }
        else
        {
            // If active not yet on cooldown
            if (nextFireTime - cooldownTime > Time.time)
            {
                if (!currentlyUlting)
                {
                    // Positive ulting logic
                    
                    
                    innerLight.color = Color.red;
                    
                    currentlyUlting = true;
                }
            }
            // If no longer active but on cooldown
            else if (nextFireTime - activeTime > Time.time)
            {
                if (currentlyUlting)
                {
                    // Negative ulting logic
                    
                    innerLight.color = initialColor;
                    
                    currentlyUlting = false;
                }
                
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        // If colliding with an enemy and r is not on cooldown
        if (other.gameObject.tag == "Enemy" && nextFireTime < Time.time)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
