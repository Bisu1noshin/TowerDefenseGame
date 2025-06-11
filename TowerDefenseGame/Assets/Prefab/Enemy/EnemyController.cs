using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseUnit
{
    Animator anim;
    AttackRange_Script attackRange;
    protected override void Setup()
    {
        attackRange = GetComponentInChildren<AttackRange_Script>();
        anim = GetComponent<Animator>();
        maxHP = 10;
        HP = 10;
        canKB = true;
        attackInterval = 0;
        maxAttackInterval = 1.0f;
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
                    Debug.Log("Attack");
                    SetAttack();
                }
                
                //p.Damage();
            }
            else
            {
                anim.SetInteger("State", 0); //直立
            }
        }
        else
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
            ChangeAnim(1); //歩き
        }
    }
    protected override void SetAttack()
    {
        ChangeAnim(2); //攻撃
        base.SetAttack();
    }
    protected override void Hit(Vector2 angle, int damage)
    {
        base.Hit(angle, damage);
    }
    protected override void KnockBack(Vector2 angle)
    {
        ChangeAnim(4); //ノックバック
        base.KnockBack(angle);
    }
    void ChangeAnim(int a) //0を経由させるためだけの処理
    {
        if(anim.GetInteger("State") == a) { return; } //アニメーションが同じ場合は変更なし
        anim.SetInteger("State", 0);
        anim.SetInteger("State", a);
    }
}
