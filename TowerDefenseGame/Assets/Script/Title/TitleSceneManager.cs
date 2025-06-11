using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class TitleSceneManager : MonoBehaviour
{
    PlayerInput inputActions;

    // �C���X�y�N�^�[�Q�Ɖ\�ϐ�

    // �C���X�y�N�^�[�Q�ƕs�ϐ�
    private Vector3 moveVec;
    private bool OnMauseLeft;

    private void Awake(){

        inputActions = new PlayerInput();

        // ���̓C�x���g���o�C���h
        {
            inputActions.Title.YesButton.started += OnYes;
            inputActions.Title.YesButton.canceled += ExitYes;
            inputActions.Title.NonButton.started += OnNon;
            inputActions.Title.NonButton.canceled += ExitNon;
        }

        // �C���v�b�g�V�X�e����L����
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

        // �}�E�X�̍��W���擾
        Vector3 mouseVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVec.z = 0;

        return mouseVec;
    }
}
