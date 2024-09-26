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
        if (CanAttack == false)
        {
            Debug.Log("CanAttack is false");
        }
        Animator PlayerAnimator = Player.GetComponent<Animator>();
        if (CanAttack && Time.time >= nextShootTime)
        {
            // groundcheck의 위치를 기준으로 총알의 이동 방향 결정
            Vector2 bulletDirection = groundcheck.position.x < transform.position.x ? Vector2.right : Vector2.left;

            // 총알을 생성하고 방향을 설정
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(bulletDirection, bulletSpeed);
            }
            PlayerAnimator.SetTrigger("Attack");

            // 다음 발사 가능 시간 업데이트
            nextShootTime = Time.time + cooldownTime;
        }
    }
}

