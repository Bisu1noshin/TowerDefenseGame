using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput inputActions;

    private void Awake()
    {
        inputActions = new PlayerInput();

        inputActions.Player.Move.started += OnFire;

        inputActions.Enable();
    }

    // 入力イベント

    public void OnFire(InputAction.CallbackContext context) { 


    }
}
