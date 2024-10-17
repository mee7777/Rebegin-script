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

    public float cooldownTime = 1f; // ��Ÿ�� �ð� (��)
    private float nextShootTime = 0f; // ���� �߻� ���� �ð�


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
            // ���� ��ġ���� Ÿ�� ��ġ������ ���� ���͸� ���
            Vector3 direction = target.transform.position - transform.position;

            // 2D ��鿡�� ȸ���� ���� ���� ���� ��� (Y�� ����)
            direction.z = 0; // Z�ุ ����ϱ� ���� X��� Y���� ���� ����

            if (direction.sqrMagnitude > 0) // ���� ������ ũ�Ⱑ 0�� �ƴ� ��쿡�� ȸ��
            {
                // ���� ���͸� �������� 2D���� ȸ���� Quaternion ����
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // ���� ȸ������ ��ǥ ȸ������ Slerp
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
            // ������Ʈ�� �ٶ󺸴� ������ �������� �Ѿ��� �̵� ���� ����
            Vector2 bulletDirection = transform.right; // �Ǵ� transform.up, ���ϴ� �������� ����

            // �Ѿ��� �����ϰ� ������ ����
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(bulletDirection, bulletSpeed);
            }
            

            // �ִϸ��̼� Ʈ���� ����
            playerAnimator.SetTrigger("Attack");

            // ���� �߻� ���� �ð� ������Ʈ
            nextShootTime = Time.time + cooldownTime;
        }
    }
}
