using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSecondAttackAction : AIAction
{
    BossAttackManage attack;

    protected override void Start()
    {
        base.Start();

        attack = brain.GetComponent<BossAttackManage>();
    }

    public override void OnStartAction()
    {
        attack.isAttack = true;
    }

    public override void TakeAction()
    {
    }
}
