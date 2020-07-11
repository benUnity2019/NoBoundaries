using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms; //array of rooms with a bottom hallway and door
    public GameObject[] topRooms;   //array of rooms with a top hallway and door
    public GameObject[] leftRooms;//array of rooms with a left hallway and door
    public GameObject[] rightRooms;//array of rooms with a right hallway and door
    public GameObject closedRooms; //self correcting gameoject if arrays fail to find correct room
    public GameObject Grid;
    public GameObject[] spawnpoints; // array of each door in scene


    void Update()
    {
        spawnpoints = GameObject.FindGameObjectsWithTag("Door");   //updating spawnpoints each frame for each door in the scene
    }
}
