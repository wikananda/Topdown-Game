using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    enum State
    {
        Normal,
        DodgeRoll
    }

    #region Setup
    Vector3 moveDirRaw;
    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float dashSpeed = 7f;
    [SerializeField] float rollSpeed = 20f;
    [SerializeField] Rigidbody2D rigid;

    float moveX, moveY;
    float speed;
    float handleRollSpeed;
    Vector3 lastMove;
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
                break;
            case State.DodgeRoll:
                DodgeRoll();
                break;
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

        rigid.velocity = moveDirRaw.normalized * speed;
        Debug.Log(moveDirRaw);
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
            animator.SetBool("Rolling", true);
        }
    }

    void DodgeRoll()
    {
        animator.SetFloat("RollX", lastMove.x);
        animator.SetFloat("RollY", lastMove.y);
        animator.SetFloat("Magnitude", 0);
        animator.speed = 1f;
        
        rigid.velocity = lastMove.normalized * handleRollSpeed;
        handleRollSpeed -= handleRollSpeed * 0.05f;
        
        if(handleRollSpeed < 5f)
        {
            state = State.Normal;
            animator.SetBool("Rolling", false);
        }
    }
}
