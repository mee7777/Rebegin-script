using System.Collections;
using UnityEngine;

public class UpDownPlatform : MonoBehaviour
{
    public float maxSpeed = 5f;  // 최대 이동 속도
    public float moveTime = 2f;  // 위아래 이동 시간
    public float smoothTime = 0.5f;  // 방향 전환 시 부드럽게 전환하는 시간
    public float stopDuration = 1f;  // 멈추는 시간 (1초)
    public bool direction = true;  // 이동 방향 (true = 위로, false = 아래로)

    private Rigidbody2D rb2d;  // 발판의 Rigidbody2D
    private bool movingUp;  // 현재 위로 이동 중인지 여부
    private float timer = 0f;  // 경과 시간
    private Vector2 velocity = Vector2.zero;  // 속도 저장용
    private bool isStopped = false;  // 발판이 멈추었는지 여부
    private float stopTimer = 0f;  // 멈춘 시간 타이머

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();  // Rigidbody2D 가져오기
        SetInitialDirection();  // 처음 방향 설정
    }

    void SetInitialDirection()
    {
        // direction 값을 바탕으로 초기 방향 설정
        movingUp = direction;
    }

    void FixedUpdate()
    {
        if (isStopped)
        {
            // 발판이 멈춘 상태에서 멈춘 시간 계산
            stopTimer += Time.fixedDeltaTime;
            if (stopTimer > stopDuration)
            {
                // 1초가 지나면 다시 이동 시작
                isStopped = false;
                stopTimer = 0f;
            }
            return;  // 멈춘 상태에서는 더 이상 아래 코드 실행 안 함
        }

        timer += Time.fixedDeltaTime;

        // moveTime 경과 시 방향 전환
        if (timer > moveTime)
        {
            movingUp = !movingUp;  // 방향 전환
            timer = 0f;  // 타이머 리셋
            isStopped = true;  // 방향 전환 시 멈춤
            return;  // 멈추는 동안에는 이동하지 않음
        }

        // 속도 페이드인/아웃 계산
        float progress = timer / moveTime;  // 0에서 1 사이 비율
        float fadeSpeed;

        // 중간점에서 최대 속도를 내고, 처음과 끝에서는 느리게
        if (progress < 0.5f)
        {
            // 가속 구간 (처음에서 중간까지)
            fadeSpeed = Mathf.Lerp(0f, maxSpeed * 10, progress * 2);  // 선형 가속
        }
        else
        {
            // 감속 구간 (중간에서 끝까지)
            fadeSpeed = Mathf.Lerp(maxSpeed * 10, 0f, (progress - 0.5f) * 2);  // 선형 감속
        }

        // 목표 위치 계산 (위 또는 아래로 이동)
        Vector2 targetPosition = rb2d.position + (movingUp ? Vector2.up : Vector2.down) * fadeSpeed * Time.fixedDeltaTime;

        // 부드럽게 목표 위치로 이동
        Vector2 newPosition = Vector2.SmoothDamp(rb2d.position, targetPosition, ref velocity, smoothTime);

        // Rigidbody2D 이동
        rb2d.MovePosition(newPosition);
    }
}






