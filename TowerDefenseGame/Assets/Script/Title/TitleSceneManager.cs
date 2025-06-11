using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class TitleSceneManager : MonoBehaviour
{
    PlayerInput inputActions;

    // インスペクター参照可能変数

    // インスペクター参照不可変数
    private Vector3 moveVec;
    private bool OnMauseLeft;

    private void Awake(){

        inputActions = new PlayerInput();

        // 入力イベントをバインド
        {
            inputActions.Title.YesButton.started += OnYes;
            inputActions.Title.YesButton.canceled += ExitYes;
            inputActions.Title.NonButton.started += OnNon;
            inputActions.Title.NonButton.canceled += ExitNon;
        }

        // インプットシステムを有効化
        inputActions.Enable();
    }

    public void OnYes(InputAction.CallbackContext context) {

        PlayerCnt++;
    }

    public void ExitYes(InputAction.CallbackContext context) {

        
    }

    public void OnNon(InputAction.CallbackContext context) { 


    }

    public void ExitNon(InputAction.CallbackContext context) {


    }

    private Vector3 GetMousePosition(){

        // マウスの座標を取得
        Vector3 mouseVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVec.z = 0;

        return mouseVec;
    }
}
