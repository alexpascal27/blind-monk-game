using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteraction : MonoBehaviour
{
    private float chargeCounter = 0;
    private bool leverTouchingPlayer = false;

    // Update is called once per frame
    void Update()
    {
        if (chargeCounter > 2)
        {
            // moving to next stage logic
            
        }
        
        if (Input.GetKey(KeyCode.Space) && leverTouchingPlayer)
        {
            Debug.Log("Charging: " + chargeCounter);
            chargeCounter += Time.deltaTime;
        }
        else
        {
            chargeCounter = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Lever enter player");
            if(!leverTouchingPlayer) leverTouchingPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Lever exit player");
            leverTouchingPlayer = false;
        }
    }
}
