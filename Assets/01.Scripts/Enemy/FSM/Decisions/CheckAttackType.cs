using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttackType : AIDecision
{
    public int attackType = 0;

    private BossAnim anim;

    protected override void Start()
    {
        anim = brain.GetComponent<BossAnim>();
    }

    public override bool MakeADecision()
    {
        return anim.GetSecondaryAttackType() == attackType;
    }
}
