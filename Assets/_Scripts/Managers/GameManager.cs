using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> boxTargets = new List<GameObject>();
    public List<Vector3> boxPositions = new List<Vector3>();

    private void Awake()
    {
        instance = this;
    }

    public void ActivateNextTarget()
    {
        boxTargets[0].SetActive(false);
        boxTargets[1].SetActive(true);
    }

    private void SaveBoxPosition(Vector3 boxPosition)
    {
        boxPositions.Add(boxPosition);
    }
}