using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_1 : ShotController
{
    // インスペクター参照可
    [SerializeField] private int shotAtp;   // 弾の攻撃力
    [SerializeField] private float speed;   // 弾の速さ

    protected override void SetShotAtp()
    {
        atp = shotAtp;
    }

    protected override void ShotUpData()
    {
        transform.position += moveVec * speed * Time.deltaTime;
    }
}
