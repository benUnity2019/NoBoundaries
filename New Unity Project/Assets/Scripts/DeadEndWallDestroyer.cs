using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEndWallDestroyer : MonoBehaviour
{
    bool isTouchingRoom = false;

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        yield return new WaitForSeconds(2f);

        if (collision.gameObject.tag == "Door")
        {
            Destroy(collision.gameObject);

        }
        
        else if(collision.gameObject.tag == "Floor")
        {
            
            isTouchingRoom = true;
           
        }

        if (!isTouchingRoom)
        {
           
            Destroy(gameObject);
        }
    }
     
}
