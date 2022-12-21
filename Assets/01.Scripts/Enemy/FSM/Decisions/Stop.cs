using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : AIDecision
{
    public override bool MakeADecision()
    {
        Debug.Break();
        return false;
    }
}
