using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public float ChangeMovementAnimSpeed = 0f;

    private Animator animator;
    private PlayerController playerController;

    private int movementHash = Animator.StringToHash("Movement");
    private int attackHash = Animator.StringToHash("Attack");
    private int gaurdHash = Animator.StringToHash("Gaurd");
    private int dieHash = Animator.StringToHash("Die");
    private int healingHash = Animator.StringToHash("Healing");
    private int dodgeHash = Animator.StringToHash("Dodge");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    public void MoveAnim(float maxSpeed, float currentSpeed)
    {
        float threshold = currentSpeed / maxSpeed;

        animator.SetFloat(movementHash, Mathf.Lerp(animator.GetFloat(movementHash), threshold, Time.deltaTime * ChangeMovementAnimSpeed));
    }

    public void AttackAnim()
    {
        animator.SetTrigger(attackHash);
    }

    public void EndOfAttack()
    {
        playerController.isAttack = false;
    }

    public void GaurdAnim()
    {
        animator.SetBool(gaurdHash, true);
    }

    public void EndOfGaurd()
    {
        animator.SetBool(gaurdHash, false);
    }

    public void DieAnim()
    {
        animator.SetTrigger(dieHash);
    }

    public void HealingAnim()
    {
        animator.SetTrigger(healingHash);
    }   

    public void DodgeAnim()
    {
        animator.SetTrigger(dodgeHash);
    }
}
