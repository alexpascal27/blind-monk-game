using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathNumberDisplay : MonoBehaviour
{
    public TextMeshProUGUI deathCountText;
    public TextMeshProUGUI timerText;
    private int deathCount;

    // Start is called before the first frame update
    void Start()
    {
        deathCount = PlayerPrefs.GetInt("DeathCount", 0);
        deathCountText.text = "Death Count: " + deathCount.ToString();
    }

    private void Update()
    {
        float currentTime = PlayerPrefs.GetFloat("CurrentTime", 0f);
        PlayerPrefs.SetFloat("CurrentTime", currentTime + Time.deltaTime);
        PlayerPrefs.Save();
        timerText.text = "Timer: " + (currentTime+Time.deltaTime).ToString();
    }
}
