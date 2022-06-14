using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Movement
{
    public static float GetAxisDirection(string axis)
    {
        return Input.GetAxisRaw(axis);
    }

    public static Vector3 GetDirection(float horizontal, float vertical)
    {
        return new Vector3(horizontal, 0f, vertical).normalized;
    }
}
