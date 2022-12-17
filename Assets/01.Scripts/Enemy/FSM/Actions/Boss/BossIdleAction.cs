using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleAction : AIAction
{
    private BossAnim anim;

    private void Start()
    {
        base.Start();

        anim = brain.GetComponent<BossAnim>();
    }

    public override void TakeAction()
    {
        anim.IdleAnim();   
    }    
}
