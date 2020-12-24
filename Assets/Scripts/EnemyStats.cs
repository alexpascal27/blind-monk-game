using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private const float EnemySpeed = 5;
    private const float EnemyScale = 1;
    
    [Range(0, 3)][SerializeField]public int difficulty = 0;

    private int numberOfEnemies;
    public int numberOfEnemiesLeftToSpawn;
    
    private float minEnemySpeed;

    public int NumberOfEnemies
    {
        get => numberOfEnemies;
        set => numberOfEnemies = value;
    }

    public float MINEnemySpeed
    {
        get => minEnemySpeed;
        set => minEnemySpeed = value;
    }

    public float MAXEnemySpeed
    {
        get => maxEnemySpeed;
        set => maxEnemySpeed = value;
    }

    public float MINEnemyScale
    {
        get => minEnemyScale;
        set => minEnemyScale = value;
    }

    public float MAXEnemyScale
    {
        get => maxEnemyScale;
        set => maxEnemyScale = value;
    }

    private float maxEnemySpeed;

    private float minEnemyScale;
    private float maxEnemyScale;


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

        numberOfEnemiesLeftToSpawn = numberOfEnemies;
    }
}
