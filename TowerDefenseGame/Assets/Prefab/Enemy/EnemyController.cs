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
    protected override void Setup()
    {
        sm = new StateMachine<StateType, StateTrigger>(StateType.Stand);
        attackRange = GetComponentInChildren<AttackRange_Script>();
        anim = GetComponent<Animator>();
        maxHP = 10;
        HP = 10;
        canKB = true;
        attackInterval = 0;
        maxAttackInterval = 1.0f;
        SMSetup();
    }
    protected override void UpdateOverrided()
    {
        if (attackRange.CollisionHitPlayer())
        {
            moveVec = Vector3.zero;
            if(attackInterval <= 0)
            {
                if (GameObject.Find("Player").gameObject.TryGetComponent<PlayerController>(out var p))
                {
                    SetAttack();
                }
                
                //p.Damage();
            }
            else
            {
                sm.ExecuteTrigger(StateTrigger.Stand);
            }
        }
        else
        {
            if(GameObject.Find("Player") != null)
            {
                Vector2 dist = (Vector2)(GameObject.Find("Player").transform.position - transform.position);
                if(dist.magnitude > 1.5f)
                {
                    moveVec = dist.normalized;
                }
                else
                {
                    moveVec = Vector2.zero;
                }
                sm.ExecuteTrigger(StateTrigger.Walk);
            }
        }
        sm.Update(Time.deltaTime);
    }
    protected override void SetAttack()
    {
        sm.ExecuteTrigger(StateTrigger.Attack);
        base.SetAttack();
    }
    protected override void Hit(Vector2 angle, int damage)
    {
        base.Hit(angle, damage);
    }
    protected override void KnockBack(Vector2 angle)
    {
        sm.ExecuteTrigger(StateTrigger.Hit);
        base.KnockBack(angle);
    }
    void ChangeAnim(int a) //0を経由させるためだけの処理
    {
        if(anim.GetInteger("State") == a) { return; } //アニメーションが同じ場合は変更なし
        anim.SetInteger("State", 0);
        anim.SetInteger("State", a);
    }
    void SMSetup() //アニメーター管理＿初期化
    {
        sm.SetupState(StateType.Stand, () => anim.Play("Stand", 0, 0), () => NullUpdate(), deltaTime => NullUpdate());
        sm.SetupState(StateType.Walk, () => anim.Play("Walk", 0, 0), () => NullUpdate(), deltaTime => NullUpdate());
        sm.SetupState(StateType.Attack, () => anim.Play("Attack", 0, 0), () => NullUpdate(), deltaTime => NullUpdate());
        sm.SetupState(StateType.Hit, () => anim.Play("Hit", 0, 0), () => IsDead(), deltaTime => NullUpdate());
        sm.SetupState(StateType.Down, ()=> StartDown(), ()=> Kill(), deltaTime => NullUpdate());

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
    void StartDown() //昇天
    {
        GetComponent<BoxCollider2D>().enabled = false;  //昇天の準備　色々な機能をOFFにする
        GetComponentInChildren<AttackRange_Script>().enabled = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.gray;
        anim.Play("Down", 0, 0);
    }
    void IsDead() //ノックバック後の死亡判定
    {
        if(this.HP <= 0)
        {
            sm.ExecuteTrigger(StateTrigger.Down);
        }
        else
        {
            sm.ExecuteTrigger(StateTrigger.Stand);
        }
    }
}
