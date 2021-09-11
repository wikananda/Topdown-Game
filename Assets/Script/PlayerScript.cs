using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region Setup
    enum State
    {
        Normal,
        DodgeRoll,
        SlashAttack
    }

    Vector3 moveDirRaw;
    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float dashSpeed = 7f;
    [SerializeField] float rollSpeed = 20f;
    [SerializeField] Rigidbody2D rigid;

    float moveX, moveY, rollX, rollY;
    float speed;
    float handleRollSpeed;

    Vector3 lastMove;
    Vector3 moveRollRaw;
    State state;

    [SerializeField] Animator animator;
    #endregion


    private void Start()
    {
        Debug.Log("PlayerController Script Starting");
        state = State.Normal;
        lastMove = new Vector3(0, -1);
        speed = movementSpeed;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Normal:
                MovementHandler();
                Dashing();
                HandleDodgeRoll();
                HandleSlashAttack();
                break;
            case State.DodgeRoll:
                DodgeRoll();
                break;
            case State.SlashAttack:
                SlashAttack();
                HandleDodgeRoll();
                break;
        }
    }

    void HandleSlashAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            state = State.SlashAttack;
            animator.SetBool("Slash", true);
            animator.SetFloat("Magnitude", 0f);
        }
    }

    void SlashAttack()
    {
        rigid.velocity = Vector2.zero;
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .9f)
        {
            animator.SetBool("Slash", false);
            state = State.Normal;
        }
    }

    void MovementHandler()
    {
        if (state != State.Normal)
            return;
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        moveDirRaw = new Vector3(moveX, moveY);
        animator.SetFloat("Horizontal", moveDirRaw.x);
        animator.SetFloat("Vertical", moveDirRaw.y);
        animator.SetFloat("Magnitude", moveDirRaw.magnitude);
        animator.SetFloat("lastMoveX", lastMove.x);
        animator.SetFloat("lastMoveY", lastMove.y);

        rigid.velocity = moveDirRaw.normalized * speed;
        if (moveDirRaw != Vector3.zero)
        {
            lastMove = moveDirRaw;
        }
    }

    void Dashing()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.speed = 1.2f;
            speed = dashSpeed;
            return;
        }
        speed = movementSpeed;
        animator.speed = 1f;
    }

    void HandleDodgeRoll()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = State.DodgeRoll;
            handleRollSpeed = rollSpeed;
            rollX = Input.GetAxis("Horizontal");
            rollY = Input.GetAxis("Vertical");
            animator.SetBool("Rolling", true);
            animator.SetBool("Slash", false);
            animator.SetFloat("Magnitude", 0);
            animator.speed = 1f;
            moveRollRaw = new Vector3(rollX, rollY);
        }
    }

    void DodgeRoll()
    {
        if(moveRollRaw == Vector3.zero)
        {
            rigid.velocity = lastMove.normalized * handleRollSpeed;
            animator.SetFloat("moveRollX", lastMove.x);
            animator.SetFloat("moveRollY", lastMove.y);
        }
        else
        {
            rigid.velocity = moveRollRaw.normalized * handleRollSpeed;
            animator.SetFloat("moveRollX", rollX);
            animator.SetFloat("moveRollY", rollY);
        }
        
        handleRollSpeed -= handleRollSpeed * 0.025f;

        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .9f)
        {
            state = State.Normal;
            animator.SetBool("Rolling", false);
        }
        
    }
}
