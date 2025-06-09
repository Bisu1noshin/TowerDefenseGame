using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShotController : MonoBehaviour
{
    // インスペクター参照不可
    protected Vector3   moveVec; // 目的の座標
    protected int       atp;     // 弾の威力
    protected float     chargep; // チャージした時の値

    // 限界値
    protected const float maxChargep = 10;

    private void Start()
    {
        // マウスの座標を取得
        Vector3 mouseVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVec.z = 0;

        // マウスの座標を移動ベクトルに変換
        Vector3 v = mouseVec - transform.position;
        float r = Mathf.Atan2(v.y, v.x);
        moveVec = new Vector3(Mathf.Cos(r), Mathf.Sin(r));

        // 弾のためた値を取得
        chargep = PlayerChargeValue();
    }

    private void Update() {

        // 移動処理
        ShotUpData();

        // 画面外処理
        OutShot();
    }

    // 接触判定
    private void OnTriggerEnter2D(Collider2D collision) {

        // BaseUnit=b_;
        //if (b_ = collision.GetComponent<BaseUnit>()) { 
        //    Vector2 v=new(moveVec.x,moveVec.y);
        //    b_.Hit(v,atp);
        //    Destroy(gameObject);
        //}

        Destroy(gameObject);
    }

    // 画面外処理
    private void OutShot() {

        float r = 15f;                  // 画面外の半径
        float x = transform.position.x; // x座標
        float y = transform.position.y; // y座標

        // 画面外に出たらオブジェクトを破壊
        if (Mathf.Sqrt(x * x + y * y) >= r) {
            Destroy(gameObject);
        }
    }

    // playerのため時間を取得

    private float PlayerChargeValue()
    {
        float chargeValue = 1f;
        
        // ためた値を取得(未実装)
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

    // 抽象メソッド

    protected abstract void SetShotAtp();

    protected abstract void ShotUpData();

    // 参照可能メソッド

}
