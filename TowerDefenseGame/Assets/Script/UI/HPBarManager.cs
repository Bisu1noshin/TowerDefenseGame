using UnityEngine;
using UnityEngine.UI;

public class HPBarManager : MonoBehaviour
{
    int maxHP;
    int nowHP;

    public Image HPFillImage; //blue�o�[


    private void Start()
    {
        int nowHP = maxHP;
    }

    private void Update()
    {
        //damage���ꂽ��HP�o�[��FillAmount�����炷
        
    }

    void UpdateHPBar()  //HP�o�[�̏��Update
    {
        //Fill Amount��0�`1�̊�
        float fillAmaount = nowHP / maxHP;
        HPFillImage.fillAmount = fillAmaount;
    }
}
