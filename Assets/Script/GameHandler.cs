using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] CameraFollow cameraFollow;
    [SerializeField] Transform playerTransform;
    internal float avgFPS;
    //[SerializeField] int targetFPS = 5;
    void Start()
    {
        //Application.targetFrameRate = targetFPS;
        Debug.Log("GameHandler Script is starting");
        cameraFollow.setCameraFollow(() => playerTransform.position);
    }

}
