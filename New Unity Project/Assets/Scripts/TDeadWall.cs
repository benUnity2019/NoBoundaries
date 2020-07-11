using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDeadWall : MonoBehaviour
{



    public GameObject BottomDeadEnd;
    bool deadEndWall = false;
    bool hasSpawned = false;
    bool hasBeenDestroyed = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("B"))
        {
          
            hasSpawned = true;
          
        }
     
        else if (hasSpawned == false && collision.gameObject.tag != "Player")
        {
            Instantiate(BottomDeadEnd, transform.position, BottomDeadEnd.transform.rotation);
            hasSpawned = true;
           

        }

        if (collision.gameObject.name.Contains("ClosedDoor"))
        {

            Destroy(collision.gameObject);

        }




    }
    
   
    
}

