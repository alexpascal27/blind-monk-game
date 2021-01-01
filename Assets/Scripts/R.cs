using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class R : MonoBehaviour
{
    public Texture rIcon;
    public Texture rActiveIcon;
    public Texture rGreyIcon;
    
    private float cooldownTime;
    private float activeTime;
    private float nextFireTime = 0f;
    private bool currentlyUlting  = false;
    private bool onCooldown = false;
    
    public Light2D innerLight;
    private Color initialColor;

    private void Start()
    {
        initialColor = innerLight.color;
        cooldownTime = PlayerPrefs.GetFloat("RCooldown");
        activeTime = PlayerPrefs.GetFloat("RActiveTime");
    }

    // Update is called once per frame
    void Update()
    {
        // If our ability is currently not on cooldown
        if (nextFireTime < Time.time)
        {
            onCooldown = false;
            // If we pressed E
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Start cooldown
                nextFireTime = Time.time + cooldownTime + activeTime;
                onCooldown = true;
            }
        }
        else
        {
            // If active not yet on cooldown
            if (nextFireTime - cooldownTime > Time.time)
            {
                if (!currentlyUlting)
                {
                    // Positive ulting logic
                    
                    
                    innerLight.color = Color.red;
                    
                    currentlyUlting = true;
                }
            }
            // If no longer active but on cooldown
            else if (nextFireTime - activeTime > Time.time)
            {
                if (currentlyUlting)
                {
                    // Negative ulting logic
                    
                    innerLight.color = initialColor;
                    
                    currentlyUlting = false;
                }
                
            }
        }
    }
    
    void OnGUI()
    {
        if (currentlyUlting)
        {
            GUI.Label(new Rect(990,55,100,100), rActiveIcon); 
        }
        else if (!onCooldown)
        {
            GUI.Label(new Rect(990,55,100,100), rIcon); 
        }
        else
        {
            GUI.Label(new Rect(990,55,100,100), rGreyIcon); 
        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        // If colliding with an enemy and r is not on cooldown
        if (other.gameObject.tag == "Enemy" && !currentlyUlting)
        {
            SceneManager.LoadScene(2);
        }
    }
}
