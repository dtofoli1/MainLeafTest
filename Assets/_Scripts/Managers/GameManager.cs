using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> boxTargets = new List<GameObject>();
    public List<GameObject> boxes = new List<GameObject>();
    public List<Vector3> boxPositions = new List<Vector3>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < boxPositions.Count; i++)
        {
            boxes[i].transform.position = boxPositions[i];
        }
    }

    public void ActivateNextTarget()
    {
        boxTargets[0].SetActive(false);
        boxTargets[1].SetActive(true);
    }

    public void SaveBoxPosition(Vector3 boxPosition)
    {
        boxPositions.Add(boxPosition);
    }
}