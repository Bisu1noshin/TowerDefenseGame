using System;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneController : MonoBehaviour
{
    private Vector3 Finish_Position = new Vector3(103f, 74.6655884f, 0f);
    private Vector3 Plese_Any_Kye_Position = new Vector3(51, -542, 0);

    [SerializeField] private MaskableGraphic FinishLogo;
    [SerializeField] private MaskableGraphic Plese_Any_KyeLogo;
    [SerializeField] private MaskableGraphic ScoreLogo;
    [SerializeField] private MaskableGraphic youre_Score_isLogo;

    private void Start()
    {

    }

    private void Update()
    {
        GamingColor(ScoreLogo);
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
}
