using System.Collections;
using UnityEngine;

public class LeftRightPlatform : MonoBehaviour
{
    public float maxSpeed = 5f;  // �ִ� �̵� �ӵ�
    public float moveTime = 2f;  // �¿� �̵� �ð�
    public float smoothTime = 0.5f;  // ���� ��ȯ �� �ε巴�� ��ȯ�ϴ� �ð�
    public float stopDuration = 1f;  // ���ߴ� �ð� (1��)

    private Rigidbody2D rb2d;  // ������ Rigidbody2D
    private bool movingRight = true;  // ���� ���������� �̵� ������ ����
    private float timer = 0f;  // ��� �ð�
    private Vector2 velocity = Vector2.zero;  // �ӵ� �����
    private bool isStopped = false;  // ������ ���߾����� ����
    private float stopTimer = 0f;  // ���� �ð� Ÿ�̸�

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();  // Rigidbody2D ��������
    }

    void FixedUpdate()
    {
        if (isStopped)
        {
            // ������ ���� ���¿��� ���� �ð� ���
            stopTimer += Time.fixedDeltaTime;
            if (stopTimer >= stopDuration)
            {
                // 1�ʰ� ������ �ٽ� �̵� ����
                isStopped = false;
                stopTimer = 0f;
            }
            return;  // ���� ���¿����� �� �̻� �Ʒ� �ڵ� ���� �� ��
        }

        timer += Time.fixedDeltaTime;

        // time �Լ� ����ŭ ����ϸ� ���� ��ȯ
        if (timer >= moveTime)
        {
            movingRight = !movingRight;  // ���� ��ȯ
            timer = 0f;  // Ÿ�̸� ����
            isStopped = true;  // ���� ��ȯ �� ����
            return;  // ���ߴ� ���ȿ��� �̵����� ����
        }

        // �ӵ� ���̵���/�ƿ� ���
        float progress = timer / moveTime;  // 0���� 1 ���� ����
        float fadeSpeed;

        // �߰������� �ִ� �ӵ��� ����, ó���� �������� ������
        if (progress < 0.5f)
        {
            // ���� ���� (ó������ �߰�����)
            fadeSpeed = Mathf.Lerp(0f, maxSpeed * 10, progress * 2);  // ���� ����
        }
        else
        {
            // ���� ���� (�߰����� ������)
            fadeSpeed = Mathf.Lerp(maxSpeed * 10, 0f, (progress - 0.5f) * 2);  // ���� ����
        }

        // ��ǥ ��ġ ��� (������ �Ǵ� �������� �̵�)
        Vector2 targetPosition = rb2d.position + (movingRight ? Vector2.right : Vector2.left) * fadeSpeed * Time.fixedDeltaTime;

        // �ε巴�� ��ǥ ��ġ�� �̵�
        Vector2 newPosition = Vector2.SmoothDamp(rb2d.position, targetPosition, ref velocity, smoothTime);

        // Rigidbody2D �̵�
        rb2d.MovePosition(newPosition);
    }
}

