using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackAction : AIAction
{
    private enum AttackType
    {
        MeleeAttack1,
        MeleeAttack2,
        JumpAttack
    }

    [SerializeField] private AttackType attackType;

    public override void OnStartAction()
    {
        BossAnim animator = brain.GetComponent<BossAnim>();

        animator.AttackAnim(attackType.ToString());
        brain.isAttack = true;
    }

    public override void TakeAction()
    {
        
    }
}
