using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossIdleAction : AIAction
{
    private BossAnim anim;

    public float turnSpeed;

    protected override void Start()
    {
        base.Start();

        anim = brain.GetComponent<BossAnim>();
    }

    public override void TakeAction()
    {
        Idle();
    }    

    private void Idle()
    {
        anim.IdleAnimImmediately();
        Turn();
    }

    private void Turn()
    {
        Vector3 dir = (brain.target.position - brain.transform.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        brain.transform.localRotation = Quaternion.Lerp(brain.transform.localRotation, Quaternion.Euler(0, angle, 0),
            Time.deltaTime * turnSpeed);
    }
}
