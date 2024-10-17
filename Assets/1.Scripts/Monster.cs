using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public float patrolSpeed = 2f;              // 몬스터의 순찰 속도
    public float chaseSpeed = 4f;               // 플레이어를 쫓는 속도
    public float detectionRange = 5f;           // 플레이어를 감지 범위
    public float attackRange = 1f;              // 공격 범위
    public float patrolDistance = 10f;          // 몬스터가 순찰할 구간의 길이
    public float AttackCoolTime = 0.5f;
    public int AttackDmg = 1;
    public GameObject childObject;

    private float nextDirectionChangeTime = 0f;
    private float directionChangeInterval = 1f;

    private Vector2 startingPosition;
    private bool movingRight = true;
    private Transform player;
    private Rigidbody2D rb;
    private float curTime;
    private string triggername;

    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        triggername = "atk";
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
        curTime -= Time.deltaTime;

    }

    void FaceTarget()
    {
        if (player.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void ChangeDirection()
    {
        if (movingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Patrol()
    {
        float patrolTargetX = movingRight ? startingPosition.x + patrolDistance : startingPosition.x - patrolDistance;

        if (movingRight && transform.position.x >= patrolTargetX)
        {
            movingRight = false;
            ChangeDirection(); // 방향 변경
        }
        else if (!movingRight && transform.position.x <= patrolTargetX)
        {
            movingRight = true;
            ChangeDirection(); // 방향 변경
        }

        float moveDirection = movingRight ? 1 : -1;
        rb.velocity = new Vector2(moveDirection * patrolSpeed, rb.velocity.y);
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
    }

    void AttackPlayer()
    {
        // 공격 로직 작성
        rb.velocity = Vector2.zero; // 공격 중에는 이동 멈춤
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Animator childAnimator = childObject.GetComponent<Animator>();
            PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
            if (playerHealth != null && curTime <= 0)
            {
                playerHealth.TakeDamage(AttackDmg); // 피해량 조정
                Debug.Log("Player Health After Hit: " + playerHealth.GetCurrentHealth());
                curTime = AttackCoolTime;
                childAnimator.SetTrigger(triggername);
            }
        }
    }
}
