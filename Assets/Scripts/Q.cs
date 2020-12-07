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
    

    // Update is called once per frame
    void Update()
    {
        CooldownHandling();
        AdjustFirePointAndAnimation();
        RotateLineAccordingToMouse();
        
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
        
        // 25 - 75 or 285 - 335: Crouch Kick
        if (shootingAngle > 25 && shootingAngle <= 75)
        {
            animationName = "CrouchKick";
            firePoint.position = camera.ScreenToWorldPoint(new Vector3(0.15f, 0.18f, 0f));
        }
        else if (shootingAngle > 285 && shootingAngle <= 335)
        {
            animationName = "CrouchKick";
            firePoint.position = new Vector3(-0.15f, 0.18f, 0f);
        }
        // 75 - 130 or 230 - 285: Horizontal punch
        else if (shootingAngle > 75 && shootingAngle <= 105)
        {
            animationName = "HorizontalPunch";
            firePoint.position = new Vector3(0.15f, 0f, 0f);
        }
        else if (shootingAngle > 230 && shootingAngle <= 285)
        {
            animationName = "HorizontalPunch";
            firePoint.position = new Vector3(-0.15f, 0f, 0f);
        }
        // 130 - 230: Flying kick
        else if (shootingAngle > 130 && shootingAngle <= 180)
        {
            animationName = "FlyingKick";
            firePoint.position = new Vector3(0.15f, -0.18f, 0f);
        }
        else if (shootingAngle > 180 && shootingAngle <= 230)
        {
            animationName = "FlyingKick";
            firePoint.position = new Vector3(-0.15f, -0.18f, 0f);
        }
        // 335 - 25: Vertical Kick
        else
        {
            animationName = "VerticalKick";
            firePoint.position = new Vector3(0f, 0.18f, 0f);
        }
       
    }
}
