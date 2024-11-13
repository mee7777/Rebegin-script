using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSword : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip fireSound;

    public Animator targetAnimator;
    public SpriteRenderer Player;

    public Transform playerTransform;
    public CamShake cameraShake;

    public string monsterTag = "Monster";
    private GameObject monster;
    public bool On = false;

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
            if (monster != null && On == true)
            {
                audioSource.PlayOneShot(fireSound);
                targetAnimator.SetBool("Sword", true);
                monster.TakeDamage(2);
                cameraShake.StartShake();
                On = false;
                // 1초 뒤에 Stop()을 실행
                StartCoroutine(StopAfterDelay(1f));
            }
        }
    }

    public void Slash()
    {
        // 현재 시간이 lastSlashTime + slashCooldown보다 크면 Slash 실행
        if (Time.time >= lastSlashTime + slashCooldown)
        {
            On = true;
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
        targetAnimator.SetBool("Sword", false);
    }
}


