using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
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

    [Header("[공격]")]
    public float attakcPower = 0f;

    [Header("[행동]")]
    public bool isAttack = false;
    public bool isGaurd = false;
    public bool isDodge = false;
    public bool isHealing = false;

    [Header("[행동별 필요 힘]")]
    public float dashNPower = 0;
    public float dodgeNPower = 0f;
    public float gaurdNPower = 0f;
    public float attackNPower = 0f;

    [Header("[바닥 확인]")]
    public float groundCheckoffset = 0f;
    public float groundCheckRadius = 0;
    public LayerMask groundLayer;

    private Vector3 moveVector = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;
    private float verticalValue = 0f;

    private CharacterController cc = null;
    private PlayerAnimation animator = null;
    private PlayerPower powerControl = null;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<PlayerAnimation>();
        powerControl = GetComponent<PlayerPower>();
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

            if(moveDir == Vector3.zero)
            {
                moveVector = transform.forward * dodgeSpeed;
            }
        }
        else
        {
            if (Mathf.Abs(x) > 0f || Mathf.Abs(z) > 0f)
            {
                currentSpeed = moveSpeed;

                if (Input.GetKey(KeyCode.LeftShift) && powerControl.canActive(dashNPower * Time.deltaTime))
                {
                    currentSpeed = dashSpeed;
                    powerControl.DecreasePower(dashNPower * Time.deltaTime);
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

        moveVector.y = verticalValue;

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

    #region 점프(폐기)
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
    #endregion

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
        if(isAttack || isHealing || !powerControl.canActive(attackNPower))
        {
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            Turn(moveDir.normalized, true);
            isAttack = true;
            powerControl.DecreasePower(attackNPower);

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
        if(isAttack || isHealing || isDodge || !powerControl.canActive(dodgeNPower))
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Turn(moveDir.normalized,true);
            isDodge = true;
            powerControl.DecreasePower(dodgeNPower);
            animator.DodgeAnim();
        }
    }

    public void EndOfDodge()
    {
        isDodge = false;
    }

    public void CheckAttack()
    {
        Collider[] col = Physics.OverlapSphere(transform.position + Vector3.forward + Vector3.up, 0.5f, 1 << 9);

        if(col.Length > 0)
        {
            col[0].GetComponent<IDamage>().Damaged(attakcPower, Vector3.zero);
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
        Gizmos.DrawWireSphere(transform.position + transform.forward * 0.7f + Vector3.up, 0.35f);
    }
#endif
}
