using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region Setup
    private Rigidbody2D rigid;
    Vector3 moveDir;
    internal Vector3 lastMove;

    float dashCooldown = .6f;
    float timeLastDash = 0;

    [SerializeField]
    float dashSpeed = 20f;

    [SerializeField]
    float speed = 4f;

    [SerializeField]
    internal AnimationController animControl;

    [SerializeField]
    internal PlayerController playerControl;
    #endregion

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Debug.Log("Player Script Starting");
        lastMove = new Vector3(0, -1);
    }

    private void Update()
    {
        playerControl.MovementHandler();
        moveDir = playerControl.moveDirRaw.normalized;
        if(moveDir != new Vector3(0, 0))
        {
            lastMove = moveDir;
        }
        //Debug.Log(lastMove);
        animControl.PlayMoveAnim();
    }

    private void FixedUpdate()
    {
        rigid.velocity = moveDir * speed;
        Dashing(); 
    }

    void Dashing()
    {
        if (playerControl.isDashDown == false || Time.time - timeLastDash < dashCooldown)
        {
            playerControl.isDashDown = false;
            return;
        }

        playerControl.canMove = false;
        Debug.Log(animControl.dirDash);

        if (playerControl.dashDuration <= 1f)
        {
            rigid.velocity = lastMove * dashSpeed;
            animControl.PlayDashAnim();
            playerControl.dashDuration += 0.1f;
            return;
        }

        timeLastDash = Time.time;
        playerControl.isDashDown = false;
        playerControl.canMove = true;

    }

}
