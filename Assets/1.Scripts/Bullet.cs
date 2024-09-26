using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Dmg = 10;
    private Vector2 moveDirection;
    public float bulletSpeed = 10f;
    public float Destroytime = 1f;

    // 초기화 메서드
    public void Initialize(Vector2 direction, float speed)
    {
        // 방향 벡터를 Normalize하여 단위 방향 벡터로 만듭니다.
        moveDirection = direction.normalized;
        bulletSpeed = speed;
    }

    // 발사체 이동 및 파괴 코루틴
    IEnumerator MoveAndDestroy()
    {
        float startTime = Time.time;

        while (Time.time - startTime < Destroytime)
        {
            // 설정된 방향으로 이동
            transform.Translate((Vector3)moveDirection * bulletSpeed * Time.deltaTime);

            // 1 프레임 대기
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Destroy(gameObject);
        }
    }
}