using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BWallDestroyer : MonoBehaviour
{
    public Transform BottomW;
    public GameObject BottomWall;
    public float speed = 0.1f;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Bottom"))
        {
            Destroy(BottomWall);
        }
    }
  
}
