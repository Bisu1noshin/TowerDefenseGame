using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public Image powerFillImage; //Red bar
    public float maxPower = 100f; //最大パワー
    public float chargeSpeed = 50f; //チャージ速度（1秒に50チャージ）

    public float minDamage = 10f; //最小ダメージ
    public float maxDamage = 100f; //最大ダメージ

    private float currentPower = 0f; //今のパワー最初は０で
    private bool isCharging = false; //今チャージ中かどうか

    void Start()
    {
        //最初はパワーを０で初期化
        currentPower = 0f;

        //UpdatePowerBar();
        powerFillImage.fillAmount = 0;
    }

    void Update()
    {
        //マウス左クリックでチャージ
        if (Input.GetMouseButton(0))
        {
            //ChargePower();
        }

        //マウスボタン離れたら発射
        if (Input.GetMouseButtonUp(0))
        {
            //Shot();

        }
    }

    /*void ChargePower()      //パワーチャージ
    {
        if (currentPower < maxPower)
        {
            currentPower += chargeSpeed * Time.deltaTime;

            //最大値を超えないように
            if (currentPower > maxPower)
            {
                currentPower = maxPower;
            }
        }

        UpdatePowerBar();
    }*/

    public void SetFillAmount(float per_)
    {
        powerFillImage.fillAmount = per_;
    }

    /*void UpdatePowerBar()       //パワーバー状態Update
    {
        //Fill Amountは0～1の間
        float fillAmaount = currentPower / maxPower;
        powerFillImage.fillAmount = fillAmaount;
    }

    //弾発射
    void Shot()
    {
        float damage = Mathf.Lerp(minDamage, maxDamage, currentPower);

        //弾発射実装

        ResetBar();
        isCharging = false;
    }

    void ResetBar()
    {
        currentPower = 0f;
        UpdatePowerBar();
    }*/
}