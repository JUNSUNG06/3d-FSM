using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("[�߷�]")]
    public float gravityScale = -9.81f;

    [Header("[�̵�]")]
    public float moveSpeed = 0f;
    public float dashSpeed = 0f;
    public float jumpHeight = 0f;
    [SerializeField] private bool isGround = false;

    [Header("[����]")]
    public float attakcPower = 0f;

    [Header("[�ൿ]")]
    public bool isJump = false;
    public bool isAttack = false;

    [Header("[�ٴ� Ȯ��]")]
    public float groundCheckoffset = 0f;
    public float groundCheckRadius = 0;
    public LayerMask groundLayer;

    private Vector3 moveVector = Vector3.zero;
    private float verticalValue = 0f;

    private CharacterController cc = null;
    private PlayerAnimation animator = null;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        CheckGround();
        Gravity();
        Jump();
        Move();
        Attack();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float currentSpeed = 0f;

        if(isAttack)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveVector = new Vector3(-x * dashSpeed, verticalValue, -z * dashSpeed) * Time.deltaTime;
            currentSpeed = dashSpeed;
        }
        else
        {
            moveVector = new Vector3(-x * moveSpeed, verticalValue, -z * moveSpeed) * Time.deltaTime;

            if (Mathf.Abs(x) > 0f || Mathf.Abs(z) > 0f)
            {
                currentSpeed = moveSpeed;
            }
            else
            {
                currentSpeed = 0f;
            }
        }

        cc.Move(moveVector);
        animator.MoveAnim(dashSpeed, currentSpeed);
    }

    private void Jump()
    {
        if(isAttack)
        {
            return;
        }

        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJump = true;
                verticalValue = Mathf.Sqrt(jumpHeight * -2 * gravityScale);

                animator.JumpAnim();
            }
        }
    }

    private void CheckGround()
    {
        Vector3 groundCheckPosition = new Vector3(transform.position.x, transform.position.y - groundCheckoffset,
            transform.position.z);

        isGround = Physics.CheckSphere(groundCheckPosition, groundCheckRadius, groundLayer, QueryTriggerInteraction.Ignore);
    }

    private void Gravity()
    {
        if (isGround)
        {
            if (verticalValue < 0f)
            {
                verticalValue = -2f;
                
                if(isJump)
                {
                    isJump = false;
                }
            }
        }
        else
        {
            verticalValue += gravityScale * Time.deltaTime;
        }
    }

    private void Attack()
    {
        if(isJump || isAttack)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            isAttack = true;

            animator.AttackAnim();
        }        
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 groundCheckPosition = new Vector3(transform.position.x, transform.position.y - groundCheckoffset,
            transform.position.z);

        Gizmos.DrawWireSphere(groundCheckPosition, groundCheckRadius);
    }
#endif
}
