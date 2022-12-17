using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnim : MonoBehaviour
{
    private Animator animator;

    private int movementHash = Animator.StringToHash("Movement");

    public float animChangeSpeed = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void IdleAnim()
    {
        animator.SetFloat(movementHash, Mathf.Lerp(animator.GetFloat(movementHash), 0f, Time.deltaTime * animChangeSpeed));
    }

    public void MoveAnim()
    {
        animator.SetFloat(movementHash, Mathf.Lerp(animator.GetFloat(movementHash), 1, Time.deltaTime * animChangeSpeed));
    }
}
