using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : BaseUnit
{
    StateMachine<StateType, StateTrigger> sm;
    Animator anim;
    AttackRange_Script attackRange;
    float deadTimer;
    int ATK;

    protected override void Setup()
    {
        GetComponent<SpriteRenderer>().sortingOrder = (int)(-transform.position.y * 10.0f) + 50;
        sm = new StateMachine<StateType, StateTrigger>(StateType.Stand);
        attackRange = GetComponentInChildren<AttackRange_Script>();
        anim = GetComponent<Animator>();
        maxHP = 10;
        HP = maxHP;
        canKB = true;
        ATK = 1;
        maxKBTime = 2;
        kbTime = maxKBTime - 1;
        attackInterval = 0;
        maxAttackInterval = 2.0f;
        deadTimer = 1.0f;
        SMSetup();
        AppearLifeBar();
    }
    protected override void UpdateOverrided()
    {
        LifeBarUpdate();
        sm.Update(Time.deltaTime);
    }
    protected override void SetAttack()
    {
        sm.ExecuteTrigger(StateTrigger.Attack);
        base.SetAttack();
    }
    public override void Hit(Vector2 angle, int damage)
    {
        base.Hit(angle, damage);
    }
    protected override void KnockBack(Vector2 angle)
    {
        bar.FillBar((float)HP / maxHP);
        sm.ExecuteTrigger(StateTrigger.Hit);
        base.KnockBack(angle);
    }
    void SMSetup() //アニメーター管理＿初期化
    {
        sm.SetupState(StateType.Stand, () => anim.Play("Stand", 0, 0), () => NullUpdate(), deltaTime => IdleUpdate());
        sm.SetupState(StateType.Walk, () => anim.Play("Walk", 0, 0), () => NullUpdate(), deltaTime => WalkUpdate());
        sm.SetupState(StateType.Attack, () => anim.Play("Attack", 0, 0), () => NullUpdate(), deltaTime => NullUpdate());
        sm.SetupState(StateType.Hit, () => anim.Play("Hit_L", 0, 0), () => NullUpdate(), deltaTime => HitUpdate());
        sm.SetupState(StateType.Down, () => StartDown(), () => NullUpdate(), deltaTime => DeadUpdate());

        sm.AddTransition(StateType.Stand, StateType.Walk, StateTrigger.Walk);
        sm.AddTransition(StateType.Stand, StateType.Attack, StateTrigger.Attack);
        sm.AddTransition(StateType.Stand, StateType.Hit, StateTrigger.Hit);


        sm.AddTransition(StateType.Walk, StateType.Stand, StateTrigger.Stand);
        sm.AddTransition(StateType.Attack, StateType.Stand, StateTrigger.Stand);
        sm.AddTransition(StateType.Hit, StateType.Stand, StateTrigger.Stand);

        sm.AddTransition(StateType.Hit, StateType.Walk, StateTrigger.Walk);
        sm.AddTransition(StateType.Attack, StateType.Walk, StateTrigger.Walk);

        sm.AddTransition(StateType.Walk, StateType.Hit, StateTrigger.Hit);
        sm.AddTransition(StateType.Attack, StateType.Hit, StateTrigger.Hit);

        sm.AddTransition(StateType.Walk, StateType.Attack, StateTrigger.Attack);
        sm.AddTransition(StateType.Hit, StateType.Attack, StateTrigger.Attack);


        sm.AddTransition(StateType.Stand, StateType.Down, StateTrigger.Down);
        sm.AddTransition(StateType.Walk, StateType.Down, StateTrigger.Down);
        sm.AddTransition(StateType.Attack, StateType.Down, StateTrigger.Down);
        sm.AddTransition(StateType.Hit, StateType.Down, StateTrigger.Down);
    }
    void NullUpdate() { /*pass*/ }
    void IdleUpdate()//
    {
        if (!AllTimerIs0()) { return; }
        moveVec = Vector3.zero;
        GameObject p = GameObject.Find("Player");
        if (p != null)
        {
            Vector2 dist = (p.transform.position - transform.position);
            if(dist.magnitude < minDist)
            {
                if (attackInterval <= 0)
                {
                    SetAttack();
                }
            }
            else
            {
                sm.ExecuteTrigger(StateTrigger.Walk);
            }
        }
    }
    void WalkUpdate()//
    {
        if (!AllTimerIs0()) { return; }
        GameObject p = GameObject.Find("Player");
        if (p != null)
        {
            Vector2 dist = (Vector2)(p.transform.position - transform.position);
            if (dist.magnitude > minDist)
            {
                moveVec = dist.normalized;
            }
            else
            {
                moveVec = Vector2.zero;
                if (AllTimerIs0())
                {
                    sm.ExecuteTrigger(StateTrigger.Attack);
                }
            }
        }
    }
    void StartDown() //昇天
    {
        GetComponent<BoxCollider2D>().enabled = false;  //昇天の準備　色々な機能をOFFにする
        GetComponentInChildren<AttackRange_Script>().enabled = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.gray;
        bar.CrushBar();
        anim.Play("Down", 0, 0);
    }
    public void EndAttack()//
    {
        sm.ExecuteTrigger(StateTrigger.Stand);
    }
    void IsDead() //ノックバック後の死亡判定
    {
        moveVec = Vector3.zero;
        if (HP <= 0)
        {
            sm.ExecuteTrigger(StateTrigger.Down);
        }
        else
        {
            sm.ExecuteTrigger(StateTrigger.Stand);
        }
    }
    void DeadUpdate()//
    {
        moveVec = new Vector3(0, 1.0f, 0);
        deadTimer -= Time.deltaTime;
        if (deadTimer <= 0) { Destroy(gameObject); }
    }

    private void AppearLifeBar()//
    {
        GameObject prefab = Resources.Load("enemy/Bar_Enemy_Prefab") as GameObject;
        GameObject g = Instantiate(prefab);
        g.transform.SetParent(GameObject.Find("Canvas").transform, false);
        bar = g.GetOrAddComponent<Bar_Enemy_Controller>();
    }
    void HitUpdate()//
    {
        if (AllTimerIs0())
        {
            IsDead();
        }
    }
    void LifeBarUpdate()//
    {
        if (sm.GetState() != StateType.Down)
        {
            bar.SetPosition(transform.position + Vector3.up * 0.5f);
            bar.FillBar((float)HP / maxHP);
        }
    }
    public void Attack()//
    {
        GameObject p = GameObject.Find("Player");
        if(p != null)
        {
            p.GetComponent<PlayerController>().Hit(ATK);
        }
    }
}
