using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;

public class GuardMove : MonoBehaviour
{
    [SerializeField] private GuardLOS guardLOS;

    [SerializeField] private List<Vector2> patrolPoints;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotateSpeed = 30f;
    [SerializeField] private float waitTime = 1f;

    private float nextAngle;
    private float currAngle;
    private float distance;
    private int currIndex;

    private bool isMoving = false;

    Vector2 currentPos;
    Vector2 nextPos;
    void Start()
    {
        transform.position = patrolPoints[0];
        guardLOS = GetComponentInChildren<GuardLOS>();

        Vector2 facing = transform.right.normalized;
        currAngle = Mathf.Atan2(facing.y, facing.x);
        if (currAngle < 0) currAngle += 360;

        StartCoroutine(PatrolLoop());
    }

    private IEnumerator PatrolLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            int nextIndex = (currIndex + 1) % patrolPoints.Count;
            Vector2 nextPos = patrolPoints[nextIndex];

            yield return RotateToFace(nextPos);
            yield return MoveTo(nextPos);
            currIndex = nextIndex;
        }
    }

    private IEnumerator RotateToFace(Vector2 targetPos)
    {
        Vector2 direction = (targetPos - (Vector2)transform.position).normalized;

        float targetAngle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (targetAngle < 0) targetAngle += 360f;

        while (Mathf.Abs(Mathf.DeltaAngle(currAngle, targetAngle)) > 0.5f)
        {
            float delta = Mathf.DeltaAngle(currAngle, targetAngle);
            float step = Mathf.Sign(delta) * rotateSpeed * Time.deltaTime;

            currAngle += step;
            currAngle = (currAngle + 360f) % 360f;

            guardLOS.IncrAimDirection(step);

            // UpdateSprite(currAngle);

            yield return null;
        }

        currAngle = targetAngle;
    }

    private IEnumerator MoveTo(Vector2 targetPos)
    {
        while (Vector2.Distance(transform.position, targetPos) > 0.05f)
        {
            Vector2 dir = (targetPos - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);


            yield return null;
        }

        transform.position = targetPos;
    }
public static Vector3 DegreeToVector3(float degree)
    {
        float radian = degree * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    private void UpdateSprite(float angle)
    {
        if ((45 >= angle && angle >= 0) || (360 > angle && angle > 315))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (135 >= angle && angle > 45)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (225 >= angle && angle > 135)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (315 > angle && angle > 225)
        {
            transform.eulerAngles = new Vector3(0, 0, 270);
        }
    }
}
