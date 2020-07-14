using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WaveSpawner : MonoBehaviour
{

    public enum Spawnstate { SPAWNING, WAITING, COUNTING};     //enum for the States of each wave being Serialzed
    [System.Serializable]
    public class Wave
    {
        public string name;                                   //name of the wave
        public Transform enemy;                               //Prefab of enemy to be spawned
        public int count;                                     //how many enemies will be spawned each round
        public float rate;                                    // rate(how fast) the enemies will spawn at
       
    }


    public GameObject[] spawnpoints;                          // Array of Doors in the game
    private GameObject currentDoor;                           // Object holding the door the game will choose to be a spawnpoint at random
    public Wave[] waves;                                      // Array of waves
    public int nextWave = 0;                                  // Counter for each wave 
    public float timeInBetweenWaves = 20f;                    // Wave Delay that can be changed or canceled by player
    public float waveCountDown;                               // Set equal to timeInBetweenWaves
    private Spawnstate state = Spawnstate.COUNTING;           // Counting state for each enemy spawned
    private float searchCountDown = 1f;                       // Searching rate to see if all enemies are dead 
    public Button EndDelay;                                   // UI Button when pressed sets waveCountDown to ==0
    int index = 0;                                            // Index for currentDoor to be = 
    public Text WaveTimer;                                    // UI Wave Delay counter
    public Text WhatWave;                                     // nextWave UI counter
    void Start()
    {
    StartCoroutine(WaitForMap());                             //Allows time for map to load in to spawn DeadEnd Walls Correctly
    currentDoor = GameObject.FindGameObjectWithTag("Enemy");  // object holding any gameobject tagged enemy to spawn at a index/door
    EndDelay.onClick.AddListener(EndWaveDelay);              // Button listener for EndDelay IEnumerator Below
    }

    void Update()
    {

    spawnpoints = GameObject.FindGameObjectsWithTag("Door");            // Array of gameobjects With tag door
    spawnpoints = spawnpoints.Where(x => x != null).ToArray();          // If array element is null is is removed
    spawnpoints = spawnpoints.Where(x => x == isActiveAndEnabled).ToArray();
        WhatWave.text = nextWave.ToString();                                     // Converts float nextWave to String and displays on UI
        if (state == Spawnstate.WAITING)                                   //State of waiting to check if enemy is alive or not to end the round
                { if (!EnemyIsAlive())
                    {
                        WaveCompleted();
                    }
                  else
                    {
                       return;
                    }

                }
        
        if(waveCountDown <= 0)                             //Checking if waveDelay == 0 and disabling the UI Element and to srt state to spawning
          {
            WaveTimer.enabled = false;
            if (state != Spawnstate.SPAWNING)
            {

                StartCoroutine(SpawnWave(waves[nextWave])); // if it is 0 it will run SpawnWave Below
            }

        }
        else
        {
            WaveTimer.enabled = true;                       //if waveDelay isnt 0 it will show on UI
            WaveTimer.text = waveCountDown.ToString();      // int waveCountDown to UI
            waveCountDown -= Time.deltaTime;                // correcting waveCountDown to match time
        }
    }

    void WaveCompleted()                                    //method declaring the wave is completed and to update UI elements and waveDelay
    {
        Debug.Log("Wave Completed");
        state = Spawnstate.COUNTING;
        waveCountDown = timeInBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("all waves completed");
        }
        nextWave++;

    }

    bool EnemyIsAlive()                                 //Check if any enemies are alive every 1 second and if its null return true, else false
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
                
            }
           
        }
        return true;
    }


     IEnumerator WaitForMap()                       // Simple wait time for Start function
    {
        yield return new WaitForSeconds(10f);
        
    }





    IEnumerator SpawnWave(Wave _wave)          //Sets state to spawning and runs through each wave and wave enemies and wave rate
    {
       
        state = Spawnstate.SPAWNING;

        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);

        }
        state = Spawnstate.WAITING;
        yield break;



    }

    void SpawnEnemy(Transform _enemy)                // Spawns enemies at current index of the random door chosen within the scene
    {
        index = Random.Range(0, spawnpoints.Length);
        currentDoor = spawnpoints[index];
        if (spawnpoints[index] == isActiveAndEnabled)
        {
       
            Instantiate(_enemy, currentDoor.transform.position , currentDoor.transform.rotation);
        }
        if (spawnpoints[index] == null)
        {
            spawnpoints = spawnpoints.Where(x => x != null).ToArray();

        }


    }

    void EndWaveDelay()                                     // simple method to end the waveDelay and start the round
    {
        waveCountDown = 0;

    }
}
