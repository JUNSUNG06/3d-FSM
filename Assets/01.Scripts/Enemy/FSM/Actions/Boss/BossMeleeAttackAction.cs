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

    public int secondaryAttack = 0;

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
        SetSecondaryAttack();
    }

    public override void TakeAction()
    {
        
    }

    private void SetSecondaryAttack()
    {
        int randValue = UnityEngine.Random.Range(0, 10);

        if(randValue < 5)
        {
            secondaryAttack = 0;
        }
        else
        {
            randValue = UnityEngine.Random.Range(0, 10);

            if(randValue < 5)
            {
                secondaryAttack = 1;
            }
            else
            {
                secondaryAttack = 2;
            }
        }

        animator.SetSecondaryAttack(secondaryAttack);
    }
}
