using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Dmg = 10;
    private Vector2 moveDirection;
    public float bulletSpeed = 10f;
    public float Destroytime = 1f;

    // �ʱ�ȭ �޼���
    public void Initialize(Vector2 direction, float speed)
    {
        // ���� ���͸� Normalize�Ͽ� ���� ���� ���ͷ� ����ϴ�.
        moveDirection = direction.normalized;
        bulletSpeed = speed;
    }

    // �߻�ü �̵� �� �ı� �ڷ�ƾ
    IEnumerator MoveAndDestroy()
    {
        float startTime = Time.time;

        while (Time.time - startTime < Destroytime)
        {
            // ������ �������� �̵�
            transform.Translate((Vector3)moveDirection * bulletSpeed * Time.deltaTime);

            // 1 ������ ���
            yield return null;
        }

        // �ı�
        Destroy(gameObject);
    }

    private void Start()
    {
        // �̵��� �ı� ����
        StartCoroutine(MoveAndDestroy());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Destroy(gameObject);
        }
    }
}