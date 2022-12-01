using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;

    private int jumpHash = Animator.StringToHash("Jump");
    private int movementHash = Animator.StringToHash("Movement");
    private int attackHash = Animator.StringToHash("Attack");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    public void MoveAnim(float maxSpeed, float currentSpeed)
    {
        float threshold = currentSpeed / maxSpeed;

        animator.SetFloat(movementHash, threshold);
    }

    public void JumpAnim()
    {
        animator.SetTrigger(jumpHash);
    }

    public void AttackAnim()
    {
        animator.SetTrigger(attackHash);
    }

    public void EndOfAttack()
    {
        playerController.isAttack = false;
    }
}
