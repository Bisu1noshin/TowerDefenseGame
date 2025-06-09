using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleSceneManager : MonoBehaviour
{
    PlayerInput inputActions;

    // インスペクター参照可能変数

    // インスペクター参照不可変数
    private Vector3 moveVec;

    private void Awake(){
        inputActions = new PlayerInput();

        // 入力イベントをバインド
        inputActions.Title.YesButton.started += OnYes;
        inputActions.Title.NonButton.started += OnNon;

        inputActions.Enable();
    }

    private void Start(){

    }

    private void Update(){

        // マウスの位置に座標を移動
        transform.position = GetMousePosition();


    }

    public void OnYes(InputAction.CallbackContext context) { 


    }

    public void OnNon(InputAction.CallbackContext context) { 


    }

    private Vector3 GetMousePosition(){

        // マウスの座標を取得
        Vector3 mouseVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVec.z = 0;

        return mouseVec;
    }
}
