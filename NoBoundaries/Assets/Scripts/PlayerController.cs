using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    CharacterController characterController;
    [SerializeField] GameObject towerPrefab;
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
            GameObject newTower = Instantiate(towerPrefab);
            newTower.transform.position = transform.position;
        }

        money.Value = Mathf.FloorToInt(Time.time);

        if (Input.GetAxis("Attack") > 0.2f)
        {
            characterController.UseWeapon();
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