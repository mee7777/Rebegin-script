using System.Collections;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    private Vector3 originalPosition;
    public float shakeAmount = 0.1f;  // ������ ����
    public float shakeDuration = 0.5f; // ���� ���� �ð�
    public float shakeTimeRemaining = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        // ���� ���� �ð� ���� ī�޶� ����
        if (shakeTimeRemaining > 0)
        {
            // Z���� ������ �ʵ��� �ϰ� X, Y�ุ ��鸮�� ��
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

