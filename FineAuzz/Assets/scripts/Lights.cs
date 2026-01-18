using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class Lights : MonoBehaviour
{
    [SerializeField] List<Light2D> alarms;
    [SerializeField] private float minIntensity = 10f;
    [SerializeField] private float maxIntensity = 10f;
    [SerializeField] private float flickerSpeed = 2f;
    private bool isBright = false;

    void Start()
    {
        InvokeRepeating(nameof(Flicker), 0f, flickerSpeed);
    }

    void Flicker()
    {
        isBright = !isBright;

        float target = isBright ? maxIntensity : minIntensity;

        foreach (Light2D light in alarms)
        {
            light.intensity = target;
        }
    }

}
