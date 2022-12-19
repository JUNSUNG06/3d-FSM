using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    public AIState currentState;
    public EnemyStatusSO status;

    private CharacterController characterController;
    public Transform target;

    public bool isAttack = false;
    public float attackPower = 0f;

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

    public void CheckHit()
    {
        Collider[] col = Physics.OverlapSphere(transform.position + transform.forward + Vector3.up, 1f, 1 << 10);

        if (col.Length > 0)
        {
            col[0].GetComponent<IDamage>().Damaged(attackPower, Vector3.zero);
        }
    }

    public void EndOfAttack()
    {
        isAttack = false;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + transform.forward + Vector3.up, 1f);
    }
}
