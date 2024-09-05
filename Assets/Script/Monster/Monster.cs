using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public float patrolSpeed = 2f;              // ������ ���� �ӵ�
    public float chaseSpeed = 4f;               // �÷��̾ �Ѵ� �ӵ�
    public float detectionRange = 5f;           // �÷��̾ ���� ����
    public float attackRange = 1f;              // ���� ����
    public float patrolDistance = 10f;          // ���Ͱ� ������ ������ ����
    public float AttackCoolTime = 0.5f;
    public int AttackDmg = 1;

    private float nextDirectionChangeTime = 0f;
    private float directionChangeInterval = 1f;

    private Vector2 startingPosition;
    private bool movingRight = true;
    private Transform player;
    private Rigidbody2D rb;
    private float curTime;
    private string triggername;

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
        if (player.position.x - transform.position.x < 0) // Ÿ���� ���ʿ� ���� ��
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else // Ÿ���� �����ʿ� ���� ��
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
            ChangeDirection(); // ���� ����
        }
        else if (!movingRight && transform.position.x <= patrolTargetX)
        {
            movingRight = true;
            ChangeDirection(); // ���� ����
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
        // ���� ���� �ۼ�
        rb.velocity = Vector2.zero; // ���� �߿��� �̵� ����
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
                playerHealth.TakeDamage(AttackDmg); // ���ط� ����
                Debug.Log("Player Health After Hit: " + playerHealth.GetCurrentHealth());
                curTime = AttackCoolTime;
            }
        }
    }
}
