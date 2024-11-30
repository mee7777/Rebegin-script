using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxHealth = 200; // 보스 체력
    public int currentHealth;

    public GameObject leftHand; // 보스의 왼손
    public GameObject rightHand;// 보스의 오른손
    public GameObject Hand;     // 보스의 양손
    public GameObject body;     // 보스의 몸체
    public GameObject player;   // 플레이어 참조
    public GameObject warningEffect; // 전조 효과
    public GameObject laserBeamPrefab; // 레이저 빔 프리팹

    public laserBeam asdf;
    public enum BossPhase { Phase1, Phase2 }
    private BossPhase currentPhase = BossPhase.Phase1;

    private int i = 1;
    private bool isAttacking = false;

    void Start()
    {
        currentHealth = maxHealth;
        StartPattern();
    }

    void Update()
    {
        if (currentHealth <= maxHealth / 2 && currentPhase == BossPhase.Phase1)
        {
            EnterPhase2();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void EnterPhase2()
    {
        currentPhase = BossPhase.Phase2;
        // 페이즈 전환 시 비주얼 변경, 팔 제거 등
        Destroy(leftHand);
        Debug.Log("Phase 2 시작!");
    }

    void Die()
    {
        Debug.Log("보스가 처치되었습니다!");
        Destroy(gameObject); // 보스 삭제
    }

    public void StartPattern()
    {
        if (currentHealth <= maxHealth / 2 && currentPhase == BossPhase.Phase1)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                switch (i)
                {
                    case 1:
                        StartCoroutine(Phase2Pattern1());
                        i++;
                        break;
                    case 2:
                        StartCoroutine(Phase2Pattern2());
                        i++;
                        break;
                    case 3:
                        StartCoroutine(Phase2Pattern3());
                        i = 1;
                        break;
                }
            }
        }
        else
        {
            if (!isAttacking)
            {
                isAttacking = true;
                switch (i)
                {
                    case 1:
                        StartCoroutine(Phase1Pattern1());
                        i++;
                        break;
                    case 2:
                        StartCoroutine(Phase1Pattern2());
                        i++;
                        break;
                    case 3:
                        StartCoroutine(Phase1Pattern3());
                        i = 1;
                        break;
                }
            }
        }
    }

    // Phase 1 Pattern 1: 왼손 추적 및 레이저 빔
    private IEnumerator Phase1Pattern1()
    {
        Debug.Log("Phase 1 Pattern 1 시작!");
        float followDuration = 3f;
        Vector3 targetPosition = player.transform.position;

        // 손이 플레이어를 따라감
        float elapsed = 0;
        while (elapsed < followDuration)
        {
            leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, targetPosition, Time.deltaTime * 2);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Debug.Log("레이저 발사 준비...");
        yield return new WaitForSeconds(3f);

        // 레이저 발사
        Instantiate(laserBeamPrefab, leftHand.transform.position, Quaternion.identity);
        

        Debug.Log("레이저 발사!");
        isAttacking = false;
        
        yield return new WaitForSeconds(3f);
        leftHand.transform.position = body.transform.position;
        StartPattern();
    }

    // Phase 1 Pattern 2: 양손 내려찍기
    private IEnumerator Phase1Pattern2()
    {
        Debug.Log("Phase 1 Pattern 2 시작!");
        for (int i = 0; i < 3; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), player.transform.position.y, 0);
            Hand.transform.position = randomPosition;
            yield return new WaitForSeconds(5f); // 들어올리기
            Debug.Log("손 내려찍음!");
            yield return new WaitForSeconds(1f);
        }
        isAttacking = false;
        StartPattern();
    }

    // Phase 1 Pattern 3: 바닥 쓸기
    private IEnumerator Phase1Pattern3()
    {
        Debug.Log("Phase 1 Pattern 3 시작!");
        // 전조 표시
        Instantiate(warningEffect, new Vector3(-5f, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(3f);

        // 쓸기
        Debug.Log("바닥 쓸기 시작!");
        float sweepDuration = 3f;
        Vector3 start = new Vector3(-5f, 0, 0);
        Vector3 end = new Vector3(5f, 0, 0);
        float elapsed = 0;

        while (elapsed < sweepDuration)
        {
            leftHand.transform.position = Vector3.Lerp(start, end, elapsed / sweepDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        isAttacking = false;
        StartPattern();
    }

    // Phase 2 Pattern 1: 눈 레이저
    private IEnumerator Phase2Pattern1()
    {
        Debug.Log("Phase 2 Pattern 1 시작!");
        // 전조
        Debug.Log("전조 표시 중...");
        yield return new WaitForSeconds(5f);

        // 레이저 발사
        Instantiate(laserBeamPrefab, body.transform.position, Quaternion.identity);
        Debug.Log("레이저 발사!");
        isAttacking = false;
        StartPattern();
    }

    // Phase 2 Pattern 2: 느린 총알
    private IEnumerator Phase2Pattern2()
    {
        Debug.Log("Phase 2 Pattern 2 시작!");
        for (int i = 0; i < 3; i++)
        {
            Vector3 bulletPosition = player.transform.position;
            Debug.Log("총알 발사 준비...");
            yield return new WaitForSeconds(0.5f);
            // 총알 생성
            Instantiate(warningEffect, bulletPosition, Quaternion.identity);
        }
        isAttacking = false;
        StartPattern();
    }

    // Phase 2 Pattern 3: 레이저 기둥
    private IEnumerator Phase2Pattern3()
    {
        Debug.Log("Phase 2 Pattern 3 시작!");
        yield return new WaitForSeconds(3f); // 전조 표시

        Debug.Log("레이저 기둥 생성!");
        for (int i = 0; i < 8; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 0);
            Instantiate(laserBeamPrefab, randomPosition, Quaternion.identity);
        }
        isAttacking = false;
        StartPattern();
    }
}