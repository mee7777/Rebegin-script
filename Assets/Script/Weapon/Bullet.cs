using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int Dmg = 10;
    public float Destroytime = 1f;

    // �ʱ�ȭ �޼���

    // �߻�ü �̵� �� �ı� �ڷ�ƾ
    IEnumerator MoveAndDestroy()
    {
        float startTime = Time.time;

        while (Time.time - startTime < Destroytime)
        {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���Ϳ� �浹�ߴ��� Ȯ��
        if (collision.CompareTag("Monster"))
        {
            Monster monster = collision.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(Dmg); // Monster�� TakeDamage �Լ� ȣ��
            }
            Destroy(gameObject); // Bullet �ı�
        }
    }
}

