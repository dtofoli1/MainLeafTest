using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook playerCam;

    public void CameraLookAt(Transform target)
    {
        playerCam.LookAt = target;
        playerCam.Follow = target;
    }

    public void Recenter(bool lockCamera)
    {
        playerCam.m_RecenterToTargetHeading.m_enabled = lockCamera;
        playerCam.m_YAxisRecentering.m_enabled = lockCamera;
    }
}