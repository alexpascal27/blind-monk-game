using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDeathScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    public void Quit()
    {
        Application.Quit();
    }
}
