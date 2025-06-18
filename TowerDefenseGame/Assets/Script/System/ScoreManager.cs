using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager ScoreManagerInstance; //生成された自分自身を入れる(多重生成防止)
    public int Score; //スコア

    private void Awake()
    {
        CheckInstance();
    }


    void Start()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //Escで終了する
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    //多重生成防止
    private void CheckInstance()
    {
        if (ScoreManagerInstance == null)
        {
            ScoreManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //スコア加算 敵が倒れた時に呼び出す
    public void AddScore()
    {
        Score++;
    }

    //スコア加算 強敵倒していっぱい増えるとき
    public void AddScore(int Add)
    {
        Score += Add;
    }

    //スコア取得
    public int GetScore()
    {
        return Score;
    }
}
