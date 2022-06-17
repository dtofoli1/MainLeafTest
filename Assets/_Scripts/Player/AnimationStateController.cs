using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator animator;

    public void StateControl(string parameter = "", bool state = false)
    {
        animator.SetBool(parameter, state);
    }

    public void ClearParameters()
    {
        for (int i = 0; i < animator.parameterCount; i++)
        {
            animator.SetBool(animator.parameters[i].name, false);
        }
    }
}
