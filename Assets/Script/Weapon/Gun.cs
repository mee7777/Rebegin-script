using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform

    public GameObject Player;
    public GameObject WeaponPoint;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed;
    public Transform groundcheck;

    private GameObject PlayerController;
    public bool CanAttack;

    Vector3 forceDirection;

    public float cooldownTime = 1f; // 쿨타임 시간 (초)
    private float nextShootTime = 0f; // 다음 발사 가능 시간

    public GameObject PlayerManager; // PlayerManager 오브젝트 참조
    private ChangeWeapon changeWeapon; // ChangeWeapon 스크립트 참조

    // Start is called before the first frame update
    void Start()
    {
        CanAttack = false;
        // PlayerManager에서 ChangeWeapon 스크립트 찾기
        if (PlayerManager != null)
        {
            changeWeapon = PlayerManager.GetComponent<ChangeWeapon>();
        }

        if (changeWeapon == null)
        {
            Debug.LogError("PlayerManager 오브젝트에서 ChangeWeapon 스크립트를 찾을 수 없습니다.");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ChangeWeapon 스크립트의 equip1을 true로 설정
            if (changeWeapon != null)
            {
                changeWeapon.equip1 = true;

                // 이 오브젝트의 렌더러만 비활성화 (오브젝트는 계속 동작)
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = false; // 오브젝트가 안 보이게 처리
                }

                // 플레이어를 계속 따라가도록 하는 코드 추가
                StartCoroutine(FollowPlayer(collision.transform));

                // 현재 오브젝트를 플레이어의 자식으로 설정
                transform.SetParent(collision.transform); // 오브젝트를 플레이어의 하위 오브젝트로 설정
            }
        }
    }


    // 플레이어를 계속 따라가는 함수
    private IEnumerator FollowPlayer(Transform playerTransform)
    {
        while (true) // 무한 루프를 돌며 계속 플레이어를 추적
        {
            if (playerTransform != null) // 플레이어가 존재할 때만 추적
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, 30 * Time.deltaTime);
            }
            yield return null; // 한 프레임 대기
        }
    }

    public void Shoot()
    {
        Animator PlayerAnimator = Player.GetComponent<Animator>();
        if (CanAttack && Time.time >= nextShootTime)
        {
            // 플레이어와 총알 간의 거리 계산
            Vector3 bulletPosition = player.position; // 총알이 발사될 위치는 플레이어의 위치

            // 총알 생성
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);

            // 플레이어와 총알 간의 상대 위치 계산
            Vector3 direction = bulletPosition - bullet.transform.position;

            // 총알의 방향에 따라 발사 방향 설정
            if (direction.x < 0)
            {
                // 왼쪽으로 발사
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 0f); // 왼쪽으로 발사
            }
            else
            {
                // 오른쪽으로 발사
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 0f); // 오른쪽으로 발사
            }
        }
    }

}

