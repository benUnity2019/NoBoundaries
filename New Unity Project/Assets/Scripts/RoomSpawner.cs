using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public int openingDirection;
    //1 need bottom door
    //2 need top
    //3 left
    //4 right

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;
    private int spawnLimit = 0;
    public float waitTime = 4f;
    Vector3 rightroomCheck;


    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
       
    }

    




    void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {

                rand = Random.Range(0, templates.bottomRooms.Length);
                
                Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);
               


            }
            else if (openingDirection == 2)
            {
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);



            }
            else if (openingDirection == 3)
            {

                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);

            }
            else if (openingDirection == 4)
            {

                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);

            }
            spawned = true;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
             if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
             {
                 Instantiate(templates.closedRooms, transform.position, Quaternion.identity);
                 Destroy(gameObject);

             }
           spawned = true;
        }

     

    }
}
