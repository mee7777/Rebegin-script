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
        // 스틱이 향해있는 방향을 저장해준다.
        Vector3 dir = new Vector3(js.Horizontal, js.Vertical, 0);

        transform.position += dir * movespeed * Time.deltaTime;

        dir.Normalize();
        // 오브젝트의 위치를 dir 방향으로 이동시킨다.
        if (js.Horizontal == 0)
        {
            // 오브젝트의 현재 위치에서 플레이어 위치로 이동
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

        // 몬스터를 찾았을 때
        if (monster != null)
        {
            // 몬스터와의 거리를 계산
            float distanceToMonster = Vector3.Distance(transform.position, monster.transform.position);

            // 일정 거리 이하로 가까워지면 몬스터를 따라감
            if (distanceToMonster <= followDistance)
            {
                // 몬스터 쪽으로 이동
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
        // 카메라 뷰포트 좌표 (0~1 범위)
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        // 카메라 밖으로 나가지 않도록 제한 (0과 1 사이로 위치 제한)
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.05f, 0.95f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.05f, 0.95f);

        // 제한된 뷰포트 좌표를 다시 월드 좌표로 변환
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

