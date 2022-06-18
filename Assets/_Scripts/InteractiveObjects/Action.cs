using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public string actionName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            UIManager.instance.UpdateAction(actionName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UIManager.instance.UpdateAction("");
    }
}
