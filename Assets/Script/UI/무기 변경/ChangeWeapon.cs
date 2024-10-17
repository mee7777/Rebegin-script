using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
{
    public GameObject TestWeapon;  // Gun ��ũ��Ʈ�� ������ �ִ� ���� ������Ʈ
    private Gun gun;  // Gun ��ũ��Ʈ ����

    public bool equip1;
    public bool equip2;
    public bool equip3;
    public bool equip4;

    public SpriteRenderer playerSpriteRenderer;  // �÷��̾� ��������Ʈ ������
    public Sprite newSprite1;  // ���� ������ ��������Ʈ
    public Sprite newSprite2;
    public Sprite newSprite3;
    public Sprite newSprite4;

    void Start()
    {
        // TestWeapon ������Ʈ���� Gun ��ũ��Ʈ ã��
        if (TestWeapon != null)
        {
            gun = TestWeapon.GetComponent<Gun>();
            if (gun == null)
            {
                Debug.LogError("TestWeapon���� Gun ������Ʈ�� ã�� �� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogError("TestWeapon�� �������� �ʾҽ��ϴ�.");
        }
    }

    // ��ư�� Ŭ������ �� ����Ǵ� �Լ�
    public void OnChangeSprite1()
    {
        if (equip1 == true)
        {
            if (playerSpriteRenderer != null && newSprite1 != null)
            {
                playerSpriteRenderer.sprite = newSprite1;  // �÷��̾��� ��������Ʈ�� ���ο� ������ ����

                if (gun != null)
                {
                    gun.CanAttack = true;  // Gun ��ũ��Ʈ�� CanAttack �� ����
                    Debug.Log("CanAttack ����: " + gun.CanAttack);  // �α׷� ����Ͽ� ���� Ȯ��
                }
            }
        }
    }
    public void OnChangeSprite2()
    {
        if (equip2 == true)
        {
            if (playerSpriteRenderer != null && newSprite2 != null)
            {
                gun.CanAttack = false;
                playerSpriteRenderer.sprite = newSprite2; // �÷��̾��� ��������Ʈ�� ���ο� ������ ����
            }
        }
    }

    public void OnChangeSprite3()
    {
        if (equip3 == true)
        {
            if (playerSpriteRenderer != null && newSprite3 != null)
            {
                gun.CanAttack = false;
                playerSpriteRenderer.sprite = newSprite3; // �÷��̾��� ��������Ʈ�� ���ο� ������ ����
            }
        }
    }

    public void OnChangeSprite4()
    {
        if (equip4 == true)
        {
            if (playerSpriteRenderer != null && newSprite4 != null)
            {
                gun.CanAttack = false;
                playerSpriteRenderer.sprite = newSprite4; // �÷��̾��� ��������Ʈ�� ���ο� ������ ����
            }
        }
    }
}


