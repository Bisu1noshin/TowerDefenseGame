using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleSceneManager : MonoBehaviour
{
    PlayerInput inputActions;

    // �C���X�y�N�^�[�Q�Ɖ\�ϐ�

    // �C���X�y�N�^�[�Q�ƕs�ϐ�
    private Vector3 moveVec;

    private void Awake(){
        inputActions = new PlayerInput();

        // ���̓C�x���g���o�C���h
        inputActions.Title.YesButton.started += OnYes;
        inputActions.Title.NonButton.started += OnNon;

        inputActions.Enable();
    }

    private void Start(){

    }

    private void Update(){

        // �}�E�X�̈ʒu�ɍ��W���ړ�
        transform.position = GetMousePosition();


    }

    public void OnYes(InputAction.CallbackContext context) { 


    }

    public void OnNon(InputAction.CallbackContext context) { 


    }

    private Vector3 GetMousePosition(){

        // �}�E�X�̍��W���擾
        Vector3 mouseVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVec.z = 0;

        return mouseVec;
    }
}
