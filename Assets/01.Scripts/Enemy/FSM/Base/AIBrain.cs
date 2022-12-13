using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    [SerializeField] AIState currentState;

    private void Start()
    {
        currentState = transform.Find("AI/IdleState").GetComponent<AIState>();    
    }

    private void Update()
    {
        currentState.StateUpdate();
    }

    public void ChangeState(AIState nextState)
    {
        currentState = nextState;
    }
}
