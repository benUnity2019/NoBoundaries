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

    //Placeholder
    [SerializeField] AIController aiPrefab;

    Vector2 move = Vector2.zero;
    Vector2 aim = Vector2.zero;

    [SerializeField] Weapon[] towers;
    [SerializeField, ReadOnly] int currentTower = 0;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Placeholder
        if (Input.GetKeyDown(KeyCode.P))
        {
            AIController newAI = Instantiate(aiPrefab);
            newAI.transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        }

        if (Input.GetButtonDown("PlaceTower"))
        {
            Tower newTower = Instantiate(towerPrefab);
            newTower.transform.position = characterController.InteractPosition;
            newTower.Init(gameObject, towers[currentTower]);
        }

        if (Input.GetAxis("Attack") > 0.2f)
        {
            characterController.UseWeapon();
        }

        int towerScroll = (Input.GetButtonDown("TowerScrollRight") ? 1 : 0) - (Input.GetButtonDown("TowerScrollLeft") ? 1 : 0);
        currentTower += towerScroll;
        if (currentTower > 0)
            currentTower %= towers.Length;
        else if (currentTower < 0)
            currentTower = towers.Length - (-currentTower % towers.Length);

        for (int i = 0; i < towers.Length; ++i)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                currentTower = i;
                break;
            }
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