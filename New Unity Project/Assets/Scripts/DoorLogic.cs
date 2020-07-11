using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    bool PlayerinRoom = false;

 
    IEnumerator OnTriggerStay2D(Collider2D col)
    {
        yield return new WaitForSeconds(11f);
        if (col.gameObject.tag == "Player" && !PlayerinRoom)
        {
            
            PlayerinRoom = true;
        }
      

        if(PlayerinRoom) 

        {
           
            col.gameObject.SetActive(true);

        }


    }
    void OnTriggerExit2D(Collider2D collision)
    {
        PlayerinRoom = false; 
    }


}
