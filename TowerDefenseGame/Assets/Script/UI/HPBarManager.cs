using UnityEngine;
using UnityEngine.UI;

public class HPBarManager : MonoBehaviour
{
    int maxHP;
    int nowHP;

    public Image HPFillImage; //blueバー


    private void Start()
    {
        int nowHP = maxHP;
    }

    private void Update()
    {
        //damageされたらHPバーのFillAmountを減らす
        
    }

    void UpdateHPBar()  //HPバーの状態Update
    {
        //Fill Amountは0～1の間
        float fillAmaount = nowHP / maxHP;
        HPFillImage.fillAmount = fillAmaount;
    }
}
