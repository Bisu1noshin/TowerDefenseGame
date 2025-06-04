using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class Shot_3 : ShotController
{
    // インスペクター参照可
    [SerializeField] private int shotAtp;   // 弾の攻撃力
    [SerializeField] private float speed;   // 弾の速さ

    private float g = 0;

    protected override void SetShotAtp()
    {
        // 少数点以下は切り捨て
        atp = shotAtp * (int)chargep;
    }

    protected override void ShotUpData()
    {
        // 重力加速度
        Vector3 vec = moveVec;
        g += -0.01f;

        // 上方向の力
        vec.y += g;
        transform.position += vec * speed * Time.deltaTime * chargep;

    }
}
