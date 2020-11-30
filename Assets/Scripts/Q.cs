using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q : MonoBehaviour
{
    public Transform horizontalFirePoint;
    public Transform verticalFirePoint;

    public Animator animator;
    public GameObject bulletPrefab;
    public Camera camera;
    public GameObject horizontalLine;
    public GameObject verticalLine;

    public float animationTime;
    private float animationTimeLeft = 0f;
    private bool shooting = false;

    private bool horizontal = true;

    void Start()
    {
        verticalLine.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Q"))
        {
            if (horizontal) HorizontalShoot();
            else VerticalShoot();

        }

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
                animator.SetBool("HorizontalShooting", false);
                animator.SetBool("VerticalShooting", false);
                
                shooting = false;
            }
        }
        
    }

    void RotateLineAccordingToMouse()
    {
        Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);

        // Compare the player and mouse position
        // If mouse position is above player position
        if (mousePosition.y > (gameObject.transform.position.y + 1))
        {
            if (horizontal)
            {
                horizontal = false;
                horizontalLine.SetActive(false);
                verticalLine.SetActive(true);
            }
        }
        else
        {
            if (!horizontal)
            {
              horizontal = true;  
              horizontalLine.SetActive(true);
              verticalLine.SetActive(false);
            }
        }
    }

    void HorizontalShoot()
    {
        // Instantiate bullet
        StartCoroutine(HorizontalShootingCoroutine());

        animator.SetBool("HorizontalShooting", true);
        animationTimeLeft = animationTime;
        shooting = true;
    }

    IEnumerator HorizontalShootingCoroutine()
    {
        yield return new WaitForSeconds(animationTime/2);
        Instantiate(bulletPrefab, horizontalFirePoint.position, horizontalFirePoint.rotation);
    }

    void VerticalShoot()
    {
        // Instantiate bullet
        StartCoroutine(VerticalShootingCoroutine());

        animator.SetBool("VerticalShooting", true);
        animationTimeLeft = animationTime;
        shooting = true;
    }
    
    IEnumerator VerticalShootingCoroutine()
    {
        yield return new WaitForSeconds(animationTime/2);
        
        // Change to shoot vertically
        Transform rotatedVerticalFirePoint = verticalFirePoint;
        rotatedVerticalFirePoint.Rotate(0f, 0f, 90f, Space.Self);
        Instantiate(bulletPrefab, verticalFirePoint.position, rotatedVerticalFirePoint.rotation);
        rotatedVerticalFirePoint.Rotate(0f, 0f, -90f, Space.Self);
    }
}
