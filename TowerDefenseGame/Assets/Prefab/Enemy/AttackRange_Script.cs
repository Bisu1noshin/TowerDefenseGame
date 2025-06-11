using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange_Script : MonoBehaviour
{
    bool colhit;
    private void Start()
    {
        colhit = false;
    }
    private void Update()
    {
        transform.localPosition = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            colhit = true;
        }
        else
        {
            colhit = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            colhit = true;
        }
        else
        {
            colhit = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        colhit = false;
    }
    public bool CollisionHitPlayer()
    {
        return colhit;
    }
}
