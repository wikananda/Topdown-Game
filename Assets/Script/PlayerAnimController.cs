using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Vector3 dir;
    Vector3 dirRoll;
    Animator anim;
    private int xValues, yValues, xRollValues, yRollValues;


    [SerializeField]
    PlayerScript playerScript;

    #region MoveAnim Setup
    private string[] vTitles = { "WalkDown", "", "WalkUp" };
    private string[] hTitles = { "WalkLeft", "", "WalkRight" };
    private string[] dUpTitles = { "UpLeft", "", "UpRight" };
    private string[] dDownTitles = { "DownLeft", "", "DownRight" };
    #endregion

    #region RollAnim Setup
    string[] vRoll = { "RollDown", "", "RollUp" };
    string[] hRoll = { "RollLeft", "", "RollRight" };
    string[] dUpRoll = { "RollUpLeft", "", "RollUpRight" };
    string[] dDownRoll = { "RollDownLeft", "", "RollDownRight" };
    #endregion


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Debug.Log("PlayerAnimController Script Starting");
    }

    internal void PlayMoveAnim(Vector3 dir)
    {
        // Move Animation Indicator
        xValues = (int)dir.x;
        yValues = (int)dir.y;

        // Move Animation Condition
        if (dir.y == 0)
        {
            anim.Play(hTitles[1 + xValues]);
        }
        else if (dir.x == 0)
        {
            anim.Play(vTitles[1 + yValues]);
        }
        else if (dir.y == 1 && dir.x != 0)
        {
            anim.Play(dUpTitles[1 + xValues]);
        }
        else if (dir.y == -1 && dir.x != 0)
        {
            anim.Play(dDownTitles[1 + xValues]);
        }
    }

    internal void PlayRollAnim(Vector3 dirRoll)
    {
        xRollValues = (int)dirRoll.x;
        yRollValues = (int)dirRoll.y;

        if (dirRoll.y == 0)
        {
            anim.Play(hRoll[1 + xRollValues]);
        }
        else if (dirRoll.x == 0)
        {
            anim.Play(vRoll[1 + yRollValues]);
        }
        else if (dirRoll.y == 1 && dirRoll.x != 0)
        {
            anim.Play(dUpRoll[1 + xRollValues]);
        }
        else if (dirRoll.y == -1 && dirRoll.x != 0)
        {
            anim.Play(dDownRoll[1 + xRollValues]);
        }
    }
}
