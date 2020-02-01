using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFloatingEffect : MonoBehaviour
{
    [Header("FloatingEffect")]
    private float degreesPerSecond = 15.0f;
    private float amplitude = 0.5f;
    private float frequency = 1f;
    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    public float minRandomDegrees,maxRandomDegrees,minAmplitude,maxAmplitude,minFrequency,maxFrequency;
    private void Start()
    {
        posOffset = transform.position;
        degreesPerSecond = Random.Range(minRandomDegrees, maxRandomDegrees);
        amplitude = Random.Range(minAmplitude, maxAmplitude);
        frequency = Random.Range(minFrequency, maxFrequency);
    }
    private void Update()
    {
        Floating();
    }
    void Floating()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
