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
    bool isPlayerEnter;

    [SerializeField] TextMeshProUGUI pickUpText;

    public float cooldownTime = 1f; // ��Ÿ�� �ð� (��)
    private float nextShootTime = 0f; // ���� �߻� ���� �ð�

    // Start is called before the first frame update
    void Start()
    {
        pickUpText.gameObject.SetActive(false);
        isPicking = false;
    }

    // Update is called once per frame

    public void SetPickUpText(TextMeshProUGUI newText)
    {
        pickUpText = newText;
    }

    public void SetIsPicking(bool value)
    {
        isPicking = value;

        UpdateGunner();
    }

    private void UpdateGunner()
    {
        Gunner gunner = FindObjectOfType<Gunner>();
        if (gunner != null)
        {
            gunner.SetIsPicking(isPicking);
        }
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
            Destroy(this.gameObject);
            isPicking = false;
        }
    }
    public void Shoot()
    {
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
