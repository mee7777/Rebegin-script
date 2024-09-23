using System.Collections;
using UnityEngine;

public class UpDownPlatform : MonoBehaviour
{
    public float maxSpeed = 5f;  // �ִ� �̵� �ӵ�
    public float moveTime = 2f;  // ���Ʒ� �̵� �ð�
    public float smoothTime = 0.5f;  // ���� ��ȯ �� �ε巴�� ��ȯ�ϴ� �ð�
    public float stopDuration = 1f;  // ���ߴ� �ð� (1��)
    public bool direction = true;  // �̵� ���� (true = ����, false = �Ʒ���)

    private Rigidbody2D rb2d;  // ������ Rigidbody2D
    private bool movingUp;  // ���� ���� �̵� ������ ����
    private float timer = 0f;  // ��� �ð�
    private Vector2 velocity = Vector2.zero;  // �ӵ� �����
    private bool isStopped = false;  // ������ ���߾����� ����
    private float stopTimer = 0f;  // ���� �ð� Ÿ�̸�

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();  // Rigidbody2D ��������
        SetInitialDirection();  // ó�� ���� ����
    }

    void SetInitialDirection()
    {
        // direction ���� �������� �ʱ� ���� ����
        movingUp = direction;
    }

    void FixedUpdate()
    {
        if (isStopped)
        {
            // ������ ���� ���¿��� ���� �ð� ���
            stopTimer += Time.fixedDeltaTime;
            if (stopTimer > stopDuration)
            {
                // 1�ʰ� ������ �ٽ� �̵� ����
                isStopped = false;
                stopTimer = 0f;
            }
            return;  // ���� ���¿����� �� �̻� �Ʒ� �ڵ� ���� �� ��
        }

        timer += Time.fixedDeltaTime;

        // moveTime ��� �� ���� ��ȯ
        if (timer > moveTime)
        {
            movingUp = !movingUp;  // ���� ��ȯ
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

        // ��ǥ ��ġ ��� (�� �Ǵ� �Ʒ��� �̵�)
        Vector2 targetPosition = rb2d.position + (movingUp ? Vector2.up : Vector2.down) * fadeSpeed * Time.fixedDeltaTime;

        // �ε巴�� ��ǥ ��ġ�� �̵�
        Vector2 newPosition = Vector2.SmoothDamp(rb2d.position, targetPosition, ref velocity, smoothTime);

        // Rigidbody2D �̵�
        rb2d.MovePosition(newPosition);
    }
}






