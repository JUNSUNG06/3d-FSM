using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    public AIState currentState;
    public EnemyStatusSO status;

    private CharacterController characterController;
    public Transform target;

    private void Start()
    {  
        characterController = GetComponent<CharacterController>();
        target = GameObject.Find("Player").GetComponent<Transform>();
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



    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + transform.forward + Vector3.up, 1f);
    }
}
