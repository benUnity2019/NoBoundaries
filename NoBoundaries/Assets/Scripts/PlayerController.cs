using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    CharacterController characterController;
    [SerializeField] Tower towerPrefab;
    [SerializeField] UICounter money;

    Vector2 move = Vector2.zero;
    Vector2 aim = Vector2.zero;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("PlaceTower"))
        {
            Tower newTower = Instantiate(towerPrefab);
            newTower.transform.position = transform.position;
            newTower.Init(gameObject);
        }

        if (Input.GetAxis("Attack") > 0.2f)
        {
            characterController.UseWeapon();
        }
    }

    public void OnMeHurtingSomeoneElse(DamageEventData damageEvent)
    {
        if (damageEvent.victimsResultingHealth <= 0.0f)
        {
            money.Value += 5;
        }
    }

    void FixedUpdate()
    {
        //Movement
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");

        characterController.Move(move.normalized, Time.fixedDeltaTime);

        //Aim
        aim.x = Input.GetAxis("AimHorizontal");
        aim.y = Input.GetAxis("AimVertical");

        characterController.Aim(aim.normalized);
    }

    void OnDead(float health)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}