using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LeverInteraction : MonoBehaviour
{
    private float chargeCounter = 0;
    private bool leverTouchingPlayer = false;
    private MoveToNextLeverLevel logic;
    public int currentLeverLevel = 0;

    [Range(0, 30)] [SerializeField] public int chargeLength;
    
    private void Start()
    {
        logic = gameObject.GetComponent<MoveToNextLeverLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        // Still playing
        if (currentLeverLevel >= 0 && currentLeverLevel < 3)
        {
            if (chargeCounter > chargeLength)
            {
                Debug.Log("Charged fully!");
                // moving to next stage logic
                currentLeverLevel = logic.Execute(currentLeverLevel);

                chargeCounter = 0;
            
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
        // Won
        else
        {
            Debug.Log("YOU WON!");
        }

        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!leverTouchingPlayer) leverTouchingPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            leverTouchingPlayer = false;
        }
    }
}
