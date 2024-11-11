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

    private float lastSlashTime = 0f; // ������ Slash ȣ�� �ð��� ����
    public float slashCooldown = 1f; // 1���� ���� ���ð�

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
                // 1�� �ڿ� Stop()�� ����
                StartCoroutine(StopAfterDelay(1f));
            }
        }
        else if (other.CompareTag("Door"))
        {
            Door door = other.GetComponent<Door>();
            if (door != null && Onn == true)
            {
                targetAnimator.SetBool("Glove", true);
                door.TakeDamage(2); // Door���� Damage �޼��带 �߰��ؾ� �մϴ�.
                cameraShake.StartShake();
                Onn = false;
                // 1�� �ڿ� Stop()�� ����
                StartCoroutine(StopAfterDelay(1f));
            }
        }
    }


    public void Punch()
    {
        // ���� �ð��� lastSlashTime + slashCooldown���� ũ�� Slash ����
        if (Time.time >= lastSlashTime + slashCooldown)
        {
            Onn = true;
            lastSlashTime = Time.time; // ������ Slash �ð��� ������Ʈ
        }
    }

    private IEnumerator StopAfterDelay(float delay)
    {
        // ������ �ð� ���� ���
        yield return new WaitForSeconds(delay);

        // ��� �� Stop() ȣ��
        Stop();
    }

    void Stop()
    {
        targetAnimator.SetBool("Glove", false);
    }
}

