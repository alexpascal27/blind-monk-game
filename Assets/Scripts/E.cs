using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class E : MonoBehaviour
{
    [Range(0, 10)][SerializeField]public int innerRadiusIncrease = 2;
    [Range(0, 10)][SerializeField]public int outerRadiusIncrease = 4;
    private float cooldownTime;
    private float activeTime;
    private float nextFireTime = 0f;
    private bool rangeCurrentlyIncreased  = false;
    
    public Light2D innerLight;
    public Light2D outerLight;

    private void Start()
    {
        cooldownTime = PlayerPrefs.GetFloat("ECooldown");
        activeTime = PlayerPrefs.GetFloat("EActiveTime");
    }

    private void Update()
    {
        // If our ability is currently not on cooldown
        if (nextFireTime < Time.time)
        {
            // If we pressed E
             if (Input.GetKeyDown(KeyCode.E))
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
                if (!rangeCurrentlyIncreased)
                {
                    // Increase range
                    innerLight.pointLightInnerRadius += innerRadiusIncrease;
                    innerLight.pointLightOuterRadius += outerRadiusIncrease;
                    outerLight.pointLightInnerRadius += innerRadiusIncrease;
                    outerLight.pointLightOuterRadius += outerRadiusIncrease;
                    
                    rangeCurrentlyIncreased = true;
                }
            }
            // If no longer active but on cooldown
            else if (nextFireTime - activeTime > Time.time)
            {
                if (rangeCurrentlyIncreased)
                {
                    // Decrease range
                    innerLight.pointLightInnerRadius -= innerRadiusIncrease;
                    innerLight.pointLightOuterRadius -= outerRadiusIncrease;
                    outerLight.pointLightInnerRadius -= innerRadiusIncrease;
                    outerLight.pointLightOuterRadius -= outerRadiusIncrease;
                    
                    rangeCurrentlyIncreased = false;
                }
                
            }
        }

    }
}