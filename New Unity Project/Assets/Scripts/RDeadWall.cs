using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RDeadWall : MonoBehaviour
{
    public GameObject leftDeadEnd;
   
    bool hasSpawned = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("L"))
        {
       
            hasSpawned =true;
            
        }
        else if(hasSpawned == false && collision.gameObject.tag != "Player")
        {
          
           Instantiate(leftDeadEnd, transform.position, leftDeadEnd.transform.rotation);
            hasSpawned = true;
       
          
        }

        if (collision.gameObject.name.Contains("ClosedDoor"))
        {

            Destroy(collision.gameObject);

        }







    }
}
