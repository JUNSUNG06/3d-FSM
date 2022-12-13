using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AIDecision> decisions = new List<AIDecision>();

    public AIState positiveResult;
    public AIState negativeResult;
}
