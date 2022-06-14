using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook playerCam;

    public void CameraLookAt(Transform target, bool lockCamera = false)
    {
        playerCam.LookAt = target;
        playerCam.Follow = target;
        if (lockCamera)
        {
            playerCam.ForceCameraPosition(target.position, Quaternion.identity);
        }
    }
}
