using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private int jumpHash = Animator.StringToHash("Jump");
    private int movementHash = Animator.StringToHash("Movement");

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
}
