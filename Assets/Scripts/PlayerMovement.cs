using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    const float runSpeed = 40f;
    float _horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    bool crouch = false;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));
        verticalMove = Input.GetAxisRaw("Vertical");
        // If negative then down key, so crouch
        if (verticalMove < 0) 
        {
            crouch = true;
            animator.SetBool("Crouching", true);
        }
        // Else, then up key so jump
        else if (verticalMove > 0) 
        {
            jump = true;
            animator.SetBool("Jumping" , true);
        }
    }

    void FixedUpdate()
    {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, crouch, jump);
        crouch = false;
        animator.SetBool("Crouching", false);
    }

    public void OnLanding()
    {
        animator.SetBool("Jumping", false);
        jump = false;
    }

}
