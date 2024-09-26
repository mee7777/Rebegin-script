using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    GameObject PlayerObject;

    public GameObject Player;
    public GameObject WeaponPoint;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed;
    public Transform groundcheck;

    private GameObject PlayerController;
    public bool CanAttack;

    Vector3 forceDirection;

    public float cooldownTime = 1f; // ��Ÿ�� �ð� (��)
    private float nextShootTime = 0f; // ���� �߻� ���� �ð�

    public GameObject PlayerManager; // PlayerManager ������Ʈ ����
    private ChangeWeapon changeWeapon; // ChangeWeapon ��ũ��Ʈ ����

    // Start is called before the first frame update
    void Start()
    {
        CanAttack = false;
        // PlayerManager���� ChangeWeapon ��ũ��Ʈ ã��
        if (PlayerManager != null)
        {
            changeWeapon = PlayerManager.GetComponent<ChangeWeapon>();
        }

        if (changeWeapon == null)
        {
            Debug.LogError("PlayerManager ������Ʈ���� ChangeWeapon ��ũ��Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            // ChangeWeapon ��ũ��Ʈ�� equip1�� true�� ����
            if (changeWeapon != null)
            {
                changeWeapon.equip1 = true;

                // �� ������Ʈ�� �������� ��Ȱ��ȭ (������Ʈ�� ��� ����)
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = false; // ������Ʈ�� �� ���̰� ó��
                }

                // �÷��̾ ��� ���󰡵��� �ϴ� �ڵ� �߰�
                StartCoroutine(FollowPlayer(collision.transform));
            }
        }
    }

    // �÷��̾ ��� ���󰡴� �Լ�
    private IEnumerator FollowPlayer(Transform playerTransform)
    {
        while (true) // ���� ������ ���� ��� �÷��̾ ����
        {
            if (playerTransform != null) // �÷��̾ ������ ���� ����
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, 30 * Time.deltaTime);
            }
            yield return null; // �� ������ ���
        }
    }
    
    public void Shoot()
    {
        if (CanAttack == false)
        {
            Debug.Log("CanAttack is false");
        }
        Animator PlayerAnimator = Player.GetComponent<Animator>();
        if (CanAttack && Time.time >= nextShootTime)
        {
            // groundcheck�� ��ġ�� �������� �Ѿ��� �̵� ���� ����
            Vector2 bulletDirection = groundcheck.position.x < transform.position.x ? Vector2.right : Vector2.left;

            // �Ѿ��� �����ϰ� ������ ����
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(bulletDirection, bulletSpeed);
            }
            PlayerAnimator.SetTrigger("Attack");

            // ���� �߻� ���� �ð� ������Ʈ
            nextShootTime = Time.time + cooldownTime;
        }
    }
}

