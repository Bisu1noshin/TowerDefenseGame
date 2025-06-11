using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public Image powerFillImage; //Red bar
    public float maxPower = 100f; //�ő�p���[
    public float chargeSpeed = 50f; //�`���[�W���x�i1�b��50�`���[�W�j

    public float minDamage = 10f; //�ŏ��_���[�W
    public float maxDamage = 100f; //�ő�_���[�W

    private float currentPower = 0f; //���̃p���[�ŏ��͂O��
    private bool isCharging = false; //���`���[�W�����ǂ���

    void Start()
    {
        //�ŏ��̓p���[���O�ŏ�����
        currentPower = 0f;

        //UpdatePowerBar();
        powerFillImage.fillAmount = 0;
    }

    void Update()
    {
        //�}�E�X���N���b�N�Ń`���[�W
        if (Input.GetMouseButton(0))
        {
            //ChargePower();
        }

        //�}�E�X�{�^�����ꂽ�甭��
        if (Input.GetMouseButtonUp(0))
        {
            //Shot();

        }
    }

    /*void ChargePower()      //�p���[�`���[�W
    {
        if (currentPower < maxPower)
        {
            currentPower += chargeSpeed * Time.deltaTime;

            //�ő�l�𒴂��Ȃ��悤��
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

    /*void UpdatePowerBar()       //�p���[�o�[���Update
    {
        //Fill Amount��0�`1�̊�
        float fillAmaount = currentPower / maxPower;
        powerFillImage.fillAmount = fillAmaount;
    }

    //�e����
    void Shot()
    {
        float damage = Mathf.Lerp(minDamage, maxDamage, currentPower);

        //�e���ˎ���

        ResetBar();
        isCharging = false;
    }

    void ResetBar()
    {
        currentPower = 0f;
        UpdatePowerBar();
    }*/
}