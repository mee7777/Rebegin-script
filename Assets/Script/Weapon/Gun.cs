using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour
{
    GameObject PlayerObject;
    private string newTag = "Equiped";

    public GameObject Player;
    public GameObject WeaponPoint;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed;
    public Transform groundcheck;

    private GameObject PlayerController;
    private bool CanAttack = false;
    bool isDrop;
    public bool isPicking;

    Vector3 forceDirection;

    [SerializeField] TextMeshProUGUI pickUpText;

    public float cooldownTime = 1f; // 쿨타임 시간 (초)
    private float nextShootTime = 0f; // 다음 발사 가능 시간

    // Start is called before the first frame update
    void Start()
    {
        pickUpText.gameObject.SetActive(false);
        isPicking = false;
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickUpText.gameObject.SetActive(true);
        }
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GameController"))
        {
            pickUpText.gameObject.SetActive(false);
            CanAttack = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickUpText.gameObject.SetActive(false);
            CanAttack = false;
        }
        
    }
    public void Shoot()
    {
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
