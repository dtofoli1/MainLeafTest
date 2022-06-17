using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBox : InteractiveObject
{
    public bool locked = false;
    public Transform cameraTarget;
    public Rigidbody rb;
    public override void Interaction(Player player)
    {
        if (locked)
        {
            return;
        }
        player.cameraController.CameraLookAt(cameraTarget);
        player.cameraController.Recenter(true);
        MoveBox(player);
    }

    public void MoveBox(Player player)
    {
        Vector3 pushDirection = Movement.GetDirection() + (Movement.GetDirection() * 0.2f);

        if (Mathf.Sign(pushDirection.z) == 0)
        {
            return;
        }
        else if (Mathf.Sign(pushDirection.z) < 0)
        {
            player.playerState = PlayerState.PULLING;
            player.animationStateController.StateControl("isPulling", true);
            player.animationStateController.StateControl("isPushing", false);
        }
        else
        {
            player.playerState = PlayerState.PUSHING;
            player.animationStateController.StateControl("isPulling", false);
            player.animationStateController.StateControl("isPushing", true);
        }

        rb.velocity = pushDirection;
    }
}