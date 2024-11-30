using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxHealth = 200; // ���� ü��
    public int currentHealth;

    public GameObject leftHand; // ������ �޼�
    public GameObject rightHand;// ������ ������
    public GameObject Hand;     // ������ ���
    public GameObject body;     // ������ ��ü
    public GameObject player;   // �÷��̾� ����
    public GameObject warningEffect; // ���� ȿ��
    public GameObject laserBeamPrefab; // ������ �� ������

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
        // ������ ��ȯ �� ���־� ����, �� ���� ��
        Destroy(leftHand);
        Debug.Log("Phase 2 ����!");
    }

    void Die()
    {
        Debug.Log("������ óġ�Ǿ����ϴ�!");
        Destroy(gameObject); // ���� ����
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

    // Phase 1 Pattern 1: �޼� ���� �� ������ ��
    private IEnumerator Phase1Pattern1()
    {
        Debug.Log("Phase 1 Pattern 1 ����!");
        float followDuration = 3f;
        Vector3 targetPosition = player.transform.position;

        // ���� �÷��̾ ����
        float elapsed = 0;
        while (elapsed < followDuration)
        {
            leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, targetPosition, Time.deltaTime * 2);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Debug.Log("������ �߻� �غ�...");
        yield return new WaitForSeconds(3f);

        // ������ �߻�
        Instantiate(laserBeamPrefab, leftHand.transform.position, Quaternion.identity);
        

        Debug.Log("������ �߻�!");
        isAttacking = false;
        
        yield return new WaitForSeconds(3f);
        leftHand.transform.position = body.transform.position;
        StartPattern();
    }

    // Phase 1 Pattern 2: ��� �������
    private IEnumerator Phase1Pattern2()
    {
        Debug.Log("Phase 1 Pattern 2 ����!");
        for (int i = 0; i < 3; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), player.transform.position.y, 0);
            Hand.transform.position = randomPosition;
            yield return new WaitForSeconds(5f); // ���ø���
            Debug.Log("�� ��������!");
            yield return new WaitForSeconds(1f);
        }
        isAttacking = false;
        StartPattern();
    }

    // Phase 1 Pattern 3: �ٴ� ����
    private IEnumerator Phase1Pattern3()
    {
        Debug.Log("Phase 1 Pattern 3 ����!");
        // ���� ǥ��
        Instantiate(warningEffect, new Vector3(-5f, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(3f);

        // ����
        Debug.Log("�ٴ� ���� ����!");
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

    // Phase 2 Pattern 1: �� ������
    private IEnumerator Phase2Pattern1()
    {
        Debug.Log("Phase 2 Pattern 1 ����!");
        // ����
        Debug.Log("���� ǥ�� ��...");
        yield return new WaitForSeconds(5f);

        // ������ �߻�
        Instantiate(laserBeamPrefab, body.transform.position, Quaternion.identity);
        Debug.Log("������ �߻�!");
        isAttacking = false;
        StartPattern();
    }

    // Phase 2 Pattern 2: ���� �Ѿ�
    private IEnumerator Phase2Pattern2()
    {
        Debug.Log("Phase 2 Pattern 2 ����!");
        for (int i = 0; i < 3; i++)
        {
            Vector3 bulletPosition = player.transform.position;
            Debug.Log("�Ѿ� �߻� �غ�...");
            yield return new WaitForSeconds(0.5f);
            // �Ѿ� ����
            Instantiate(warningEffect, bulletPosition, Quaternion.identity);
        }
        isAttacking = false;
        StartPattern();
    }

    // Phase 2 Pattern 3: ������ ���
    private IEnumerator Phase2Pattern3()
    {
        Debug.Log("Phase 2 Pattern 3 ����!");
        yield return new WaitForSeconds(3f); // ���� ǥ��

        Debug.Log("������ ��� ����!");
        for (int i = 0; i < 8; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 0);
            Instantiate(laserBeamPrefab, randomPosition, Quaternion.identity);
        }
        isAttacking = false;
        StartPattern();
    }
}