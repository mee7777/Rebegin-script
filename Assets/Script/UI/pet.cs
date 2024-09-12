using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pet : MonoBehaviour
{
    public Transform player;      // �÷��̾��� Transform
    public float followDistance = 3f;  // ���� �÷��̾ ���󰡱� �����ϴ� �Ÿ�
    public float moveSpeed = 2f;       // ���� �̵� �ӵ�
    public float stopDistance = 1f;    // ������ ������ �Ÿ�
    public float smoothTime = 0.3f;    // ���� �ð� (�ε巴�� ���ߴ� �ð�)

    private Vector3 currentVelocity = Vector3.zero;  // ������ ���� ���� �ӵ�

    void Update()
    {
        // �÷��̾�� �� ������ �Ÿ� ���
        float distance = Vector3.Distance(transform.position, player.position);

        // �÷��̾���� �Ÿ��� followDistance���� ũ�� �̵�
        if (distance > stopDistance)
        {
            FollowPlayer();
        }
        else
        {
            // ���� �� �ε巴�� ���ߴ� ó��
            SmoothStop();
        }
    }

    void FollowPlayer()
    {
        // �÷��̾ ���� ���� �̵��ϵ��� ��
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 targetPosition = player.position - direction * stopDistance;

        // ���� �ε巴�� ��ǥ �������� �̵�
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }

    void SmoothStop()
    {
        // ���� ���� ��ġ�� �ӹ��� �ε巴�� ���ߴ� ó��
        transform.position = Vector3.SmoothDamp(transform.position, transform.position, ref currentVelocity, smoothTime);
    }
}

