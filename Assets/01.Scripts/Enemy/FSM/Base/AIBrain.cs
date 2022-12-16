using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    public AIState currentState;

    private CharacterController characterController;

    private void Start()
    {  
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        currentState.StateUpdate();
    }

    public void ChangeState(AIState nextState)
    {
        currentState = nextState;
        currentState.StateStart();
    }

    public void Move()
    {

    }

    public void StopMove()
    {

    }
}
