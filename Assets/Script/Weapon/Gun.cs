using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform

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

                // ���� ������Ʈ�� �÷��̾��� �ڽ����� ����
                transform.SetParent(collision.transform); // ������Ʈ�� �÷��̾��� ���� ������Ʈ�� ����
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
        Animator PlayerAnimator = Player.GetComponent<Animator>();
        if (CanAttack && Time.time >= nextShootTime)
        {
            // �÷��̾�� �Ѿ� ���� �Ÿ� ���
            Vector3 bulletPosition = player.position; // �Ѿ��� �߻�� ��ġ�� �÷��̾��� ��ġ

            // �Ѿ� ����
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);

            // �÷��̾�� �Ѿ� ���� ��� ��ġ ���
            Vector3 direction = bulletPosition - bullet.transform.position;

            // �Ѿ��� ���⿡ ���� �߻� ���� ����
            if (direction.x < 0)
            {
                // �������� �߻�
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 0f); // �������� �߻�
            }
            else
            {
                // ���������� �߻�
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 0f); // ���������� �߻�
            }
        }
    }

}

