using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BWallDestroyer : MonoBehaviour
{
    public Transform BottomW;
    public GameObject BottomWall;
    public float speed = 0.1f;
    void OnCollisionEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bottom"))
        {
            Destroy(BottomWall);
        }
    }
  
}
