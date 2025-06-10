using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TItleLogoControllre : MonoBehaviour
{
    [SerializeField] MaskableGraphic[] titleLogo;
    [SerializeField] MaskableGraphic schoolLogo;
    [SerializeField] TitleSceneManager tsm;

    private int PlayerCnt;
    private float timeCnt;

    private void Start()
    {
        // äwçZÉçÉSÇÃèâä˙âª
        {
            schoolLogo.color = new Color(255, 255, 255, 0);
            schoolLogo.enabled = true;
        }

        // É^ÉCÉgÉãÉçÉSÇÃèâä˙âª
        {
            for (int i = 0; i < titleLogo.Length; i++)
            {

                Vector3 vec = new (16.13888931274414f, 1.7222223281860352f, 90);
                titleLogo[i].rectTransform.position = vec;
            }
        }

        PlayerCnt = 0;
    }

    private void Update()
    {
        if (tsm.GetOnMauseLeft()) {

            PlayerCnt++;
        }

        if (PlayerCnt >= 0) {

            SchoolLogoUpData();
        }
        else
        {
            TitleLogoUpData();
        }
    }

    private void TitleLogoUpData() { 


        if (PlayerCnt == -1){

            for (int i = 0; i < titleLogo.Length; i++)
            {

                Vector3 vec = new(-0.8f, 1.7222223281860352f, 90);
                titleLogo[i].rectTransform.position = vec;
            }
        }
    }

    private void SchoolLogoUpData(){ 

        if (schoolLogo.color.a >= 1) {

            PlayerCnt = -8;
            schoolLogo.color = new Color(255, 255, 255, 0);
        }

        schoolLogo.color += new Color(0f, 0f, 0f, 1f / 256f);

        if (PlayerCnt == 1) {

            schoolLogo.color = new Color(255, 255, 255, 255);
        }
    }
}
