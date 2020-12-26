using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private const float EnemySpeed = 4;
    private const float EnemyScale = 1;
    
    [Range(0, 3)][SerializeField]public int difficulty = 0;

    private int[] numberOfEnemies = new int[4] {3, 3, 5, 5};
    
    private float[] minSpeedScales = new float[4] {0.5f, 1f, 1f, 1.5f};
    private float[] maxSpeedScales = new float[4] {1f, 1.5f, 1.5f, 2f};
    private float[] minScaleScales = new float[4] {1.5f, 1.25f, 1.25f, 1f};
    private float[] maxScaleScales = new float[4] {2f, 1.75f, 1.75f, 1.5f};

    public int GetNumberOfEnemies()
    {
        return numberOfEnemies[difficulty];
    }
    
    public float GetMinEnemySpeed()
    {
        return EnemySpeed * minSpeedScales[difficulty];
    }
    public float GetMaxEnemySpeed()
    {
        return EnemySpeed * maxSpeedScales[difficulty];
    }
    
    public float GetMinEnemyScale()
    {
        return EnemyScale * minScaleScales[difficulty];
    }
    public float GetMaxEnemyScale()
    {
        return EnemyScale * maxScaleScales[difficulty];
    }
}
