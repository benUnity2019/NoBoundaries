using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    CharacterController characterController;
    [SerializeField] GameObject towerPrefab;

    Vector2 move = Vector2.zero;

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
    }

    void FixedUpdate()
    {
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");

        characterController.Move(move, Time.fixedDeltaTime);
    }

    void OnDead(float health)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}