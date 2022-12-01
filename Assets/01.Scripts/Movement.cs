using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("[중력]")]
    public float gravityScale = -9.81f;

    [Header("[이동]")]
    public float moveSpeed = 0f;
    public float dashSpeed = 0f;
    public float jumpHeight = 0f;
    [SerializeField] private bool isGround = false;

    [Header("[바닥 확인]")]
    public float groundCheckoffset = 0f;
    public float groundCheckRadius = 0;
    public LayerMask groundLayer;

    private Vector3 moveVector = Vector3.zero;
    private float verticalValue = 0f;

    private CharacterController cc;
    private PlayerAnimation animator;

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
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float currentSpeed;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveVector = new Vector3(-x * dashSpeed, verticalValue, -z * dashSpeed) * Time.deltaTime;
            currentSpeed = dashSpeed;
        }
        else
        {
            moveVector = new Vector3(-x * moveSpeed, verticalValue, -z * moveSpeed) * Time.deltaTime;
            currentSpeed = moveSpeed;
        }

        animator.MoveAnim(dashSpeed, currentSpeed);
        cc.Move(moveVector);
    }

    private void Jump()
    {
        if(isGround)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
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
        if(isGround)
        {
            if(verticalValue < 0f)
            {
                verticalValue = -2f;
            }
        }
        else
        {
            verticalValue += gravityScale * Time.deltaTime;
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
