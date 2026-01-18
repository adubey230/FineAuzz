using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;

public class GuardMove : MonoBehaviour
{
    [SerializeField] private GuardLOS guardLOS;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public Animator animator;

    [SerializeField] private List<Vector2> patrolPoints;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotateSpeed = 60f;
    [SerializeField] private float waitTime = 0.5f;

    private float nextAngle;
    private float currAngle;
    private int currIndex;

    private float startAngle;

    Vector2 currentPos;
    Vector2 nextPos;
    void Start()
    {
        transform.position = patrolPoints[0];
        guardLOS = GetComponentInChildren<GuardLOS>();

        startAngle = guardLOS.startingAngle;

        if (startAngle < 0) startAngle += 360;

        // initialize to face starting angle
        if (patrolPoints.Count > 1)
        {
            Vector2 direction = (patrolPoints[1] - (Vector2)transform.position).normalized;

            float targetAngle =
                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (targetAngle < 0) targetAngle += 360f;
            if (targetAngle > 360) targetAngle -= 360f;

            float delta = Mathf.DeltaAngle(startAngle, nextAngle);
            guardLOS.IncrAimDirection(delta);
            StartCoroutine(PatrolLoop());
        }
    }

    private void Update()
    {
        animator.SetLayerWeight(1, animator.GetBool("shocked") ? 1f : 0f);
        animator.SetLayerWeight(2, animator.GetBool("closed") ? 1f : 0f);
    }
    private IEnumerator PatrolLoop()
    {
        int nextIndex = 1;
        while (true)
        {
            while (guardLOS.blinking)
            {
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);
            nextIndex = (currIndex + 1) % patrolPoints.Count;
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
            while (guardLOS.blinking)
            {
                yield return null;
            }

            float delta = Mathf.DeltaAngle(currAngle, targetAngle);
            float step = Mathf.Sign(delta) * rotateSpeed * Time.deltaTime;

            currAngle += step;
            currAngle = (currAngle + 360f) % 360f;

            guardLOS.IncrAimDirection(step);

            UpdateSprite(currAngle);

            yield return null;
        }

        currAngle = targetAngle;
        UpdateSprite(currAngle);
    }

    private IEnumerator MoveTo(Vector2 targetPos)
    {
        animator.SetBool("moving", true);

        while (Vector2.Distance(transform.position, targetPos) > 0.05f)
        {
            Vector2 dir = (targetPos - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);


            yield return null;
        }

        transform.position = targetPos;
        animator.SetBool("moving", false);
    }
public static Vector3 DegreeToVector3(float degree)
    {
        float radian = degree * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    private void UpdateSprite(float angle)
    {
        // up
        if ((45 >= angle && angle >= 0) || (360 > angle && angle > 315))
        {
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("Horizontal", 1f);
            spriteRenderer.flipX = false;
        }
        // left?
        else if (135 >= angle && angle > 45)
        {
            animator.SetFloat("Vertical", 1f);
            animator.SetFloat("Horizontal", 0f);
        }
        // down
        else if (225 >= angle && angle > 135)
        {
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("Horizontal", -1f);
            spriteRenderer.flipX = true;
        }
        // right?
        else if (315 > angle && angle > 225)
        {
            animator.SetFloat("Vertical", -1f);
            animator.SetFloat("Horizontal", 0f);
        }
    }
}
