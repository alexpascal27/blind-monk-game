using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveToNextLeverLevel : MonoBehaviour
{
    private Vector2 screenBounds;
    public EnemySpawn m_enemySpawnScript;

    private int enemiesLeftToSpawn;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        enemiesLeftToSpawn = PlayerPrefs.GetInt("numberOfEnemies");
    }

    public int Execute(int level)
    {
        // Only three levels
        if (level < 2)
        {
            // Work out how many enemies need to be spawned
            int enemiesToSpawn;
            Debug.Log("level: " + level);
            if (level == 0) enemiesToSpawn = enemiesLeftToSpawn / 2;
            else enemiesToSpawn = enemiesLeftToSpawn;
            Debug.Log("Need to spawn: " + enemiesToSpawn);
            
            // Spawn more enemies
            m_enemySpawnScript.DoTheSpawn(enemiesToSpawn);

            enemiesLeftToSpawn -= enemiesToSpawn;
            
            // Respawn the lever
            RePosition();
            return level+1;
        }
        else
        {
            // Win logic
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
