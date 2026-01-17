using System;
using UnityEngine;
using System.Collections;

public class GuardLOS : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private int detectionTimer = 60;
    private Mesh mesh;
    Vector3 origin;
    [SerializeField, Range(0.0f, 360.0f)] private float startingAngle;
    private float angle;
    private bool inVision = false;
    private bool beingDetected = false;

    [SerializeField, Range(0.0f, 180.0f)] public float fov = 67.5f;

    public static event Action<GuardLOS> PlayerDetected;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        // startingAngle += fov / 2;
    }

    void LateUpdate()
    {
        int rayCount = 100;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        float viewDistance = 50f;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            } else {
                vertex = transform.InverseTransformPoint(raycastHit2D.point);
                if (raycastHit2D.collider.CompareTag(playerTag))
                {
                    inVision = true;
                    if (beingDetected == false) StartCoroutine(DetectPlayer());
                    beingDetected = true;
                }
                else
                {
                    inVision = false;
                }
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) - fov / 2f;
    }
    public void IncrAimDirection(float aimIncr)
    {
        startingAngle += aimIncr;
    }

    private IEnumerator DetectPlayer()
    {
        int timer = detectionTimer;

        while (timer > 0)
        {
            yield return new WaitForSeconds(0.016f);
            timer--;

            // end early if player leaves vision
            if (!inVision)
            {
                beingDetected = false;
                yield break;
            }
        }

        PlayerDetected?.Invoke(this);
        yield break;
    }
    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
