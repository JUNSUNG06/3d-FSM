using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMoveAction : AIAction
{
    private CharacterController cc;
    private NavMeshAgent nav;

    private Vector3 moveDir;

    private void Start()
    {
        base.Start();

        cc = brain.GetComponent<CharacterController>();
        nav = brain.GetComponent<NavMeshAgent>();
        Debug.Log(brain.target);
    }

    public override void TakeAction()
    {
        moveDir = (brain.target.position - transform.position).normalized;

        Move();
    }

    private void Move()
    {
        /*cc.Move(moveDir * brain.status.moveSpeed * Time.deltaTime);
        Turn();*/
        nav.destination = brain.target.position;
        Debug.Log(brain.target.position);
    }

    private void Turn()
    {
        float angle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;

        brain.transform.localRotation = Quaternion.Lerp(brain.transform.localRotation, Quaternion.AngleAxis(angle, Vector3.up),
            Time.deltaTime * 5);
    }
}
