using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform[] waypoints;
    public int speed;
    public float duration;

    private int wayPointIndex;

    private void Start()
    {
        wayPointIndex = 0;
        transform.LookAt(waypoints[wayPointIndex].position);
        StartCoroutine(PatrolRoutine());
    }
    
    float CalculateDistance()
    {
        float distance = Vector3.Distance(transform.position, waypoints[wayPointIndex].position);
        return distance;
    }

    IEnumerator PatrolRoutine()
    {
        float percent = 0;
        WaitForFixedUpdate update = new WaitForFixedUpdate();

        while (percent < duration)
        {
            percent += Time.deltaTime;
            transform.position = Vector3.Lerp(this.transform.position, waypoints[wayPointIndex].position, percent / speed);
            yield return update;
        }

        if (CalculateDistance() < 1f)
        {
            IncreaseIndex();
            StartCoroutine(PatrolRoutine());
        }
    }

    void IncreaseIndex()
    {
        wayPointIndex++;

        if (wayPointIndex >= waypoints.Length)
        {
            wayPointIndex = 0;
        }

        transform.LookAt(waypoints[wayPointIndex].position);
    }

    public void StopMovement()
    {
        StopCoroutine(PatrolRoutine());
    }
}
