using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gunner : MonoBehaviour
{
    public GameObject[] objectToDisable;
    GameObject PlayerObject;
    private string newTag = "Equiped";

    public GameObject Player;
    public GameObject WeaponPoint;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed;
    public Transform groundcheck;
    public bool isPicking;

    private GameObject PlayerController;
    private bool CanAttack = false;
    bool isDrop;

    Vector3 forceDirection;
    bool isPlayerEnter;

    [SerializeField] TextMeshProUGUI pickUpText;

    public float cooldownTime = 1f; // 쿨타임 시간 (초)
    private float nextShootTime = 0f; // 다음 발사 가능 시간


    public void SetIsPicking(bool value)
    {
        isPicking = value;
    }

    void Start()
    {
        isPicking = false;
        pickUpText.gameObject.SetActive(false);
        foreach (GameObject obj in objectToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    void Update()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Attack");
        if (target != null)
        {
            // 현재 위치에서 타겟 위치까지의 방향 벡터를 계산
            Vector3 direction = target.transform.position - transform.position;

            // 2D 평면에서 회전을 위한 방향 벡터 계산 (Y축 무시)
            direction.z = 0; // Z축만 고려하기 위해 X축과 Y축의 값을 유지

            if (direction.sqrMagnitude > 0) // 방향 벡터의 크기가 0이 아닌 경우에만 회전
            {
                // 방향 벡터를 기준으로 2D에서 회전할 Quaternion 생성
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // 현재 회전에서 목표 회전으로 Slerp
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 30f);
            }
        }
    }

    public void SetPickUpText(TextMeshProUGUI newText)
    {
        pickUpText = newText;
    }

    public void ChangeWeapon()
    {
        if (isPicking == false)
        {
            if (isPlayerEnter)
            {
                transform.SetParent(WeaponPoint.transform);
                transform.localPosition = Vector3.zero;
                transform.rotation = Quaternion.identity;
                tag = newTag;

                isPlayerEnter = false;
                isPicking = true;
                foreach (GameObject obj in objectToDisable)
                {
                    if (obj != null)
                    {
                        obj.SetActive(true);
                    }
                }
            }
        }
        else
        {
            Drop();
            if (isPlayerEnter)
            {                
                transform.SetParent(WeaponPoint.transform);
                transform.localPosition = Vector3.zero;
                transform.rotation = Quaternion.identity;
                tag = newTag;

                isPlayerEnter = false;
                isPicking = true;
                foreach (GameObject obj in objectToDisable)
                {
                    if (obj != null)
                    {
                        obj.SetActive(true);
                    }
                }
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerEnter = true;
            pickUpText.gameObject.SetActive(true);
        }
        if (collision.gameObject.CompareTag("GameController"))
        {
            isDrop = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GameController"))
        {
            pickUpText.gameObject.SetActive(false);
            CanAttack = true;
        }
        if (collision.gameObject.CompareTag("GameController"))
        {
            isDrop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerEnter = false;
            pickUpText.gameObject.SetActive(false);
            CanAttack = false;
        }
        if (collision.gameObject.CompareTag("GameController"))
        {
            isDrop = false;
        }
    }
    public void Drop()
    {
        if (isDrop)
        {
            isPicking = false;
            Destroy(this.gameObject);
            foreach (GameObject obj in objectToDisable)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    public void Shoot()
    {
        Animator playerAnimator = Player.GetComponent<Animator>();

        if (CanAttack && Time.time >= nextShootTime)
        {
            // 오브젝트의 바라보는 방향을 기준으로 총알의 이동 방향 결정
            Vector2 bulletDirection = transform.right; // 또는 transform.up, 원하는 방향으로 설정

            // 총알을 생성하고 방향을 설정
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(bulletDirection, bulletSpeed);
            }
            

            // 애니메이션 트리거 설정
            playerAnimator.SetTrigger("Attack");

            // 다음 발사 가능 시간 업데이트
            nextShootTime = Time.time + cooldownTime;
        }
    }
}
