using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public float patrolSpeed = 2f;              // 몬스터의 순찰 속도
    public float chaseSpeed = 4f;               // 플레이어를 쫓는 속도
    public float detectionRange = 5f;           // 플레이어를 감지 범위
    public float attackRange = 1f;              // 공격 범위
    public float patrolDistance = 10f;          // 몬스터가 순찰할 구간의 길이
    public float AttackCoolTime = 1f;
    public int AttackDmg = 1;
    public int Health = 1;
    public EnemyManager enemyManager;
    public float size = 1;
    public Animator animator;

    private Vector2 startingPosition;
    private bool movingRight = true;
    private Transform player;
    private Rigidbody2D rb;
    private float curTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        curTime = AttackCoolTime;

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
            FaceTarget();
        }
        else
        {
            Patrol();
        }
        curTime -= Time.deltaTime;
        enemyManager = FindObjectOfType<EnemyManager>();

    }

    void FaceTarget()
    {
        if (player.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(-size, size, 0);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(size, size, 0);
        }
    }

    void ChangeDirection()
    {
        if (movingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.localScale = new Vector3(size, size, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(-size, size, 0);
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
            Player playerHealth = col.GetComponent<Player>();
            if (player != null && curTime <= 0)
            {
                chaseSpeed = 0;
                animator.SetBool("Attack", true);
                playerHealth.TakeDamage(AttackDmg); // 피해량 조정
                Debug.Log("Player Health After Hit: " + playerHealth.GetCurrentHealth());
                curTime = AttackCoolTime;
                Invoke("stopAttack", 1.5f);
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        chaseSpeed = 3f;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;  // 데미지만큼 체력 차감
        Debug.Log("몬스터 체력: " + Health);

        if (Health <= 0)
        {
            stop();
            animator.SetBool("hit", true);
            Invoke("Die", 1f);
        }
    }

    public void Die()
    {
        Destroy(gameObject);  // 적 오브젝트를 파괴
        // 적이 죽을 때 EnemyManager의 deathCount를 증가시킵니다.
        enemyManager.IncreaseDeathCount();
    }

    public void stop()
    {
        chaseSpeed = 0f;
    }

    public void stopAttack()
    {
        animator.SetBool("Attack", false);
    }
}
