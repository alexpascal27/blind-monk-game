using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDeathScreen : MonoBehaviour
{
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI deathCountText;
    public TextMeshProUGUI timerText;
    
    // Start is called before the first frame update
    private void Start()
    {
        String difficulty = PlayerPrefs.GetString("Difficulty", "Casual");
        int deathCount = PlayerPrefs.GetInt("DeathCount", 0);
        float timer = PlayerPrefs.GetFloat("CurrentTime", 0f);

        difficultyText.text = "Difficulty: " + difficulty;
        deathCountText.text = "Deaths Count: " + deathCount.ToString();
        timerText.text = "Time: " + timer.ToString();
        

        PlayerPrefs.SetFloat("CurrentTime", 0f);
        PlayerPrefs.Save();
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    public void Quit()
    {
        PlayerPrefs.SetInt("DeathCount", 0);
        PlayerPrefs.Save();
        Application.Quit();
    }
}
