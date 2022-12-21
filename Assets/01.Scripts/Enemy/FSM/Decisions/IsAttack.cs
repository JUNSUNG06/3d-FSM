using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAttack : AIDecision
{
    private BossAttackManage attack;

    protected override void Start()
    {
        base.Start();

        attack = brain.GetComponent<BossAttackManage>();
    }

    public override bool MakeADecision()
    {
        return attack.isAttack == false;
    }
}
