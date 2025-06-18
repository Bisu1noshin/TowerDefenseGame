using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    [SerializeField]protected int HP;     //現在HP
    protected enum StateType { Stand, Walk, Attack, Hit, Down, Bound };
    protected enum StateTrigger { Stand, Walk, Attack, Hit, Down, Bound };
    protected int maxHP;  //最大HP
    protected bool canKB; //ノックバックができるオブジェクトか否か
    protected int kbTime; //ノックバック回数
    protected int maxKBTime;
    protected Vector2 moveVec; //一秒間で移動する座標量を入れる
    List<float> timers = new () { 0, 0, 0, 0, 0, 0 };             //行動不能タイマー管理用　1番目：攻撃所要時間　2番目：ノックバック時間
    List<float> maxTimers = new () { 0, 0, 0.5f, 0.2f, 0, 1.0f };    //行動不能タイマー管理用　1番目：最大攻撃時間　2番目：最大ノックバック時間
    protected float attackInterval;       //攻撃間隔
    protected float maxAttackInterval;    //最大攻撃間隔
    protected bool isAlive;
    protected Bar_Enemy_Controller bar;
    protected float minDist = 1.8f;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.PlayerInstance == null) { return; }
        transform.position += (Vector3)moveVec * Time.deltaTime;
        if (!isAlive) { return; }
        if(bar != null) { bar.SetPosition(transform.position + Vector3.up * 0.5f); }
        if(attackInterval > 0) { attackInterval -=  Time.deltaTime; }
        if (!DecreaseTimer()) { return; }
        UpdateOverrided();
    }
    protected virtual void Setup()
    {

    }
    protected abstract void UpdateOverrided();
    public virtual void Hit(Vector2 angle, int damage) //弾の発射角度とダメージを見る
    {
        HP -= damage;

        float HPper = Mathf.Clamp01((float)HP / (float)maxHP); //ノックバック処理
        float KBper = Mathf.Clamp01((float)kbTime / (float)maxKBTime);
        if (HPper <= KBper && canKB)
        {
            SetTimer(StateType.Hit);
            KnockBack(angle);
        }

    }
    protected virtual void KnockBack(Vector2 angle)
    {
        kbTime--;
        moveVec = angle.normalized * 3.0f;
    }
    protected virtual void Kill()
    {
        Destroy(gameObject);
    }
    bool DecreaseTimer()
    {
        bool all0 = true;
        for(int t = 0; t < timers.Count; ++t)
        {
            if (timers[t] > 0)
            {
                timers[t] -= Time.deltaTime;
                all0 = false;
            }
        }
        return all0;
    }
    protected bool AllTimerIs0()
    {
        bool b = true;
        foreach(float f in timers)
        {
            if(f > 0)
            {
                b = false;
            }
        }
        return b;
    }
    protected void SetTimer(int elementNum) //timersの指定した番号を対応した最大値に設定する
    {
        if(elementNum >= timers.Count) { return; }
        timers[elementNum] = maxTimers[elementNum];
    }
    protected void SetTimer(StateType type)
    {
        if ((int)type >= timers.Count) { return; }
        timers[(int)type] = maxTimers[(int)type];
    }
    protected virtual void SetAttack() //攻撃したことにする処理
    {
        attackInterval = maxAttackInterval;
        SetTimer(StateType.Attack);

    }
    public void Attack(int dmg) //Animationで呼び出す用の関数
    {
        PlayerController p = GameObject.Find("Player").GetComponent<PlayerController>();
        p.Hit(dmg);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
