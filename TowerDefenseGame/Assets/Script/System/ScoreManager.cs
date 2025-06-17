using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public ScoreManager ScoreManagerInstance; //生成された自分自身を入れる(多重生成防止)
    public int Score; //スコア

    private void Awake()
    {
        CheckInstance();
    }


    void Start()
    {
        DontDestroyOnLoad(gameObject);
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
