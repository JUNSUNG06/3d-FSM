using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAttackManage : MonoBehaviour
{
    public bool isAttack = false;
    public float attackPower = 0f;

    public float jumpSpeed;

    private NavMeshAgent nav;
    private AIBrain brain;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        brain = GetComponent<AIBrain>();    
    }

    public void CheckHit()
    {
        Collider[] col = Physics.OverlapSphere(transform.position + transform.forward + Vector3.up, 1.5f, 1 << 10);

        if (col.Length > 0)
        {
            col[0].GetComponent<IDamage>().Damaged(attackPower, Vector3.zero);
        }
    }

    public void JumpAttack()
    {
        nav.destination = brain.target.position;
        Debug.Log(brain.target.position);
    }

    public void EndJumpAttack()
    {
        nav.destination = brain.transform.position;
        PlayerCamera.Instance.ShakeCam(3.5f, 0.3f);
    }

    public void CheckJumpAttackHit()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, 4f, 1 << 10);

        if (col.Length > 0)
        {
            col[0].GetComponent<IDamage>().Damaged(attackPower * 1.5f, Vector3.zero);
        }
    }

    public void EndOfAttack()
    {
        isAttack = false;
    }
}
