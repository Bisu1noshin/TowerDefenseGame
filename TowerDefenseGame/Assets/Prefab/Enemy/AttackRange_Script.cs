using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange_Script : MonoBehaviour
{
    bool colhit = false;
    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.TryGetComponent<PlayerController>(out var p))
        {
            colhit = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        colhit = false;
    }
    public bool CollisionHitPlayer()
    {
        return colhit;
    }
}
