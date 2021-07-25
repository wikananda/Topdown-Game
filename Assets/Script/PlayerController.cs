using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Setup
    private Rigidbody2D rigid;
    internal bool isDashDown;
    internal float dashDuration;
    internal bool canMove;

    internal Vector3 moveDirRaw;
    private Animator anim;

    [SerializeField]
    PlayerScript playerScript;
    #endregion

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    private void Start()
    {
        Debug.Log("Player Script Starting");
    }

    internal void MovementHandler()
    {
        float moveX = 0;
        float moveY = 0;

        if (canMove == false)
        {
            return;
        }

        // Input Reading
        if (Input.GetKey(KeyCode.W))
        {
            moveY += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX -= 1;
        }

        moveDirRaw = new Vector3(moveX, moveY);

        // Dashing

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dashDuration = 0f;
            isDashDown = true;
        }

        // Rolling ...
    }
}
