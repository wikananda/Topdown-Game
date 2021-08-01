using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Setup
    internal float runDuration;
    internal float rollDuration;
    internal bool canMove;
    internal bool canRoll;

    internal Vector3 moveDirRaw;
    private Animator anim;

    [SerializeField]
    PlayerScript playerScript;
    #endregion


    private void Start()
    {
        Debug.Log("PlayerController Script Starting");
        canMove = true;
        canRoll = false;
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

        // Rolling
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rollDuration = 0f;
            canRoll = true;
        }
    }
}
