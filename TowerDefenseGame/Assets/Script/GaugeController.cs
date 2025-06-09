using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public float maxPower = 10f;
    public float chargeSpeed = 2f;
    public float dischargeSpeed = 2f;
    private Slider powerSlider;

    public float currentPower;

    private Coroutine Charge;
    private Coroutine disCharge;
    private bool isCharing;


    public void Start()
    {
        if (powerSlider != null)
        {
            powerSlider.minValue = 0f;
            powerSlider.maxValue = maxPower;

        }

    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ICharge());
        }
        else
        {
            StopCoroutine(ICharge());
        }
    }

    // 6/9�ǋL
    IEnumerator ICharge()   //�Q�[�W�`���[�W
    {
        while (isCharing == true)
        {
            powerSlider.value += Time.deltaTime * chargeSpeed;
            yield return null;
        }

        while (isCharing == false)
        {
            powerSlider.value -= Time.deltaTime * chargeSpeed;
            yield return null;
        }
    }

#if false
    
    // ���̃R�[�h
    IEnumerator Charge()   //�Q�[�W�`���[�W
    {
        while(isCharing == true)
        {
            powerSlider.value += Time.deltaTime * chargeSpeed;
            yield return null;
        }

        while(isCharing == false)
        {
            powerSlider.value -= Time.deltaTime * chargeSpeed;
            yield return null;
        }
    }
#endif
}
