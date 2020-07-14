using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;                                //Dud Script
    public GameObject Player1;

     void Start()
    {

    }
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
        }


        transform.position = pos;
      
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.name.Contains("TopDoor"))
            Destroy(collision.gameObject);
        if (collision.gameObject.name.Contains("RightDoor"))
            Destroy(collision.gameObject);

        if (collision.gameObject.tag == "Enemy")
            Destroy(collision.gameObject);

       
    }
   



}

