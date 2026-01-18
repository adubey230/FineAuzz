using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class GuardLOS : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private int detectionTimer = 25;
    private Mesh mesh;
    Vector3 origin;
    [SerializeField, Range(0.0f, 360.0f)] public float startingAngle;
    private float angle;
    private bool inVision = false;
    private bool beingDetected = false;
    [SerializeField, Range(0.0f, 10.0f)] public float blinkTimerVal;
    [SerializeField, Range(0.0f, 3.0f)] public float resetTimerVal;
    [SerializeField]private float blinkTimer;
    private float resetTimer;
    private bool runResetTimer = false;
    public bool blinking = false;
    [SerializeField, Range(0.0f, 180.0f)] public float fov = 67.5f;
    private float rotateSpeed = 100.0f;

    private AudioSource audioSource;
    public static event Action<GuardLOS> PlayerDetected;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        //startingAngle += fov / 2;
        blinkTimer = blinkTimerVal;
        resetTimer = resetTimerVal;
        audioSource = GetComponent<AudioSource>();
    }


    void LateUpdate()
    {
        inVision = false;

        if (!blinking){
        int rayCount = 500;
        float angle = startingAngle + fov / 2f;
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
            // RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, GetVectorFromAngle(angle), viewDistance, layerMask);

            Vector3 worldOrigin = transform.TransformPoint(origin);
            Vector3 worldDir = transform.TransformDirection(GetVectorFromAngle(angle));

            RaycastHit2D raycastHit2D = Physics2D.Raycast(worldOrigin, worldDir, viewDistance, layerMask);

            if (raycastHit2D.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            } else {
                vertex = transform.InverseTransformPoint(raycastHit2D.point);
                if (raycastHit2D.collider.CompareTag(playerTag))
                {
                    inVision = true;
                    if (beingDetected == false)
                    {
                        StartCoroutine(DetectPlayer());
                        beingDetected = true;
                        Debug.Log(beingDetected);
                    }
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

        }else{
            mesh.vertices = null;
            mesh.uv = null;
            mesh.triangles = null;
        }

        blinkTimer-=Time.deltaTime;
        if(blinkTimer <= 0.0f && !blinking){

            audioSource.Play();
            blink();
        }
        if(runResetTimer){
            resetTimer -=Time.deltaTime;
            
            if (resetTimer <= 0){
                runResetTimer = false;
                blinking = false;
                resetTimer = resetTimerVal;
                blinkTimer = blinkTimerVal;
            }
        }
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
                Debug.Log(beingDetected);
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

    public Vector3 GetCurrAng(){
        return GetVectorFromAngle(angle);
    }

    private void blink(){
        //Debug.Log("blink");
        runResetTimer = true;
        blinking = true;
    }

    public void RotateToVase(Vector2 targetPos)
    {
        Vector2 direction = (targetPos - (Vector2)transform.position).normalized;

        float targetAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 180f;

        if (targetAngle < 0) targetAngle += 360f;

        while (Mathf.Abs(Mathf.DeltaAngle(angle, targetAngle)) > 0.5f)
        {
            float delta = Mathf.DeltaAngle(angle, targetAngle);
            float step = Mathf.Sign(delta) * rotateSpeed * Time.deltaTime;

            angle += step;
            angle = (angle + 360f) % 360f;

            IncrAimDirection(step);
        }
    }
}

