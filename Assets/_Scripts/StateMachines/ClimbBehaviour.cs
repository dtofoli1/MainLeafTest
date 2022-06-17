using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player player = animator.transform.parent.GetComponent<Player>();
        player.ClimbLedge();
    }
}
