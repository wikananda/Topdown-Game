using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    private Vector3 dir;
    internal Vector3 dirDash;
    Animator anim;
    private int xValues, yValues, xDashValues, yDashValues;

    
    [SerializeField]
    PlayerScript playerScript;

    #region MoveAnim Setup
    private string[] vTitles = { "WalkDown", "" ,"WalkUp" };
    private string[] hTitles = { "WalkLeft", "" ,"WalkRight" };
    private string[] dUpTitles = { "UpLeft", "" ,"UpRight" };
    private string[] dDownTitles = { "DownLeft", "" ,"DownRight" };
    #endregion

    #region DashAnim Setup
    private string[] vDash = { "DashDown", "", "DashUp" };
    private string[] hDash = { "DashLeft", "", "DashRight" };
    private string[] dUpDash = { "DashUpLeft", "", "DashUpRight"};
    private string[] dDownDash = { "DashDownLeft", "", "DashDownRight"};
    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Debug.Log("Animation Script Starting");
    }

    internal void PlayMoveAnim()
    {
        // Move Animation Indicator
        dir = playerScript.playerControl.moveDirRaw;
        xValues = (int)dir.x;
        yValues = (int)dir.y;
        
        // Move Animation Condition
        if(dir.y == 0)
        {
            anim.Play(hTitles[1 + xValues]);
        }
        else if(dir.x == 0)
        {
            anim.Play(vTitles[1 + yValues]);
        }        
        else if(dir.y == 1 && dir.x != 0)
        {
            anim.Play(dUpTitles[1 + xValues]);
        }
        else if(dir.y == -1 && dir.x != 0){
            anim.Play(dDownTitles[1 + xValues]);
        }
    }

    internal void PlayDashAnim()
    {
        dirDash = playerScript.lastMove;
        xDashValues = (int)dirDash.x;
        yDashValues = (int)dirDash.y;

        if (dirDash.y == 0)
        {
            anim.Play(hDash[1 + xDashValues]);
        }
        else if (dirDash.x == 0)
        {
            anim.Play(vDash[1 + yDashValues]);
        }
        else if (dirDash.y == 1 && dirDash.x != 0)
        {
            anim.Play(dUpDash[1 + xDashValues]);
        }
        else if (dirDash.y == -1 && dirDash.x != 0)
        {
            anim.Play(dDownTitles[1 + xDashValues]);
        }
    }
}
