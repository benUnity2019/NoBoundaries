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

    Transform currentTarget = null;
    float squareReachMagnitude;

    float lastAttackTime = float.MinValue;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        squareReachMagnitude = characterController.CurrentWeapon.Reach * characterController.CurrentWeapon.Reach;
    }

    private void Update()
    {
        if (currentTarget)
        {
            Vector2 vec = transform.position - currentTarget.position;
            if (vec.sqrMagnitude <= squareReachMagnitude)
            {
                characterController.Aim(vec.normalized);

                if (Time.time - lastAttackTime > characterController.CurrentWeapon.Cooldown)
                {
                    lastAttackTime = Time.time;

                    characterController.UseWeapon();
                }
            }
        }
    }

    void FixedUpdate()
    {
        int amountOfCollidersISee = Physics2D.OverlapCircleNonAlloc(transform.position, viewDistance, collidersISee);

        //TODO determine which collider to attack via some decision making
        for (int i = 0; i < amountOfCollidersISee; ++i)
        {
            if (collidersISee[i].GetComponent<PlayerController>())
            {
                currentTarget = collidersISee[i].transform;
                move = (currentTarget.position - transform.position).normalized;
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