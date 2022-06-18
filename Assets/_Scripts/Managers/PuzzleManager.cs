using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] boxTargets;

    public void ActivateNextTarget()
    {
        boxTargets[0].GetComponent<BoxTarget>().boxCollider.enabled = false;

        boxTargets[1].GetComponent<BoxTarget>().boxCollider.enabled = true;
    }
}