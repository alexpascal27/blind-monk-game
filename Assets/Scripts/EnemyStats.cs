using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private const float EnemySpeed = 5;
    private const float EnemyScale = 1;
    
    [Range(0, 3)][SerializeField]public int difficulty = 0;

    private int numberOfEnemies;

    private float minEnemySpeed;
    private float maxEnemySpeed;

    private float minEnemyScale;
    private float maxEnemyScale;

    public int GetNumberOfEnemies()
    {
        return numberOfEnemies;
    }
    
    public float GetMinEnemySpeed()
    {
        return minEnemySpeed;
    }
    public float GetMaxEnemySpeed()
    {
        return maxEnemySpeed;
    }
    
    public float GetMinEnemyScale()
    {
        return minEnemyScale;
    }
    public float GetMaxEnemyScale()
    {
        return maxEnemyScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (difficulty)
        {
            // Casual
            case(0):
                numberOfEnemies = 3;
                minEnemySpeed = 0.5f * EnemySpeed;
                maxEnemySpeed = 1f * EnemySpeed;
                minEnemyScale = 1.5f * EnemyScale;
                maxEnemyScale = 2f * EnemyScale;
                break;
            // Normal
            case(1):
                numberOfEnemies = 3;
                minEnemySpeed = 1f * EnemySpeed;
                maxEnemySpeed = 1.5f * EnemySpeed;
                minEnemyScale = 1f * EnemyScale;
                maxEnemyScale = 1.5f * EnemyScale;
                break;
            // Rage mode
            case(2):
                numberOfEnemies = 5;
                minEnemySpeed = 1f * EnemySpeed;
                maxEnemySpeed = 1.5f * EnemySpeed;
                minEnemyScale = 1f * EnemyScale;
                maxEnemyScale = 1.5f * EnemyScale;
                break;
            // Impossible
            case(3):
                numberOfEnemies = 5;
                minEnemySpeed = 1.5f * EnemySpeed;
                maxEnemySpeed = 2f * EnemySpeed;
                minEnemyScale = 0.5f * EnemyScale;
                maxEnemyScale = 1f * EnemyScale;
                break;
        }
    }
}
