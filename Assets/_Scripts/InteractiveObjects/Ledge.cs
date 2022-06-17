using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    public Transform handPosition;
    public Transform standPosition;

    private float yOffset = 6.5f;

    private Vector3 newHandPos;

    private void Start()
    {
        newHandPos = new Vector3(handPosition.position.x, handPosition.position.y - yOffset, handPosition.position.z);
    }

    public Vector3 GetStandPosition()
    {
        return standPosition.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge Checker"))
        {
            Player player = other.GetComponentInParent<Player>();
            player.playerState = PlayerState.CLIMBING;
            player.HandleClimb(newHandPos, this);
        }
    }
}
