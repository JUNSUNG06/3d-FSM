using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossJumpAttackAction : AIAction
{
    private enum AttackType
    {
        MeleeAttack1,
        MeleeAttack2,
        JumpAttack
    }

    [SerializeField] private AttackType attackType;

    private NavMeshAgent nav;
    private BossAnim animator;
    private BossAttackManage attack;

    private Vector3 targetPos;
    public float jumpSpeed;


    protected override void Start()
    {
        base.Start();

        animator = brain.GetComponent<BossAnim>();
        nav = brain.GetComponent<NavMeshAgent>();
        attack = brain.GetComponent<BossAttackManage>();
    }

    public override void OnStartAction()
    {       
        animator.AttackAnim(attackType.ToString());
        attack.isAttack = true;

        nav.destination = brain.transform.position; //BossAttackMaange에서 이어감
        nav.speed = jumpSpeed;
    }

    public override void TakeAction()
    {

    }
}

