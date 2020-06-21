using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject x;
    public Transform Player;
    public float spawnrate = 10f;
    float nextSpawn = 1f;
    Vector2 whereToSpawn;
    float randx;
    public GameObject enemy;
    
    bool spawnCap = false;
    private int MaxSpawn = 0;
    public int waitTime = 3;
    void Start()
    {
        x = GameObject.FindWithTag("Enemy");
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    void Update()
    {
        if(Time.time > waitTime)
        if (!spawnCap && MaxSpawn != 2 )
        {
            
            whereToSpawn = new Vector2(transform.position.x, transform.position.y);  /*playerHealth.healthAmount > 0f*/
            Instantiate(enemy, whereToSpawn, Quaternion.identity);
            spawnCap = true;
            MaxSpawn++;
        }
      
    }

    
 }

        
  



