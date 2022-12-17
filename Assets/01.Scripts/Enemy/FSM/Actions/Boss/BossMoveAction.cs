using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMoveAction : AIAction
{
    private NavMeshAgent nav;
    private BossAnim anim;

    private void Start()
    {
        base.Start();

        nav = brain.GetComponent<NavMeshAgent>();
        anim = brain.GetComponent<BossAnim>();
    }

    public override void TakeAction()
    {
        Move();
        anim.MoveAnim();
    }

    private void Move()
    {
        nav.destination = brain.target.position;
    }
}
