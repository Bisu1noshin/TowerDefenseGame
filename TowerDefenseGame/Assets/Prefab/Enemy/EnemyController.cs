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
        maxAttackInterval = 0.4f;
    }
    protected override void UpdateOverrided()
    {
        if (attackRange.CollisionHitPlayer())
        {
            if(attackInterval <= 0 && TryGetComponent<PlayerController>(out var p))
            {
                Attack();
                //p.Damage();
            }
            else
            {
                anim.SetInteger("State", 0); //直立
            }
        }
        else
        {
            anim.SetInteger("State", 1); //歩き
            moveVec = (Vector2)(GameObject.Find("Player").transform.position - transform.position).normalized;
        }
    }
    protected override void Attack()
    {
        anim.SetInteger("State", 2); //攻撃
        base.Attack();
    }
    protected override void Hit(Vector2 angle, int damage)
    {
        base.Hit(angle, damage);
    }
    protected override void KnockBack(Vector2 angle)
    {
        anim.SetInteger("State", 4); //ノックバック
        base.KnockBack(angle);
    }
}
