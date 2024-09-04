using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;

    public Transform attackPoint; // 공격 위치
    public float attackRange = 0.5f; // 공격 범위
    public LayerMask enemyLayers; // 적 레이어
    public int attackDamage = 10; // 공격력    
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
        Debug.Log("점프");
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
        Debug.Log("공격");
        // 공격 범위 내의 적을 탐지
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        // 적에게 피해를 입히는 로직 (예: 적의 Health 컴포넌트에 접근하여 피해를 입힘)
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("공격 범위");
            // 적의 Health 스크립트에 접근하여 피해를 입히는 코드
            // 예: enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
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
