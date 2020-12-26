using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveToNextLeverLevel : MonoBehaviour
{
    private Vector2 screenBounds;
    
    public EnemyStats enemyStats;
    public EnemySpawn m_enemySpawnScript;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    public int Execute(int level)
    {
        // Only three levels
        if (level < 3)
        {
            // Work out how many enemies need to be spawned
            int enemiesToSpawn = Mathf.FloorToInt(enemyStats.GetNumberOfEnemies() / 2);
            
            // Spawn more enemies
            m_enemySpawnScript.DoTheSpawn(enemiesToSpawn);
            
            // Respawn the lever
            RePosition();
            return level+1;
        }
        else
        {
            // Win logic
            Debug.Log("You Win!");
            return -1;
        }
    }
    
    void RePosition()
    {
        float minSpawnX = -screenBounds.x + 1;
        float maxSpawnX = screenBounds.x - 1;

        float x = Random.Range(minSpawnX, maxSpawnX);
        
        gameObject.transform.position = new Vector2(x, gameObject.transform.position.y);
    }
}
