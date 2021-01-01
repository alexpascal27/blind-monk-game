using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LeverInteraction : MonoBehaviour
{
    public Animator animator;
    
    public Texture leverLevelUnavailable;
    public Texture leverNotCharged;
    public Texture leverCharge0;
    public Texture leverCharge1;
    public Texture leverCharge2;
    public Texture leverCharge3;
    public Texture leverCharge4;
    public Texture leverFullyCharged;
    
    private float chargeCounter = 0;
    private bool leverTouchingPlayer = false;
    private bool charging = false;
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
                // moving to next stage logic
                currentLeverLevel = logic.Execute(currentLeverLevel);
                chargeCounter = 0;
            }
        
            if (Input.GetKey(KeyCode.Space) && leverTouchingPlayer)
            {
                chargeCounter += Time.deltaTime;
                charging = true;
                animator.SetBool("Charging", true);
            }
            else
            {
                chargeCounter = 0;
                charging = false;
                animator.SetBool("Charging", false);
            }
        }
        // Won
        else
        {
            SceneManager.LoadScene(3);
        }
    }

    private void OnGUI()
    {
        DisplayBase3LeverIcons();
        DisplayCurrentLeverIcons();
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

    private void DisplayCurrentLeverIcons()
    {
        if (chargeCounter > 0 && chargeCounter < 1)
        {
            GUI.Label(new Rect(1125 + currentLeverLevel * 90,85,70,70), leverCharge0);
        }
        else if (chargeCounter >= 1 && chargeCounter < 2)
        {
            GUI.Label(new Rect(1125 + currentLeverLevel * 90,85,70,70), leverCharge1);
        }
        else if (chargeCounter >= 2 && chargeCounter < 3)
        {
            GUI.Label(new Rect(1125 + currentLeverLevel * 90,85,70,70), leverCharge2);
        }
        else if (chargeCounter >= 3 && chargeCounter < 4)
        {
            GUI.Label(new Rect(1125 + currentLeverLevel * 90,85,70,70), leverCharge3);
        }
        else if (chargeCounter >= 4 && chargeCounter < 5)
        {
            GUI.Label(new Rect(1125 + currentLeverLevel * 90,85,70,70), leverCharge4);
        }
        else
        {
            GUI.Label(new Rect(1125 + currentLeverLevel * 90,85,70,70), leverNotCharged);
        }
    }

    private void DisplayBase3LeverIcons()
    {
        switch (currentLeverLevel)
        {
            // 2nd and 3rd are x
            case(0):
                // 2nd
                GUI.Label(new Rect(1215,85,70,70), leverLevelUnavailable);
                // 3rd
                GUI.Label(new Rect(1305,85,70,70), leverLevelUnavailable);
                break;
            
            // 1st is done, 3rd is x
            case(1):
                // 1st
                GUI.Label(new Rect(1125,85,70,70), leverFullyCharged);
                // 3rd
                GUI.Label(new Rect(1305,85,70,70), leverLevelUnavailable);
                break;
            
            // 1st and 2nd are done
            case(2):
                // 1st
                GUI.Label(new Rect(1125,85,70,70), leverFullyCharged);
                // 2nd
                GUI.Label(new Rect(1215,85,70,70), leverFullyCharged);
                break;
        }
    }
}
