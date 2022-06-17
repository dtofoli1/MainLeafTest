using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator animator;

    public void StateControl(string parameter = "")
    {
        for (int i = 0; i < animator.parameterCount; i++)
        {
            animator.SetBool(animator.parameters[i].name, animator.parameters[i].name == parameter);
        }
    }
}
