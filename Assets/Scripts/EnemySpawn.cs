using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Rigidbody2D rb2D;
    // Range for spawn area
    float maxSpawnY;
    float minSpawnY;
    float maxSpawnX;
    float minSpawnX;

    // ScreenBounds
    private Vector2 screenBounds;

    // Range for enemy size
    private float minScale;
    private float maxScale;
    
    // Range for enemy speed
    private float minSpeed;
    private float maxSpeed;

    // Instance of GameObject
    public GameObject enemyPrefab;

    public void DoTheSpawn(int enemiesToSpawn)
    {
        SetSpawn();
        SpawnAtRandom(GetAvailableSlots(), enemiesToSpawn);
    }

    void Start()
    {
        minScale = PlayerPrefs.GetFloat("minEnemyScale");
        maxScale = PlayerPrefs.GetFloat("maxEnemyScale");
        
        minSpeed = PlayerPrefs.GetFloat("minEnemySpeed");
        maxSpeed = PlayerPrefs.GetFloat("maxEnemySpeed");
        float velocity = GetVelocity();

        float randomX = Random.Range(-2, 2);
        float randomY = Random.Range(-2, 2);
        do 
        {
            if(randomX == 0) randomX = Random.Range(-2, 2);
            if(randomY == 0) randomY = Random.Range(-2, 2);
        }while(randomX==0 && randomY==0);
        Vector2 direction = new Vector2(randomX, randomY);

        // Max scale - scale * 5
        rb2D.velocity = direction.normalized * (velocity);
    }

    private float GetVelocity()
    {
        float scale = gameObject.transform.localScale.x;
        // Work out scale relative to min max bounds
        float percentange = scale - 1;

        return (maxSpeed - minSpeed) * percentange + minSpeed;
    }

    void SetSpawn()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    List<float[]> GetAvailableSlots()
    {
        minSpawnY = 0;
        maxSpawnY = screenBounds.y;
        minSpawnX = -screenBounds.x;
        maxSpawnX = screenBounds.x;

        float ySpan = maxSpawnY - minSpawnY;
        float xSpan = maxSpawnX - minSpawnX;

        List<float[]> slotCoordinates = new List<float[]>();
        for(int row = (int)(maxScale); row < (int)(ySpan); row+=(int)(maxScale))
        {
            for(int column = (int)(maxScale); column < (int)(xSpan); column+=(int)(maxScale))
            {
                // Work out x coordinate
                float x =  minSpawnX + column;
                // Work out y coordinate
                float y =  minSpawnY + row;
                // Storing the x and y in a coordinate array
                float[] coordinate = {x, y};
                slotCoordinates.Add(coordinate);
            }
            
        }
        return slotCoordinates;
    }

    void SpawnAtRandom(List<float[]> availableSlots, int enemiesToSpawn)
    {
        int availableSlotsNumber = availableSlots.Count;

        while(enemiesToSpawn > 0 && availableSlotsNumber>0)
        {
            int randomPosition = Random.Range(0, availableSlots.Count);
             if(availableSlots[randomPosition]!=null)
                {
                    // Spawn enemy at available slot
                    SpawnEnemy(availableSlots[randomPosition][0], availableSlots[randomPosition][1]);
                    // Make slot unavailable
                    availableSlots[randomPosition] = null;
                    // Reduce number of enemies needed to be spawned
                    enemiesToSpawn--;
                    availableSlotsNumber--;
                }
        }
    }

    private void SpawnEnemy(float x, float y)
    {
        GameObject enemy = Instantiate(enemyPrefab) as GameObject;
        enemy.transform.position = new Vector2(x, y);
        float randomSize = Random.Range(minScale, maxScale);
        enemy.transform.localScale = new Vector2(randomSize, randomSize);
        Debug.Log("Enemy Spawned");
    }
}
