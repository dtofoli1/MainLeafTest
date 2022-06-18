using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : InteractiveObject
{
    public Transform cameraTarget;
    public override void Interaction(Player player)
    {
        player.cameraController.CameraLookAt(cameraTarget);
        player.cameraController.Recenter(true);
        player.animationStateController.StateControl("isCrouching", true);
        StartCoroutine(EnterHoleRoutine(player));
    }

    private IEnumerator EnterHoleRoutine(Player player)
    {
        player.controller.enabled = false;
        player.transform.LookAt(cameraTarget);
        yield return new WaitForSeconds(1);
        player.animationStateController.StateControl("isMoving", true);

        float percent = 0;
        WaitForFixedUpdate update = new WaitForFixedUpdate();

        while (percent < 1.5f)
        {
            float step = player.speed * percent * Time.deltaTime;
            percent += Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(player.transform.position, cameraTarget.position, step);
            yield return update;
        }

        player.transform.position = cameraTarget.position;
        GameManager.instance.LoadScene(1);
    }
}