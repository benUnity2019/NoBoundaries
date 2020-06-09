using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    CharacterController characterController;

    Vector2 move = Vector2.zero;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
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