using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBox : InteractiveObject
{
    public float speed = 0.5f;
    public bool locked = false;
    public Rigidbody rb;
    public override void Interaction(Player player)
    {
        if (locked)
        {
            return;
        }
        player.cameraController.CameraLookAt(this.transform, true);
        MoveBox();
    }

    public void MoveBox()
    {
        float horizontal = Movement.GetAxisDirection("Horizontal");
        float vertical = Movement.GetAxisDirection("Vertical");
        Vector3 direction = Movement.GetDirection(horizontal, vertical);

        if (direction.magnitude >= 0.1f)
        {
            Debug.Log(direction);
            rb.AddForce(direction, ForceMode.Force);
            //this.transform.position += direction;
        }
    }
}
