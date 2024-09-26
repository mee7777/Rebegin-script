using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int Dmg = 10;
    public float Destroytime = 1f;

    // 초기화 메서드

    // 발사체 이동 및 파괴 코루틴
    IEnumerator MoveAndDestroy()
    {
        float startTime = Time.time;

        while (Time.time - startTime < Destroytime)
        {
            yield return null;
        }

        // 파괴
        Destroy(gameObject);
    }

    private void Start()
    {
        // 이동과 파괴 시작
        StartCoroutine(MoveAndDestroy());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 몬스터와 충돌했는지 확인
        if (collision.CompareTag("Monster"))
        {
            Monster monster = collision.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(Dmg); // Monster의 TakeDamage 함수 호출
            }
            Destroy(gameObject); // Bullet 파괴
        }
    }
}

