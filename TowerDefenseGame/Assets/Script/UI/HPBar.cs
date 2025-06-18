using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image HPFillImage; 
    public float maxHP = 100f; //最大HP
    public float minHP = 0f;   //最小HP
    //public float chargeSpeed = 50f; //チャージ速度（1秒に50チャージ）

    //public float minDamage = 10f; //最小ダメージ
    //public float maxDamage = 100f; //最大ダメージ

    private float nowHP = 100f; //今のHPー最初は100で
    //private bool isCharging = false; //今チャージ中かどうか

    void Start()
    {
        //最初はHPを100で初期化
        nowHP = 100f;

        //UpdateHPBar();
        HPFillImage.fillAmount = 100;
    }

    void Update()
    {
        //どんどんFillBarを減らす
        nowHP = GetComponent<PlayerController>().GetHP();
    }


    public void SetFillAmount(float per_)
    {
        HPFillImage.fillAmount = per_;
    }

}
