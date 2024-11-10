using System.Collections;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    private Vector3 originalPosition;
    public float shakeAmount = 0.1f;  // 흔들기의 강도
    public float shakeDuration = 0.5f; // 흔들기 지속 시간
    public float shakeTimeRemaining = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        // 흔들기 지속 시간 동안 카메라 흔들기
        if (shakeTimeRemaining > 0)
        {
            // Z축은 변하지 않도록 하고 X, Y축만 흔들리게 함
            transform.position = new Vector3(
                originalPosition.x + Random.Range(-shakeAmount, shakeAmount),
                originalPosition.y + Random.Range(-shakeAmount, shakeAmount),
                originalPosition.z
            );
            shakeTimeRemaining -= Time.deltaTime;
        }
    }

    public void StartShake()
    {
        originalPosition = transform.position;
        shakeTimeRemaining = shakeDuration;
    }
}

