using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuardMove : MonoBehaviour
{
    [SerializeField] private GuardLOS guardLOS;

    [SerializeField] private List<Vector2> patrolPoints;
    [SerializeField] private static float guardSpeed = 1f;
    [SerializeField] private static float rotateSpeed = -1f;

    private float nextAngle;
    private float currAngle;
    private float distance;
    private int currIndex;

    private bool isMoving = false;
    void Start()
    {
        this.transform.position = patrolPoints[0];
        guardLOS = GetComponentInChildren<GuardLOS>();
    }

    void Update()
    {
        if (!isMoving) 
        {
            isMoving = true;
            NextPoint();
        }
    }

    private void NextPoint()
    {
        // get current location
        Vector2 currentPos = patrolPoints[currIndex];

        // increment index, accounting for overflow
        currIndex++;
        if (currIndex >= patrolPoints.Count) currIndex = 0;

        // get next location
        Vector2 nextPos = patrolPoints[currIndex];

        // calculate distance
        distance = Vector2.Distance(currentPos, nextPos);

        // calculate angle between vectors
        nextAngle = Vector2.SignedAngle(currentPos, nextPos);

        StartCoroutine(RotateGuard());
    }

    private IEnumerator RotateGuard()
    {
        Debug.Log(nextAngle + " " + currAngle);
        for (float i = (Mathf.Abs(nextAngle - currAngle)) / rotateSpeed; i >= 0; i-= 1)
        {
            // rotate the vision cone
            guardLOS.IncrAimDirection(rotateSpeed);
            // rotate the guard
            yield return new WaitForSeconds(0.016f);
        }
        yield return new WaitForSeconds(1f);
        //StartCoroutine(MoveGuard());
    }

    private IEnumerator MoveGuard()
    {
        for (float i = distance / guardSpeed; i >= 0; i--)
        {
            // move the guard
            transform.position += new Vector3(0, guardSpeed, 0);
            // move the vision cone
            guardLOS.SetOrigin(transform.position);
            yield return new WaitForSeconds(0.016f);
        }

    }

    private Vector3 AngleToVector3XY(float angleInDegrees)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

        float x = Mathf.Cos(angleInRadians);
        float y = Mathf.Sin(angleInRadians);

        return new Vector3(x, y, 0f).normalized;
    }
}
