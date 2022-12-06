using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IDamage
{
    [Header("[중력]")]
    public float gravityScale = -9.81f;

    [Header("[움직임]")]
    public float moveSpeed = 0f;
    public float dashSpeed = 0f;
    public float dodgeSpeed = 0f;
    public float activitySpeed = 0f;
    public float turnSpeed = 0f;
    [SerializeField] private bool isGround = false;

    [Header("[체력]")]
    [SerializeField] private float maxHp = 0f;
    [SerializeField] private float currentHp = 0f;

    [Header("[공격]")]
    public float attakcPower = 0f;

    [Header("[행동]")]
    public bool isAttack = false;
    public bool isGaurd = false;
    public bool isDodge = false;
    public bool isHealing = false;

    [Header("[바닥 확인]")]
    public float groundCheckoffset = 0f;
    public float groundCheckRadius = 0;
    public LayerMask groundLayer;

    private Vector3 moveVector = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;
    private float verticalValue = 0f;

    private CharacterController cc = null;
    private PlayerAnimation animator = null;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<PlayerAnimation>();
    }

    private void Start()
    {
        currentHp = maxHp;
    }

    private void Update()
    {
        CheckGround();
        Gravity();
        Dodge();
        Move();
        Attack();
        Gaurd();
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

        if (isDodge)
        {
            moveVector = moveDir.normalized * dodgeSpeed;
        }
        else
        {
            if (Mathf.Abs(x) > 0f || Mathf.Abs(z) > 0f)
            {
                currentSpeed = moveSpeed;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    currentSpeed = dashSpeed;
                }

                if (isGaurd || isHealing)
                {
                    currentSpeed = activitySpeed;
                }

                moveVector = new Vector3(x * currentSpeed, verticalValue, z * currentSpeed);
                moveDir = new Vector3(moveVector.x, 0, moveVector.z);

                Turn(new Vector3(moveVector.x, 0, moveVector.z).normalized, false);
            }
            else
            {
                currentSpeed = 0f;
                moveVector = new Vector3(0, verticalValue, 0);
            }

            animator.MoveAnim(dashSpeed, currentSpeed);
        }
        
        cc.Move(moveVector * Time.deltaTime);                
    }

    private void Turn(Vector3 direction, bool immediately)
    {
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        if(immediately)
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), Time.deltaTime * turnSpeed);
        }      
    }

    /*private void Jump()
    {
        if(isAttack || isHealing || isJump)
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

                isGaurd = false;
                animator.EndOfGaurd();
            }
        }
    }*/

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
            }
        }
        else
        {
            verticalValue += gravityScale * Time.deltaTime;
        }
    }

    private void Attack()
    {
        if(isAttack || isHealing)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            Turn(moveDir.normalized, true);
            isAttack = true;

            animator.AttackAnim();

            isGaurd = false;
            animator.EndOfGaurd();           
        }        
    }

    private void Gaurd()
    {
        if(isAttack || isHealing)
        {
            return;
        }

        if(Input.GetMouseButtonDown(1))
        {
            Turn(moveDir.normalized, true);
            isGaurd = true;
            animator.GaurdAnim();
        }
        else if(Input.GetMouseButtonUp(1))
        {
            isGaurd = false;
            animator.EndOfGaurd();
        }
    }

    private void Dodge()
    {
        if(isAttack || isHealing || isDodge)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Turn(moveDir.normalized,true);
            isDodge = true;
            animator.DodgeAnim();
        }
    }

    public void EndOfDodge()
    {
        isDodge = false;
    }

    private void Healing()
    {
        if(isAttack || isHealing)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            //
        }
    }

    public void Damaged(int damage, Vector3 direction)
    {
        currentHp -= damage;

        //방향 따라 넉백

        if(currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.DieAnim();
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
