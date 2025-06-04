using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput inputActions;
    float cnt_MouseTime = 0; //クリックしている時間をカウント
    [SerializeField] float maxChargeTime = 5.0f; //最大チャージ時間
    [SerializeField]GameObject bulletPrefab;
    bool pushing = false; //マウスボタンがdown状態かのフラグ

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
        cnt_MouseTime = 0;
    }

    public float GetCharge()
    {
        return cnt_MouseTime / maxChargeTime;
    }
    
}
