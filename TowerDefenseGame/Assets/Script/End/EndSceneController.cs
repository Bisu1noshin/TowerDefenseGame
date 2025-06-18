using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneController : MonoBehaviour
{
    [SerializeField] private MaskableGraphic FinishLogo;
    [SerializeField] private MaskableGraphic Plese_Any_KyeLogo;
    [SerializeField] private TextMeshProUGUI ScoreLogo;
    [SerializeField] private MaskableGraphic youre_Score_isLogo;

    private int PlayerCnt;
    private float timeCnt;

    private void Start()
    {
        PlayerCnt = 0;

        // logoの初期化
        {
            Plese_Any_KyeLogo.enabled = false;
            ScoreLogo.enabled = false;
            youre_Score_isLogo.enabled = false;
        }

        // Scoreの登録

        ScoreLogo.text = ScoreManager.ScoreManagerInstance.GetScore().ToString()+"点";
    }

    private void Update()
    {
        GamingColor(ScoreLogo);

        if (Input.anyKeyDown) {

            PlayerCnt++;

            if (Plese_Any_KyeLogo.enabled)
            {
                SceneManager.LoadScene("TitleScene");
            }
        }

        FinishLogoUpData(PlayerCnt);
        youre_Score_isLogoUpData(PlayerCnt);
        Plese_Any_KyeLogoUpData(PlayerCnt);
        ScoreLogoUpData(PlayerCnt);
    }

    private void GamingColor(MaskableGraphic ui)
    {
        float addValue = 1f / 256f * 16f;
        float maxValue = 1f;

        float r = ui.color.r;
        float g = ui.color.g;
        float b = ui.color.b;

        if (r == maxValue && g == 0) {

            b += addValue;
        }

        if (g == 0 && b == maxValue)
        {
            r -= addValue;
        }

        if (r == 0 && b == maxValue)
        {
            g += addValue;
        }

        if (r == 0 && g == maxValue)
        {
            b -= addValue;
        }

        if (b == 0 && g == maxValue) {

            r += addValue;
        }

        if (b == 0 && r == maxValue)
        {
            g -= addValue;
        }

        ui.color = new Color(r, g, b);
    }

    private void FinishLogoUpData(int cnt) {

        if (cnt == 0) {

            timeCnt += Time.deltaTime;

            if (timeCnt >= 0.5f) {

                if(FinishLogo.enabled)
                    FinishLogo.enabled = false;
                else
                    FinishLogo.enabled = true;

                timeCnt = 0;
            }
        }

        if (cnt >= 1) {

            FinishLogo.enabled = false;
        }
    }

    private void youre_Score_isLogoUpData(int cnt) {

        if (cnt >= 1)
        {
            youre_Score_isLogo.enabled = true;
        }
    }

    private void ScoreLogoUpData(int cnt) {

        if (cnt >= 2) {

            timeCnt += Time.deltaTime;

            if (timeCnt >= 0.1f)
            {

                if (ScoreLogo.enabled)
                    ScoreLogo.enabled = false;
                else
                    ScoreLogo.enabled = true;

                timeCnt = 0;
            }
        }
    }

    private void Plese_Any_KyeLogoUpData(int cnt) {

        if (cnt >= 3) {

            Plese_Any_KyeLogo.enabled = true;
        }
    }
}
