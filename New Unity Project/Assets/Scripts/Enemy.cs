using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int waitTime = 3;
    public float speed = 0.1f;
    public Transform Player;
    public Rigidbody2D rb;
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        
        Player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);

        if (Player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1.985872f, 2.258929f, 1.985872f);          // Flips the enemy if player is on oppisite face
        }
        else if (Player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1.985872f, 2.258929f, 1.985872f);
        }
    }
}
