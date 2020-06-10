using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AIController : MonoBehaviour
{
    [Header("References")]
    CharacterController characterController;

    [Header("References")]
    [SerializeField] float viewDistance;

    Vector2 move = Vector2.zero;

    Collider2D[] collidersISee = new Collider2D[20];

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        int amountOfCollidersISee = Physics2D.OverlapCircleNonAlloc(transform.position, viewDistance, collidersISee);

        //TODO determine which collider to attack via some decision making
        for (int i = 0; i < amountOfCollidersISee; ++i)
        {
            if (collidersISee[i].GetComponent<PlayerController>())
            {
                move = (collidersISee[i].transform.position - transform.position).normalized;
                break;
            }
        }

        characterController.Move(move, Time.fixedDeltaTime);
    }

    void OnDead(float health)
    {
        Destroy(gameObject);
    }
}