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
    public int healCount = 0;

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
    private Vector3 dodgeDir = Vector3.zero;
    private Vector3 forward = Vector3.zero;
    private float verticalValue = 0f;

    private CharacterController cc = null;
    public PlayerAnimation animator = null;
    private PlayerPower powerControl = null;
    private PlayerHealth healthControl = null;
    public Transform cameraArm;

    private bool dodgeToCameraDir = false;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<PlayerAnimation>();
        powerControl = GetComponent<PlayerPower>();
        healthControl = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        CheckGround();
        Gravity();
        Dodge();
        Move();
        Attack();
        Gaurd();
        Healing();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float currentSpeed = 0f;
        forward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z).normalized;

        if(isAttack || isHealing || isGaurd)
        {
            return;
        }

        if (isDodge)
        {
            if((Mathf.Abs(x) <= 0f && Mathf.Abs(z) <= 0f))
            {
                moveVector = dodgeDir * dodgeSpeed;
                dodgeToCameraDir = true;
                Turn(dodgeDir, true);
            }
            else
            {
                if(!dodgeToCameraDir)
                {
                    moveVector = moveDir * dodgeSpeed;
                    Turn(moveDir, true);
                } 
            }
        }
        else
        {
            if (Mathf.Abs(x) > 0f || Mathf.Abs(z) > 0f)
            {
                currentSpeed = moveSpeed;

                if (Input.GetKey(KeyCode.LeftShift) && powerControl.CanActive(dashNPower * Time.deltaTime))
                {
                    currentSpeed = dashSpeed;
                    powerControl.DecreasePower(dashNPower * Time.deltaTime);
                }

                if (isGaurd || isHealing)
                {
                    currentSpeed = activitySpeed;
                }

                Vector3 cameraArmRight = new Vector3(cameraArm.right.x, 0, cameraArm.right.z);
                moveVector = (forward * z + cameraArmRight * x) * currentSpeed;
                moveDir = moveVector.normalized;
                Turn(moveVector.normalized, false);
            }
            else
            {
                currentSpeed = 0f;
                moveVector = new Vector3(0, verticalValue, 0);
            }

            dodgeDir = forward;
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
        if(isAttack || isHealing || !powerControl.CanActive(attackNPower))
        {
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            Turn(forward, true);
            isAttack = true;
            powerControl.DecreasePower(attackNPower);

            animator.AttackAnim();

            isGaurd = false;
            animator.EndOfGaurd();           
        }        
    }

    private void Gaurd()
    {
        if (!powerControl.CanActive(gaurdNPower))
        {
            isGaurd = false;
            animator.EndOfGaurd();
        }

        if (isAttack || isHealing || !powerControl.CanActive(gaurdNPower))
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
        if(isAttack || isHealing || isDodge || !powerControl.CanActive(dodgeNPower))
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
        dodgeToCameraDir = false;
    }

    private void Healing()
    {
        if (isAttack || isHealing || isDodge || isGaurd || healCount <= 0)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.HealingAnim();
            isHealing = true;
            healthControl.Damaged(-healthControl.maxHealth * 7 * 0.1f, Vector3.zero);
            healCount--;
        }
    }

    public void EndOfHealing()
    {
        isHealing = false;
    }

    public void CheckAttack()
    {
        Collider[] col = Physics.OverlapSphere(transform.position + transform.forward * 0.7f + Vector3.up, 0.35f, 1 << 9);

        if(col.Length > 0)
        {
            col[0].GetComponent<IDamage>().Damaged(attakcPower, Vector3.zero);
            PlayerCamera.Instance.ShakeCam(3, 0.1f);
        }
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
