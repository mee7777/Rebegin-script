using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Glove : MonoBehaviour
{
    public Animator targetAnimator;
    public SpriteRenderer Player;

    public Transform playerTransform;
    public CamShake cameraShake;

    public string doorTag = "Door";
    public string monsterTag = "Monster";
    private GameObject monster;
    public bool Onn = false;

    private float lastSlashTime = 0f; // 마지막 Slash 호출 시간을 저장
    public float slashCooldown = 1f; // 1초의 재사용 대기시간

    private void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        Player = playerObject.GetComponent<SpriteRenderer>();
    }

    public void OnAim()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster != null && Onn == true)
            {
                targetAnimator.SetBool("Glove", true);
                monster.TakeDamage(2);
                cameraShake.StartShake();
                Onn = false;
                // 1초 뒤에 Stop()을 실행
                StartCoroutine(StopAfterDelay(1f));
            }
        }
        else if (other.CompareTag("Door"))
        {
            Door door = other.GetComponent<Door>();
            if (door != null && Onn == true)
            {
                targetAnimator.SetBool("Glove", true);
                door.TakeDamage(2); // Door에도 Damage 메서드를 추가해야 합니다.
                cameraShake.StartShake();
                Onn = false;
                // 1초 뒤에 Stop()을 실행
                StartCoroutine(StopAfterDelay(1f));
            }
        }
    }


    public void Punch()
    {
        // 현재 시간이 lastSlashTime + slashCooldown보다 크면 Slash 실행
        if (Time.time >= lastSlashTime + slashCooldown)
        {
            Onn = true;
            lastSlashTime = Time.time; // 마지막 Slash 시간을 업데이트
        }
    }

    private IEnumerator StopAfterDelay(float delay)
    {
        // 지정된 시간 동안 대기
        yield return new WaitForSeconds(delay);

        // 대기 후 Stop() 호출
        Stop();
    }

    void Stop()
    {
        targetAnimator.SetBool("Glove", false);
    }
}

