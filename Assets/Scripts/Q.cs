using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q : MonoBehaviour
{
    public GameObject shootingLine;
    public Transform firePoint;

    public Animator animator;
    public GameObject bulletPrefab;
    public Camera camera;

    [Range(0, 30)][SerializeField]public int cooldownTime = 5;
    private float nextFireTime = 0f;
    
    public float animationTime;
    private float animationTimeLeft = 0f;
    private String animationName;
    private bool shooting = false;
    private float shootingAngle = 0f;

    private bool facingRight = true;
    

    // Update is called once per frame
    void Update()
    {
        CooldownHandling();
        AdjustFirePointAndAnimation();
        RotateLineAccordingToMouse();
        RotatePlayerAccordingToMouse();
        
        // Reduce animation time left
        if (animationTimeLeft > 0)
        {
            animationTimeLeft -= 0.01f;
        }
        else
        {
            if (shooting)
            {
                SetAnimationFalse();
                shooting = false;
            }
        }
        
    }

    void RotatePlayerAccordingToMouse()
    {
        // Right 
        if (shootingAngle <= 90 && shootingAngle > -90)
        {
            if (!facingRight)
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = true;
            }
        }
        // Left
        else
        {
            if (facingRight)
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = false;
            }
        }
    }

    void RotateLineAccordingToMouse()
    {
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookingDirection = mousePosition - shootingLine.transform.position;
        shootingAngle = Mathf.Atan2(lookingDirection.y, lookingDirection.x) * Mathf.Rad2Deg;
        shootingLine.transform.rotation = Quaternion.Euler(new Vector3(0,0, shootingAngle));
    }

    void Shoot()
    {
        // Instantiate bullet
        StartCoroutine(ShootingCoroutine());

        SetAnimationFalse();
        animator.SetBool(animationName, true);
        animationTimeLeft = animationTime;
        shooting = true;
    }

    IEnumerator ShootingCoroutine()
    {
        yield return new WaitForSeconds(animationTime/2);

        firePoint.rotation = Quaternion.Euler(new Vector3(0,0, shootingAngle));
        
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void SetAnimationFalse()
    {
        animator.SetBool("CrouchKick", false);
        animator.SetBool("HorizontalPunch", false);
        animator.SetBool("FlyingKick", false);
        animator.SetBool("VerticalKick", false);
    }

    private void CooldownHandling()
    {
        // If our ability is currently not on cooldown
        if (nextFireTime < Time.time)
        {
            // If we pressed Q
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // Start cooldown
                nextFireTime = Time.time + cooldownTime;
                
                Shoot();
            }
        }
        // If on cooldown
        else
        {
            
        }
    }

    private void AdjustFirePointAndAnimation()
    {
        // Set according to ranges
        
        // 15 to 65 or 115 to 165: Crouch Kick
        if (shootingAngle > 15 && shootingAngle <= 65)
        {
            animationName = "CrouchKick";
            firePoint.localPosition = new Vector3(0.15f, 0.18f, 0f);
        }
        else if (shootingAngle > 115 && shootingAngle < 165)
        {
            animationName = "CrouchKick";
            firePoint.localPosition = new Vector3(-0.15f, 0.18f, 0f);
        }
        // 15 to -15 or -165 to 165: Horizontal punch
        else if (shootingAngle >= -15 && shootingAngle <= 15)
        {
            animationName = "HorizontalPunch";
            firePoint.localPosition = new Vector3(0.15f, 0f, 0f);
        }
        else if (shootingAngle < -165 && shootingAngle >= 165)
        {
            animationName = "HorizontalPunch";
            firePoint.localPosition = new Vector3(-0.15f, 0f, 0f);
        }
        // -15 to -165: Flying kick
        else if (shootingAngle < -15 && shootingAngle >= -90)
        {
            animationName = "FlyingKick";
            firePoint.localPosition = new Vector3(0.15f, -0.18f, 0f);
        }
        else if (shootingAngle < -90 && shootingAngle >= -165)
        {
            animationName = "FlyingKick";
            firePoint.localPosition = new Vector3(-0.15f, -0.18f, 0f);
        }
        // 65 to 115: Vertical Kick
        else
        {
            animationName = "VerticalKick";
            firePoint.localPosition = new Vector3(0f, 0.18f, 0f);
        }
       
    }
}
