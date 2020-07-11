using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRDooras : MonoBehaviour
{
    bool PlayerinRoom = false;
 

    void OnTriggerStay2D(Collider2D col)
    {
       
        if (col.gameObject.tag == "Player")
        {

            PlayerinRoom = true;
        }
       

        if (PlayerinRoom == true)

        {
       
            col.gameObject.SetActive(true);

        }


    }
    void OnTriggerExit2D(Collider2D collision)
    {
        PlayerinRoom = false;
    }

}
