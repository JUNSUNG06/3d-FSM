using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random : AIDecision
{
    [SerializeField] private int maxValue = 0;
    [SerializeField] private int boundaryValue = 0;

    public override bool MakeADecision()
    {
        int randValue = UnityEngine.Random.Range(0, maxValue);

        return randValue < boundaryValue;
    }
}
