using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public partial class TitleSceneManager : MonoBehaviour
{
    [SerializeField] MaskableGraphic[] titleLogo;
    [SerializeField] MaskableGraphic schoolLogo;
    [SerializeField] MaskableGraphic startLogo;

    private int PlayerCnt;
    private float timeCnt;

    private void Start()
    {
        // 学校ロゴの初期化
        SchoolLogoInitialize();

        // タイトルロゴの初期化
        TitleLogoInitialize();

        // スタートボタン初期化
        StartButtonInitialize();

        // メンバー変数の初期化
        PlayerCnt = 0;
        timeCnt = 0;
    }

    private void Update()
    {
        // マウスの位置に座標を移動
        transform.position = GetMousePosition();

        // ロゴの遷移
        if (PlayerCnt >= 0) {
            
            SchoolLogoUpData();
        }
        else
        {
            TitleLogoUpData();
        }
    }

    private void TitleLogoUpData() {

        if (PlayerCnt == -1)
        {
            timeCnt += Time.deltaTime;
            StartButtonUpData();

            if (timeCnt >= 20.0f)
            {
                PlayerCnt++;
                timeCnt = 0;
                return;
            }

            return;
        }

        Vector3 toVec = new(-0.8f, 1.7222223281860352f, 90);

        int num = PlayerCnt * -1 % 2;
        int arr = PlayerCnt * -1 / 2;

        if (num == 1)
        {

            if (MoveTitleUI(titleLogo[arr - 1], toVec))
            {

                PlayerCnt++;
                return;
            }
        }
        else
        {

            SetTitleUI(titleLogo[arr - 1], toVec);
            timeCnt += Time.deltaTime;

            if (timeCnt >= 1.0f)
            {

                PlayerCnt++;
                timeCnt = 0;
                return;
            }
        }
    }

    private bool MoveTitleUI(MaskableGraphic ui_,Vector3 vec) {

        if (ui_.transform.position.x == vec.x) { 

            return true;
        }

        float speed = 5.0f;

        // 指定された座標に移動
        ui_.transform.position = Vector3.MoveTowards(
            ui_.transform.position,
            vec,
            speed * Time.deltaTime
            ) ;

        return false;
    }

    private void SetTitleUI(MaskableGraphic ui_, Vector3 vec) {

        ui_.transform.position = vec;
    }

    private void SchoolLogoUpData(){

        if (PlayerCnt == 0)
        {
            TitleLogoInitialize();
            StartButtonInitialize();
        }

        if (schoolLogo.color.a >= 1) {

            PlayerCnt = -7;
            schoolLogo.color = new Color(255, 255, 255, 0);
        }

        schoolLogo.color += new Color(0f, 0f, 0f, 1f / 256f / 3f);

        if (PlayerCnt == 1) {

            schoolLogo.color = new Color(255, 255, 255, 255);
        }
    }

    private void StartButtonUpData() {

        startLogo.enabled = true;

        // 接触処理

        Vector3 p = transform.position;
        float x = 2.2f;
        float y = -2.32f;
        float w = 6.4f;
        float h = -3.67f;

        if (p.x <= x && p.y <= y){
            if (p.x > w && p.y > h){

                startLogo.color = Color.red;
            }
        }
    }

    private void SchoolLogoInitialize() {

        schoolLogo.color = new Color(255, 255, 255, 0);
        schoolLogo.enabled = true;
    }

    private void TitleLogoInitialize() {

        for (int i = 0; i < titleLogo.Length; i++)
        {

            Vector3 vec = new(16.13888931274414f, 1.7222223281860352f, 90);
            titleLogo[i].rectTransform.position = vec;
        }
    }

    private void StartButtonInitialize() {

        startLogo.enabled = false;
        startLogo.color = Color.green;
    }
}