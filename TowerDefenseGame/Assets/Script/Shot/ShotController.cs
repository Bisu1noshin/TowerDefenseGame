using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShotController : MonoBehaviour
{
    // �C���X�y�N�^�[�Q�ƕs��
    protected Vector3   moveVec; // �ړI�̍��W
    protected int       atp;     // �e�̈З�
    protected float     chargep; // �`���[�W�������̒l

    // ���E�l
    protected const float maxChargep = 10;

    private void Start()
    {
        // �}�E�X�̍��W���擾
        Vector3 mouseVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVec.z = 0;

        // �}�E�X�̍��W���ړ��x�N�g���ɕϊ�
        Vector3 v = mouseVec - transform.position;
        float r = Mathf.Atan2(v.y, v.x);
        moveVec = new Vector3(Mathf.Cos(r), Mathf.Sin(r));

        // �e�̂��߂��l���擾
        chargep = PlayerChargeValue();
    }

    private void Update() {

        // �ړ�����
        ShotUpData();

        // ��ʊO����
        OutShot();
    }

    // �ڐG����
    private void OnTriggerEnter2D(Collider2D collision) {

        // BaseUnit=b_;
        //if (b_ = collision.GetComponent<BaseUnit>()) { 
        //    Vector2 v=new(moveVec.x,moveVec.y);
        //    b_.Hit(v,atp);
        //    Destroy(gameObject);
        //}

        Destroy(gameObject);
    }

    // ��ʊO����
    private void OutShot() {

        float r = 15f;                  // ��ʊO�̔��a
        float x = transform.position.x; // x���W
        float y = transform.position.y; // y���W

        // ��ʊO�ɏo����I�u�W�F�N�g��j��
        if (Mathf.Sqrt(x * x + y * y) >= r) {
            Destroy(gameObject);
        }
    }

    // player�̂��ߎ��Ԃ��擾

    private float PlayerChargeValue()
    {
        float chargeValue = 1f;
        
        // ���߂��l���擾(������)
        if (GameObject.Find("Player").TryGetComponent<PlayerController>(out var p_)) {

            chargeValue = p_.GetCharge() * 5;

            if (chargeValue >= maxChargep)
            {
                chargeValue = maxChargep;
            }
        }

        Debug.Log("chargeValue : " + chargeValue);
        return chargeValue;
    }

    // ���ۃ��\�b�h

    protected abstract void SetShotAtp();

    protected abstract void ShotUpData();

    // �Q�Ɖ\���\�b�h

}
