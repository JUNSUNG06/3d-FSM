using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDistance : AIDecision
{
    [SerializeField] private float distance = 0f;
    [SerializeField] private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player").gameObject;
    }

    public override bool MakeADecision()
    {
        return Vector3.Distance(player.transform.position, transform.position) < distance;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, distance);
    }
#endif
}
