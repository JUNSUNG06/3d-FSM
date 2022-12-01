using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Player
{
    public float attackPower;

    private PlayerAnimation animator;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        isActive = true;

        animator.AttackAnim();
    }
}