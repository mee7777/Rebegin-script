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
    public Text buttonText;
   
    private Rigidbody2D rb;

    public bool isJump = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();

        attackButton.onClick.AddListener(OnAttackButtonClick);
    }

    void Update()
    {

    }

    public void Jump()
    {
        Debug.Log("����");
        if (!isJump)
        {
            isJump = true;
            rigid.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Equals("Floor"))
        {
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


}
