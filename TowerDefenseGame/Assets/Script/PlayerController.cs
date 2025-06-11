using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput inputActions;
    float cnt_MouseTime = 0; //クリックしている時間をカウント
    [SerializeField] const float maxChargeTime = 5.0f; //最大チャージ時間(割合計算に使うためconst)
    [SerializeField] GameObject bulletPrefab;
    bool pushing = false; //マウスボタンがdown状態かのフラグ
    [SerializeField] const int maxHP = 50; //最大体力(割合計算に使うためconst)
    int nowHP = maxHP; //現在体力 初期化はmaxHPで
    [SerializeField] ParticleSystem Ef_Explosion; //爆発エフェクト

    private void Awake()
    {
        inputActions = new PlayerInput();

        //入力処理関数をバインド
        inputActions.Player.Shot.started += StartCharge;
        inputActions.Player.Shot.canceled += Shoot;

        inputActions.Enable();
    }

    private void Update()
    {
        if (pushing == true)
        {
            //チャージ
            cnt_MouseTime += Time.deltaTime;
            if( cnt_MouseTime > maxChargeTime ) { cnt_MouseTime=maxChargeTime; }
        }

        //HPが0になったら死亡
        if (nowHP <= 0)
        {
            Instantiate(Ef_Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        Debug.Log(nowHP);
    }

    //攻撃された時の処理
    public void Hit(int damage_)
    {
        nowHP -= damage_;
    }

    // 入力イベント

    //クリック開始処理
    public void StartCharge(InputAction.CallbackContext context)
    {
        pushing = true;
    }

    //クリック終了処理(離したとき)
    public void Shoot(InputAction.CallbackContext context)
    {
        //弾を生成
        Vector3 v=this.transform.position;
        Quaternion q=this.transform.rotation;
        GameObject shot = Instantiate(bulletPrefab, v, q);

        //チャージのリセット
        pushing = false;
        // cnt_MouseTime = 0;
    }

    //チャージ割合の取得
    public float GetCharge()
    {
        float chargeValue = cnt_MouseTime / maxChargeTime;
        cnt_MouseTime = 0;
        return chargeValue;
    }
    
    //HP割合の取得
    public float GetHP()
    {
        return nowHP / maxHP;
    }
}
