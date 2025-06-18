using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    PlayerController p;
    float timeCnt;
    const float maxTime = 2f;
    private void Start()
    {
        p = PlayerController.PlayerInstance;
    }

    private void Update()
    {
        if (p == null) {

            timeCnt += Time.deltaTime;

            if(timeCnt>=maxTime)
            SceneManager.LoadScene("EndScene");
        }
    }
}
