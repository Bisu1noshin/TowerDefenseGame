using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_2 : ShotController
{
    // インスペクター参照可
    [SerializeField] private int shotAtp;   // 弾の攻撃力
    [SerializeField] private float speed;   // 弾の速さ

    protected override void SetShotAtp()
    {
        // 少数点以下は切り捨て
        atp = shotAtp * (int)chargep;
    }

    protected override void ShotUpData()
    {
        transform.position += moveVec * speed * Time.deltaTime;
    }
}
