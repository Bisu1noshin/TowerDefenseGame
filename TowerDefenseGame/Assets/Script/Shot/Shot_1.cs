using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_1 : ShotController
{
    // �C���X�y�N�^�[�Q�Ɖ�
    [SerializeField] private int shotAtp;   // �e�̍U����
    [SerializeField] private float speed;   // �e�̑���

    protected override void SetShotAtp()
    {
        atp = shotAtp;
    }

    protected override void ShotUpData()
    {
        transform.position += moveVec * speed * Time.deltaTime;
    }
}
