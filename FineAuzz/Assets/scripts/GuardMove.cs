using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuardMove : MonoBehaviour
{
    [SerializeField] private GuardLOS guardLOS;

    [SerializeField] private List<Vector2> patrolPoints;
    [SerializeField] private static float guardSpeed = 1f;
    [SerializeField] private static float rotateSpeed = 1f;

    private float nextAngle;
    private float currAngle;
    private float distance;
    private int currIndex;

    private bool isMoving = false;

    Vector2 currentPos;
    Vector2 nextPos;
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
        currentPos = patrolPoints[currIndex];

        // increment index, accounting for overflow
        currIndex++;
        if (currIndex >= patrolPoints.Count) currIndex = 0;

        // get next location
        nextPos = patrolPoints[currIndex];

        StartCoroutine(RotateGuard());
    }

    private IEnumerator RotateGuard()
    {
        Vector2 direction = (nextPos - currentPos).normalized;

        float targetAngle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        targetAngle /= 2;

        if (targetAngle < 0) targetAngle += 360f;

        while (Mathf.Abs(Mathf.DeltaAngle(currAngle, targetAngle)) > 0.5f)
        {
            float delta = Mathf.DeltaAngle(currAngle, targetAngle);

            float step = Mathf.Sign(delta) * rotateSpeed;

            currAngle += step;
            guardLOS.IncrAimDirection(step);

            yield return new WaitForSeconds(0.016f);
        }

        currAngle = targetAngle;
        // StartCoroutine(MoveGuard());
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
    public static Vector3 DegreeToVector3(float degree)
    {
        float radian = degree * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(radian), Mathf.Sin(radian));
    }
}
