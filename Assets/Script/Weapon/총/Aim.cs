using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    public Animator targetAnimator;
    public SpriteRenderer Player;

    public Transform playerTransform;
    public VariableJoystick js;
    public float movespeed = 5f;
    public float hitspeed = 5f;
    public float followDistance = 5f;
    public CamShake cameraShake;
    public SpriteRenderer rendere;

    public string monsterTag = "Monster";
    private GameObject monster;

    private void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        Player = playerObject.GetComponent<SpriteRenderer>();
        rendere = GetComponent<SpriteRenderer>();
        rendere.color = new Color(1, 1, 1, 0);
    }

    public void OnAim()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        rendere = GetComponent<SpriteRenderer>();
        rendere.color = new Color(1, 1, 1, 1);
    }

    void FixedUpdate()
    {
        // ��ƽ�� �����ִ� ������ �������ش�.
        Vector3 dir = new Vector3(js.Horizontal, js.Vertical, 0);

        transform.position += dir * movespeed * Time.deltaTime;

        dir.Normalize();
        // ������Ʈ�� ��ġ�� dir �������� �̵���Ų��.
        if (js.Horizontal == 0)
        {
            // ������Ʈ�� ���� ��ġ���� �÷��̾� ��ġ�� �̵�
            transform.position = playerTransform.position;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            Boo();
        }
        else
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = true;

        }

        if (monster == null)
        {
            monster = GameObject.FindGameObjectWithTag(monsterTag);
        }

        // ���͸� ã���� ��
        if (monster != null)
        {
            // ���Ϳ��� �Ÿ��� ���
            float distanceToMonster = Vector3.Distance(transform.position, monster.transform.position);

            // ���� �Ÿ� ���Ϸ� ��������� ���͸� ����
            if (distanceToMonster <= followDistance)
            {
                // ���� ������ �̵�
                transform.position = Vector3.MoveTowards(transform.position, monster.transform.position, hitspeed * Time.deltaTime);
            }
        }

        RestrictPositionToCamera();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster != null)
            {
                targetAnimator.SetBool("Gun", true);
                monster.TakeDamage(1);
                cameraShake.StartShake();
            }
        }
    }

    void RestrictPositionToCamera()
    {
        // ī�޶� ����Ʈ ��ǥ (0~1 ����)
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        // ī�޶� ������ ������ �ʵ��� ���� (0�� 1 ���̷� ��ġ ����)
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.05f, 0.95f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.05f, 0.95f);

        // ���ѵ� ����Ʈ ��ǥ�� �ٽ� ���� ��ǥ�� ��ȯ
        transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
    }


    void Boo()
    {
        targetAnimator.SetBool("Gun", false);
        
    }

    public void Button()
    {
        js.gameObject.SetActive(true);
    }

    public void ButtonDown()
    {
        js.gameObject.SetActive(false);
    }
}

