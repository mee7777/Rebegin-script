using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pet : MonoBehaviour
{
    public Transform player;      // 플레이어의 Transform
    public float followDistance = 3f;  // 펫이 플레이어를 따라가기 시작하는 거리
    public float moveSpeed = 2f;       // 펫의 이동 속도
    public float stopDistance = 1f;    // 감속을 시작할 거리
    public float smoothTime = 0.3f;    // 감속 시간 (부드럽게 멈추는 시간)

    private Vector3 currentVelocity = Vector3.zero;  // 감속을 위한 현재 속도

    void Update()
    {
        // 플레이어와 펫 사이의 거리 계산
        float distance = Vector3.Distance(transform.position, player.position);

        // 플레이어와의 거리가 followDistance보다 크면 이동
        if (distance > stopDistance)
        {
            FollowPlayer();
        }
        else
        {
            // 감속 후 부드럽게 멈추는 처리
            SmoothStop();
        }

        if (player.position.x < transform.position.x)
        {
            // 플레이어가 오브젝트의 왼쪽에 있을 때
            transform.localScale = new Vector3(1f, 1f, 1f); // 오브젝트를 왼쪽으로 반전
        }
        else
        {
            // 플레이어가 오브젝트의 오른쪽에 있을 때
            transform.localScale = new Vector3(-1f, 1f, 1f); // 오브젝트를 오른쪽으로 설정
        }
    }

    void FollowPlayer()
    {
        // 플레이어를 향해 펫이 이동하도록 함
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 targetPosition = player.position - direction * stopDistance;

        // 펫을 부드럽게 목표 지점으로 이동
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }

    void SmoothStop()
    {
        // 펫이 현재 위치에 머물며 부드럽게 멈추는 처리
        transform.position = Vector3.SmoothDamp(transform.position, transform.position, ref currentVelocity, smoothTime);
    }
}

