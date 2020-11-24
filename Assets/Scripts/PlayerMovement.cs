using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    const float runSpeed = 40f;
    const float verticalSpeed = 10f;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    bool crouch = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical");
        // If negative then down key, so crouch
        if (verticalMove == -1) crouch = true;
        // Else, then up key so jump
        else if (verticalMove > 0) jump = true;
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        if (crouch == true) Debug.Log("Player is crouching");
        jump = false;
        crouch = false;
    }

    
}
