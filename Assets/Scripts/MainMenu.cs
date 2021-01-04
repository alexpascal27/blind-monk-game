using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Game
    private String[] difficultyStrings = new String[4] {"Casual", "Normal", "RAGE MODE", "impossible"};
    
    // Enemy
    private const float EnemySpeed = 3f;
    private const float EnemyScale = 1.25f;

    private float[] minSpeedScales = new float[4] {1f, 1.5f, 2f, 2.5f};
    private float[] maxSpeedScales = new float[4] {1.5f, 2.5f, 2.5f, 3f};
    private float[] minScaleScales = new float[4] {1.4f, 1.25f, 1.25f, 1f};
    private float[] maxScaleScales = new float[4] {1.5f, 1.5f, 1.5f, 1.25f};

    private int[] enemyNumber = new int[4] {1, 1, 2, 2};
    
    // Player
    [Range(0, 30)][SerializeField]public const int QCooldown = 2;
    [Range(0, 30)][SerializeField]public const int LShiftCooldown = 4;
    [Range(0, 30)][SerializeField]public const int ECooldown = 4;
    [Range(0, 30)][SerializeField]public const int EActiveTime = 3;
    [Range(0, 30)][SerializeField]public const int RCooldown = 6;
    [Range(0, 30)][SerializeField]public const int RActiveTime = 2;
    
    private float[] playerCooldownScale = new float[4] {1f, 1f, 1f, 1.5f};
    
    public void Casual()
    {
        SetPlayerPrefs(0);
        // Move to game scene
        SceneManager.LoadScene(4);
    }
    
    public void Normal()
    {
        SetPlayerPrefs(1);
        // Move to game scene
        SceneManager.LoadScene(4);
    }
    
    public void RageMode()
    {
        SetPlayerPrefs(2);
        // Move to game scene
        SceneManager.LoadScene(4);
    }
    
    public void Impossible()
    {
        SetPlayerPrefs(3);
        // Move to game scene
        SceneManager.LoadScene(4);
    }

    private void SetPlayerPrefs(int difficulty)
    {
        //Game preferences
        CompareDifficulties(difficultyStrings[difficulty]);
        // Enemy preferences
        PlayerPrefs.SetInt("numberOfEnemies", enemyNumber[difficulty]);
        PlayerPrefs.SetFloat("minEnemySpeed", EnemySpeed * minSpeedScales[difficulty]);
        PlayerPrefs.SetFloat("maxEnemySpeed", EnemySpeed * maxSpeedScales[difficulty]);
        PlayerPrefs.SetFloat("minEnemyScale", EnemyScale * minScaleScales[difficulty]);
        PlayerPrefs.SetFloat("maxEnemyScale", EnemyScale * maxScaleScales[difficulty]);
        // Player preferences
        PlayerPrefs.SetFloat("QCooldown", QCooldown * playerCooldownScale[difficulty]);
        PlayerPrefs.SetFloat("LShiftCooldown", LShiftCooldown * playerCooldownScale[difficulty]);
        PlayerPrefs.SetFloat("ECooldown", ECooldown * playerCooldownScale[difficulty]);
        PlayerPrefs.SetFloat("EActiveTime", EActiveTime);
        PlayerPrefs.SetFloat("RCooldown", RCooldown * playerCooldownScale[difficulty]);
        PlayerPrefs.SetFloat("RActiveTime", RActiveTime);
        
        PlayerPrefs.Save();
    }

    void CompareDifficulties(String difficulty)
    {
        String oldDifficulty = PlayerPrefs.GetString("Difficulty", difficulty);
        // If difficulty is changed then reset death count
        if (!oldDifficulty.Equals(difficulty))
        {
            PlayerPrefs.SetInt("DeathCount", 0);
            
        }
        PlayerPrefs.SetString("Difficulty", difficulty);
        PlayerPrefs.Save();
    }
}
