using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LShift : MonoBehaviour
{
    public Camera camera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            MovePlayerToMousePosition();
        }
    }
    
    private void MovePlayerToMousePosition()
    {
        // Get mouse position
        Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 screenBounds = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        // If out of the screen
        while(mousePosition.y > screenBounds.y - 0.5f)
        {
            mousePosition.y -= 0.5f;
        }
        while(mousePosition.x > screenBounds.x - 0.2f)
        {
            mousePosition.x -= 0.5f;
        }
        while(mousePosition.y < -screenBounds.y + 1f)
        {
            mousePosition.y += 0.5f;
        }
        while(mousePosition.x < -screenBounds.x + 0.2f)
        {
            mousePosition.x += 0.5f;
        }

        // Flash to mouse position
        gameObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    }
}
