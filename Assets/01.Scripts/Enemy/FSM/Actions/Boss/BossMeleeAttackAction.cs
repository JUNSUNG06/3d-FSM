using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeAttackAction : AIAction
{
    private enum AttackType
    {
        MeleeAttack1,
        MeleeAttack2,
        JumpAttack
    }

    [SerializeField] private AttackType attackType;

    private BossAnim animator;
    private BossAttackManage attack;

    protected override void Start()
    {
        base.Start();

        animator = brain.GetComponent<BossAnim>();
        attack = brain.GetComponent<BossAttackManage>();
    }

    public override void OnStartAction()
    {

        animator.AttackAnim(attackType.ToString());
        attack.isAttack = true;
    }

    public override void TakeAction()
    {
        
    }
}
