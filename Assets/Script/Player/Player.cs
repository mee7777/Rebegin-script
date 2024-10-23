using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    public TextManager textmanager;

    public Animator animator;
    public Transform attackPoint; // 공격 위치
    public float attackRange = 0.5f; // 공격 범위
    public LayerMask enemyLayers; // 적 레이어
    public int attackDamage = 10; // 공격력    
    public float jumpForce = 10f;
    public Button jumpButton;
    public Button attackButton;
    public VariableJoystick js;
    public float speed; // 조이스틱에 의해 움직일 오브젝트의 속도.
    SpriteRenderer rend;
    public int maxHealth = 5;
    private int currentHealth;
    // 여기 추가했습니다
    int direction;
    float detect_range = 2f;
    public LayerMask objectLayer;

    private Rigidbody2D rb;
    GameObject scanObject;
    GameObject detectedObject;
    public GameObject textObject;

    public bool isJump = false;
    public int maxJumpCount = 2;
    public int JumpCount = 0;

    Vector3 moveVec;
    public float size = 0.9f;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();

        attackButton.onClick.AddListener(OnAttackButtonClick);

        currentHealth = maxHealth;
    }

    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detect_range, objectLayer);

        if (textmanager.isAction == false)
        {
            if (hits.Length <= 0)
            {
                Debug.Log("텍스트출력취소");
                textObject.SetActive(false);
                textmanager.isAction = true;
            }
        }            
    }

    public void Jump()
    {
        if (scanObject != null)
        {
            textmanager.Action(scanObject);
        }
        else
        {
            Debug.Log("점프");
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

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // 스틱이 향해있는 방향을 저장해준다.
        moveVec = new Vector3(js.Horizontal, 0, 0).normalized;

        moveVec = moveVec * speed;

        // 오브젝트의 위치를 dir 방향으로 이동시킨다.
        rigid.velocity = new Vector3(moveVec.x, rigid.velocity.y, 0);

        if(js.Horizontal < 0)
        {
            direction = -2;
            animator.SetBool("isWalk", true);
            transform.localScale = new Vector3(-size, size, 0);
        }
        if(js.Horizontal > 0)
        {
            direction = 2;
            animator.SetBool("isWalk", true);
            transform.localScale = new Vector3(size, size, 0);
        }
        if(js.Horizontal == 0)
        {
            animator.SetBool("isWalk", false);
        }
        Debug.DrawRay(rigid.position, new Vector3(direction * detect_range, 0, 0), new Color(0, 0, 1));

        RaycastHit2D rayHit_detect = Physics2D.Raycast(rigid.position, new Vector3(direction, 0, 0), detect_range, LayerMask.GetMask("Object"));
        if (rayHit_detect.collider != null)
        {
            scanObject = rayHit_detect.collider.gameObject;

        }
        else
        {
            scanObject = null;
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        if (currentHealth == 0)
        {
            // 플레이어가 죽었을 때의 처리
            Debug.Log("Player Died");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

}
