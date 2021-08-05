using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Setup
    Func<Vector3> getCameraFollowPos;
    Vector3 cameraPos;
    Vector3 cameraMoveDir;
    Vector3 newCameraPos;
    float distance;
    float distanceNewCameraPos;
    [SerializeField] float cameraSpeed = 4f;
    [SerializeField] bool smoothCamera;
    #endregion

    private void Start()
    {
        Debug.Log("CameraFollow Script is starting");
        smoothCamera = true;
    }

    private void Update()
    {
        cameraFollowing(smoothCamera);
    }
    void cameraFollowing(bool smoothCamera)
    {
        cameraPos = getCameraFollowPos();
        cameraPos.z = transform.position.z;
        if (!smoothCamera)
        {
            transform.position = cameraPos;
            return;
        }
        cameraMoveDir = (cameraPos - transform.position).normalized;
        distance = Vector3.Distance(cameraPos, transform.position);

        if (distance > 0)
        {
            newCameraPos = transform.position + cameraMoveDir * distance * cameraSpeed * Time.deltaTime;
            distanceNewCameraPos = Vector3.Distance(newCameraPos, cameraPos);

            if (distanceNewCameraPos > distance)
            {
                //Camera overshoot the target
                newCameraPos = cameraPos;
            }
            transform.position = newCameraPos;
        }

    }

    internal void setCameraFollow(Func<Vector3> getCameraFollowPos)
    {
        this.getCameraFollowPos = getCameraFollowPos;
    }
}
