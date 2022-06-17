using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    public string actionName;
    public abstract void Interaction(Player player);
}
