using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class Shot_3 : ShotController
{
    // �C���X�y�N�^�[�Q�Ɖ�
    [SerializeField] private int shotAtp;   // �e�̍U����
    [SerializeField] private float speed;   // �e�̑���

    private float g = 0;

    protected override void SetShotAtp()
    {
        // �����_�ȉ��͐؂�̂�
        atp = shotAtp * (int)chargep;
    }

    protected override void ShotUpData()
    {
        // �d�͉����x
        Vector3 vec = moveVec;
        g += -0.01f;

        // ������̗�
        vec.y += g;
        transform.position += vec * speed * Time.deltaTime * chargep;

    }
}
