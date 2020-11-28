using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q : MonoBehaviour
{
    public Transform horizontalFirePoint;
    public Transform verticalFirePoint;

    public Animator animator;
    public GameObject bulletPrefab;

    public float animationTime;
    private float animationTimeLeft = 0f;
    private bool shooting = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Q"))
        {
            Shoot();
        }
        
        // Reduce animation time left
        if (animationTimeLeft > 0)
        {
            animationTimeLeft -= 0.01f;
        }
        else
        {
            if (shooting)
            {
              animator.SetBool("HorizontalShooting", false);
              shooting = false;
            }
        }
        
    }

    void Shoot()
    {
        // Instantiate bullet
        StartCoroutine(ShootingCoroutine());
        animator.SetBool("HorizontalShooting", true);
        animationTimeLeft = animationTime;
        shooting = true;
    }

    IEnumerator ShootingCoroutine()
    {
        yield return new WaitForSeconds(animationTime/2);
        Instantiate(bulletPrefab, horizontalFirePoint.position, horizontalFirePoint.rotation);
    }
}
