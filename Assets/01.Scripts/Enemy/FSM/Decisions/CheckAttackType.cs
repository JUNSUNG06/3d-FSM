using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttackType : AIDecision
{
    public int attackType = 0;

    private BossAnim anim;

    protected override void Start()
    {
        base.Start();

        anim = brain.GetComponent<BossAnim>();
    }

    public override bool MakeADecision()
    {
        Debug.Log(anim.GetSecondaryAttackType());
        return (anim.GetSecondaryAttackType() == attackType);
    }
}
