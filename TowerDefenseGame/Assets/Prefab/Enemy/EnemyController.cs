using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseUnit
{
    enum StateType { Stand, Walk, Attack, Hit, Down };
    enum StateTrigger { Stand, Walk, Attack, Hit, Down };
    StateMachine<StateType, StateTrigger> sm;
    Animator anim;
    AttackRange_Script attackRange;
    float deadTimer;
    Bar_Enemy_Controller bar;
    protected override void Setup()
    {
        sm = new StateMachine<StateType, StateTrigger>(StateType.Stand);
        attackRange = GetComponentInChildren<AttackRange_Script>();
        anim = GetComponent<Animator>();
        bar = GetComponentInChildren<Bar_Enemy_Controller>();
        maxHP = 10;
        HP = 10;
        canKB = true;
        kbTime = 4;
        attackInterval = 0;
        maxAttackInterval = 2.0f;
        deadTimer = 1.0f;
        SMSetup();
    }
    protected override void UpdateOverrided()
    {
        if(sm.GetState() != StateType.Down)
        {
            bar.SetPosition(transform.position + Vector3.up * 0.5f);
            bar.FillBar((float)HP / maxHP);
        }
        if(sm.GetState() == StateType.Hit && AllTimerIs0())
        {
            IsDead();
        }
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
        sm.ExecuteTrigger(StateTrigger.Hit);
        base.KnockBack(angle);
    }
    void SMSetup() //アニメーター管理＿初期化
    {
        sm.SetupState(StateType.Stand, () => anim.Play("Stand", 0, 0), () =>NullUpdate(), deltaTime => IdleUpdate());
        sm.SetupState(StateType.Walk, () => anim.Play("Walk", 0, 0), () => NullUpdate(), deltaTime => WalkUpdate());
        sm.SetupState(StateType.Attack, () => anim.Play("Attack", 0, 0), () => NullUpdate(), deltaTime => NullUpdate());
        sm.SetupState(StateType.Hit, () => anim.Play("Hit_L", 0, 0), () => NullUpdate(), deltaTime => NullUpdate());
        sm.SetupState(StateType.Down, ()=> StartDown(), ()=> NullUpdate(), deltaTime => DeadUpdate());

        sm.AddTransition(StateType.Stand, StateType.Walk, StateTrigger.Walk);
        sm.AddTransition(StateType.Stand, StateType.Attack, StateTrigger.Attack);
        sm.AddTransition(StateType.Stand, StateType.Hit, StateTrigger.Hit);
        sm.AddTransition(StateType.Walk, StateType.Stand, StateTrigger.Stand);
        sm.AddTransition(StateType.Attack, StateType.Stand, StateTrigger.Stand);
        sm.AddTransition(StateType.Hit, StateType.Stand, StateTrigger.Stand);
        sm.AddTransition(StateType.Hit, StateType.Down, StateTrigger.Down);
        sm.AddTransition(StateType.Walk, StateType.Attack, StateTrigger.Attack);
        sm.AddTransition(StateType.Walk, StateType.Hit, StateTrigger.Hit);
        sm.AddTransition(StateType.Attack, StateType.Walk, StateTrigger.Walk);
        sm.AddTransition(StateType.Attack, StateType.Hit, StateTrigger.Hit);
        sm.AddTransition(StateType.Hit, StateType.Attack, StateTrigger.Attack);
        sm.AddTransition(StateType.Hit, StateType.Walk, StateTrigger.Walk);
    }
    void NullUpdate()
    {
        //pass
    }
    void IdleUpdate()
    {
        moveVec = Vector3.zero;
        if (attackRange.CollisionHitPlayer())
        {
            if(attackInterval <= 0)
            {
                SetAttack();
            }
            else
            {
                if (!sm.Equals(StateTrigger.Stand))
                {
                    sm.ExecuteTrigger(StateTrigger.Stand);
                }
            }
        }
        else
        {
            sm.ExecuteTrigger(StateTrigger.Walk);
        }
    }
    void WalkUpdate()
    {
        if (attackRange.CollisionHitPlayer())
        {
            sm.ExecuteTrigger(StateTrigger.Stand);
        }
        else
        {
            if (GameObject.Find("Player") != null)
            {
                Vector2 dist = (Vector2)(GameObject.Find("Player").transform.position - transform.position);
                if (dist.magnitude > 1.5f)
                {
                    moveVec = dist.normalized;
                }
                else
                {
                    moveVec = Vector2.zero;
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
        anim.Play("Down", 0, 0);
    }
    public void EndAttack()
    {
        sm.ExecuteTrigger(StateTrigger.Stand);
    }
    void IsDead() //ノックバック後の死亡判定
    {
        moveVec = Vector3.zero;
        if(this.HP <= 0)
        {
            sm.ExecuteTrigger(StateTrigger.Down);
        }
        else
        {
            sm.ExecuteTrigger(StateTrigger.Stand);
        }
    }
    void DeadUpdate()
    {
        moveVec = new Vector3(0, 1.0f, 0);
        deadTimer -= Time.deltaTime;
        if (deadTimer <= 0) { Destroy(gameObject); }
    }
}
