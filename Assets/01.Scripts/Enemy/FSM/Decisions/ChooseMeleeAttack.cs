using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMeleeAttack : AIDecision
{
    public override bool MakeADecision()
    {
        int value = UnityEngine.Random.Range(0, 10);

        return value < 7;
    }
}
