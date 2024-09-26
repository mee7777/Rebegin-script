using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;

    public Transform attackPoint; // ���� ��ġ
    public float attackRange = 0.5f; // ���� ����
    public LayerMask enemyLayers; // �� ���̾�
    public int attackDamage = 10; // ���ݷ�    
    public float jumpForce = 10f;
    public Button jumpButton;
    public Button attackButton;
    public VariableJoystick js;
    public float speed; // ���̽�ƽ�� ���� ������ ������Ʈ�� �ӵ�.
    SpriteRenderer rend;
    public int maxHealth = 5;
    private int currentHealth;

    private Rigidbody2D rb;

    public bool isJump = false;
    public int maxJumpCount = 2;
    public int JumpCount = 0;

    Vector3 moveVec;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();

        attackButton.onClick.AddListener(OnAttackButtonClick);

        currentHealth = maxHealth;
    }

    void Update()
    {

    }

    public void Jump()
    {
        Debug.Log("����");
        if (!isJump)
        {
            if (JumpCount < maxJumpCount)
            {
                JumpCount++;
                rigid.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
            if (JumpCount == maxJumpCount)
            {
                isJump = true;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Floor"))
        {
            JumpCount = 0;
            isJump = false;        
        }
    }

    public void OnJumpButtonClick()
    {
        Jump();
    }

    public void Attack()
    {
        Debug.Log("����");
        // ���� ���� ���� ���� Ž��
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        // ������ ���ظ� ������ ���� (��: ���� Health ������Ʈ�� �����Ͽ� ���ظ� ����)
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("���� ����");
            // ���� Health ��ũ��Ʈ�� �����Ͽ� ���ظ� ������ �ڵ�
            // ��: enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }

    public void OnAttackButtonClick()
    {
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // ��ƽ�� �����ִ� ������ �������ش�.
        moveVec = new Vector3(js.Horizontal, 0, 0).normalized;

        moveVec = moveVec * speed;

        // ������Ʈ�� ��ġ�� dir �������� �̵���Ų��.
        rigid.velocity = new Vector3(moveVec.x, rigid.velocity.y, 0);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        if (currentHealth == 0)
        {
            // �÷��̾ �׾��� ���� ó��
            Debug.Log("Player Died");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

}
