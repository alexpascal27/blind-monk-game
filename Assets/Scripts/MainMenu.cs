using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Enemy
    private const float EnemySpeed = 4;
    private const float EnemyScale = 1;

    private int[] numberOfEnemies = new int[4] {2, 2, 3, 3};
    private float[] minSpeedScales = new float[4] {1f, 1.25f, 1.5f, 2f};
    private float[] maxSpeedScales = new float[4] {1.5f, 1.75f, 2f, 3f};
    private float[] minScaleScales = new float[4] {1.5f, 1.25f, 1.25f, 1f};
    private float[] maxScaleScales = new float[4] {2f, 1.75f, 1.75f, 1.5f};
    
    // Player
    [Range(0, 30)][SerializeField]public const int QCooldown = 3;
    [Range(0, 30)][SerializeField]public const int LShiftCooldown = 5;
    [Range(0, 30)][SerializeField]public const int ECooldown = 5;
    [Range(0, 30)][SerializeField]public const int EActiveTime = 3;
    [Range(0, 30)][SerializeField]public const int RCooldown = 8;
    [Range(0, 30)][SerializeField]public const int RActiveTime = 2;
    
    private float[] playerCooldownScale = new float[4] {1f, 2f, 1.5f, 2f};
    
    public void Casual()
    {
        SetPlayerPrefs(0);
        // Move to game scene
        SceneManager.LoadScene(1);
    }
    
    public void Normal()
    {
        SetPlayerPrefs(1);
        // Move to game scene
        SceneManager.LoadScene(1);
    }
    
    public void RageMode()
    {
        SetPlayerPrefs(2);
        // Move to game scene
        SceneManager.LoadScene(1);
    }
    
    public void Impossible()
    {
        SetPlayerPrefs(3);
        // Move to game scene
        SceneManager.LoadScene(1);
    }

    private void SetPlayerPrefs(int difficulty)
    {
        // Enemy preferences
        PlayerPrefs.SetInt("numberOfEnemies", numberOfEnemies[difficulty]);
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
    }
}
