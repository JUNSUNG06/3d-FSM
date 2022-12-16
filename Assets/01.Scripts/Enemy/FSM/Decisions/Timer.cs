using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : AIDecision
{
    private bool isStart = true;
    public float time = 0f;
    public float currentTime = 0f;

    public override bool MakeADecision()
    {
        if(isStart)
        {
            currentTime = 0f;
            isStart = false;
        }

        currentTime += Time.deltaTime;

        if(currentTime >= time)
        {
            isStart = true;
            return true;
        }

        return false;
    }
}
