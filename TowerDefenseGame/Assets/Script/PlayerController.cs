using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    PlayerInput inputActions;
    public static PlayerController PlayerInstance;

    float cnt_MouseTime = 0; //クリックしている時間をカウント
    [SerializeField] const float maxChargeTime = 2.0f; //最大チャージ時間(割合計算に使うためconst)
    [SerializeField] GameObject bulletPrefab;
    bool pushing = false; //マウスボタンがdown状態かのフラグ

    [SerializeField] const int maxHP = 10; //最大体力(割合計算に使うためconst)
    int nowHP = maxHP; //現在体力 初期化はmaxHPで

    [SerializeField] ParticleSystem Ef_Explosion; //爆発エフェクト
    bool EfOnce = false; //爆発エフェクトが一回だけ出るように

    bool IsAlive = true; //生存フラグ
    GameObject PowerBer; //チャージバー
    GameObject HPBer; //HPバー

    AudioSource audioSource; //オーディオソース
    [SerializeField]AudioClip SE_Shoot; //弾発射時の音
    [SerializeField]AudioClip SE_Bomb; //死亡時の音

    private Image ber;

    private void Awake()
    {
        if (PlayerInstance != null && PlayerInstance != this)
        {
            Destroy(gameObject);
        }
        inputActions = new PlayerInput();

        //入力処理関数をバインド
        inputActions.Player.Shot.started += StartCharge;
        inputActions.Player.Shot.canceled += Shoot;

        inputActions.Enable();

        PowerBer = GameObject.Find("Player_Charge");
        HPBer = GameObject.Find("Canvas");
        ber = PowerBer.GetComponent<Image>();

        PlayerInstance = this;

    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (IsAlive == true)
        {
            if (pushing == true)
            {
                //チャージ
                cnt_MouseTime += Time.deltaTime;
                if (cnt_MouseTime > maxChargeTime) { cnt_MouseTime = maxChargeTime; }

                //チャージ割合を表示する
                PowerBer.GetComponentInChildren<PowerBar>().SetFillAmount(cnt_MouseTime / maxChargeTime);
            }


        }

        //HPが0になったら死亡
        if (nowHP <= 0)
        {
            //エフェクトを一回だけ再生
            if (EfOnce == false)
            {
                Instantiate(Ef_Explosion, transform.position, Quaternion.identity);
                IsAlive = false;
                EfOnce = true;
            }

            //死亡時に音を鳴らす(Destroyしても大丈夫)
            AudioSource.PlayClipAtPoint(SE_Bomb, transform.position);

            Destroy(PowerBer);
            Destroy(gameObject);
        }
    }

    //攻撃された時の処理
    public void Hit(int damage_)
    {
        nowHP -= damage_;
    }

    //入力イベント
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

        //発射音を鳴らす
        audioSource.PlayOneShot(SE_Shoot);

        //チャージバーをリセット
        pushing = false;
        GameObject PowerBer = GameObject.Find("Canvas");
        PowerBer.GetComponentInChildren<PowerBar>().SetFillAmount(0);
    }
    //入力イベントここまで

    //チャージ割合の取得
    //弾が呼び出す　このタイミングでMouseカウントをリセット
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
