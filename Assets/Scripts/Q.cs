using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q : MonoBehaviour
{
    public Transform horizontalFirePoint;
    public Transform verticalFirePoint;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Q"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        
    }
}
