﻿using System.Collections;
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
    public float minSize;
    public float maxSize;
    
    // Range for enemy speed
    private float minSpeed;
    private float maxSpeed;

    // Instance of GameObject
    public GameObject enemyPrefab;
    
    // Instance of enemy stat manager
    public EnemyStats enemyStats;

    public void DoTheSpawn(int enemiesToSpawn)
    {
        SetSpawn();
        SpawnAtRandom(GetAvailableSlots(), enemiesToSpawn);
    }

    void Start()
    {
        if(enemyStats==null) Debug.Log("enemy stats script is null");

        minSpeed = enemyStats.GetMinEnemySpeed();
        Debug.Log("MinSpeed: " + minSpeed);
        maxSpeed = enemyStats.GetMaxEnemySpeed();
        Debug.Log("MaxSpeed: " + maxSpeed);
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
        rb2D.velocity = direction.normalized * (velocity * Mathf.Max((maxSize-gameObject.transform.localScale.x), 0.2f));
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
        minSpawnY = -(screenBounds.y / 2);
        maxSpawnY = screenBounds.y;
        minSpawnX = -screenBounds.x;
        maxSpawnX = screenBounds.x;

        float ySpan = maxSpawnY - minSpawnY;
        float xSpan = maxSpawnX - minSpawnX;

        List<float[]> slotCoordinates = new List<float[]>();
        for(int row = (int)(maxSize); row < (int)(ySpan); row+=(int)(maxSize))
        {
            for(int column = (int)(maxSize); column < (int)(xSpan); column+=(int)(maxSize))
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
        float randomSize = Random.Range(minSize, maxSize);
        enemy.transform.localScale = new Vector2(randomSize, randomSize);
        Debug.Log("Enemy Spawned");
    }
}
