using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Movement
{
    public static Vector3 GetDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        return new Vector3(horizontal, 0f, vertical).normalized;
    }
}
