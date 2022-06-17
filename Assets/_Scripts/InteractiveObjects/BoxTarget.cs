using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTarget : MonoBehaviour
{
    public Vector3 position;

    private void Awake()
    {
        position = this.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InteractableBox>())
        {
            InteractableBox interactableBox = other.gameObject.GetComponent<InteractableBox>();
            StopCoroutine(LockBox(interactableBox));
            StartCoroutine(LockBox(interactableBox));
        }
    }

    private IEnumerator LockBox(InteractableBox interactableBox)
    {
        float percent = 0;
        WaitForFixedUpdate update = new WaitForFixedUpdate();
        interactableBox.locked = true;

        while (percent < 3f)
        {
            percent += Time.deltaTime;
            interactableBox.rb.MovePosition(Vector3.Lerp(interactableBox.transform.position, position, percent / 3f));
            yield return update;
        }

        interactableBox.rb.constraints = RigidbodyConstraints.FreezePosition;
        yield return update;
        GameManager.instance.ActivateNextTarget();
        GameManager.instance.SaveBoxPosition(this.position);
    }
}
