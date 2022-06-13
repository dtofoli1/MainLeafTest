using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBox : InteractiveObject
{
    public override void Interaction(Player player)
    {
        Debug.Log("Box Interaction");
    }
}
