using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    [SerializeField] AIState currentState;

    private CharacterController characterController;

    private void Start()
    {
        currentState = transform.Find("AI/IdleState").GetComponent<AIState>();    
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        currentState.StateUpdate();
    }

    public void ChangeState(AIState nextState)
    {
        currentState = nextState;
    }

    public void Move()
    {

    }

    public void StopMove()
    {

    }
}
