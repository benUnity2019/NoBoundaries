using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("References")]
    new Rigidbody2D rigidbody;
    [SerializeField] GameObject projectilePrefab;

    [Header("Settings")]
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;

    [Header("Data")]
    Vector2 velocity = Vector2.zero;
    [SerializeField] Weapon currentWeapon;
    [SerializeField] Team team;

    public Team Team { get => team; }
    public Weapon CurrentWeapon { get => currentWeapon; set => currentWeapon = value; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// If |move| > 0, accelerates player to maxSpeed scaled by move
    /// else |move| == 0, deccelerates player to 0
    /// </summary>
    /// <param name="move"></param>
    /// <param name="deltaTime">Used for passing in either Time.deltaTime or Time.fixedDeltaTime</param>
    public void Move(Vector2 move, float deltaTime)
    {
        // If move.x == 0.0f, decelerate x movement
        if (Mathf.Approximately(move.x, 0.0f))
            velocity.x = Mathf.MoveTowards(velocity.x, 0.0f, deceleration * deltaTime);
        else // Else accelerate x movement towards maxSpeed
            velocity.x = Mathf.MoveTowards(velocity.x, move.x * maxSpeed, acceleration * deltaTime);

        // If move.y == 0.0f, decelerate x movement
        if (Mathf.Approximately(move.y, 0.0f))
            velocity.y = Mathf.MoveTowards(velocity.y, 0.0f, deceleration * deltaTime);
        else // Else accelerate x movement towards maxSpeed
            velocity.y = Mathf.MoveTowards(velocity.y, move.y * maxSpeed, acceleration * deltaTime);

        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
    }

    public void UseWeapon()
    {

    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
    }
}
