using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAttack : AIDecision
{
    public override bool MakeADecision()
    {
        return brain.isAttack;
    }
}
