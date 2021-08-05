using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region Setup
    private Rigidbody2D rigid;
    Vector3 moveDir;
    internal Vector3 lastMove;

    float rollCooldown = .1f;
    float timeLastRoll = 0f;

    [SerializeField]
    float rollSpeed;

    [SerializeField]
    float runSpeed = 7.5f;

    [SerializeField]
    float speed = 4f;

    [SerializeField]
    internal PlayerAnimController animControl;

    [SerializeField]
    internal PlayerController playerControl;

    [SerializeField] float rollLength = 2f;
    #endregion

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Debug.Log("PlayerScript Starting");
        lastMove = new Vector3(0, -1);
    }

    private void Update()
    {
        rollSpeed = 10f;
        playerControl.MovementHandler();
        moveDir = playerControl.moveDirRaw.normalized;
        if (moveDir != new Vector3(0, 0))
        {
            lastMove = playerControl.moveDirRaw;
        }
        if (playerControl.canRoll == false)
        {
            animControl.PlayMoveAnim(playerControl.moveDirRaw);
        }
        
    }

    private void FixedUpdate()
    {
        // Checking to run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rollSpeed = 12.5f;
            rigid.velocity = moveDir * runSpeed;
            Rolling();
            return;
        }

        rigid.velocity = moveDir * speed;
        Rolling();
    }

    // BUG WHEN GAME RUNS IN LOW FPS < 10, ROLLING ANIMATION SOMETIMES WONT SHOW.
    #region Rolling
    void Rolling()
    {
        if(playerControl.canRoll == false || Time.time - timeLastRoll < rollCooldown)
        {
            playerControl.canRoll = false;
            return;
        }

        playerControl.canMove = false;

        if (playerControl.rollDuration < rollLength)
        {
            rigid.velocity = lastMove.normalized * rollSpeed;
            animControl.PlayRollAnim(lastMove);
            playerControl.rollDuration += rollLength/13.3f;
            return;
        }
        playerControl.canRoll = false;
        playerControl.canMove = true;
        timeLastRoll = Time.time;
    }
    #endregion
}
