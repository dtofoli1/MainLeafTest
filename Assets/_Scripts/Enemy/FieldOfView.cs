using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            other.GetComponent<Player>().controller.enabled = false;
            StartCoroutine(PlayerCaughtRoutine());
        }
    }

    IEnumerator PlayerCaughtRoutine()
    {
        this.GetComponentInParent<Guard>().StopMovement();
        UIManager.instance.UpdateTitle("The guards found you");
        yield return new WaitForSeconds(3f);
        GameManager.instance.LoadScene(0);
    }
}
