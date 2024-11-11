using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
{
    public GameObject TestWeapon;  // Gun ��ũ��Ʈ�� ������ �ִ� ���� ������Ʈ
    public Aim aim;
    public WeaponSword Sword;
    public GameObject SwordWeapon;
    public GameObject GloveWeapon;

    public bool equip1;
    public bool equip2;
    public bool equip3;
    public bool equip4;

    public SpriteRenderer playerSpriteRenderer;  // �÷��̾� ��������Ʈ ������
    public Sprite newSprite1;  // ���� ������ ��������Ʈ
    public Sprite newSprite2;
    public Sprite newSprite3;
    public Sprite newSprite4;

    // buttonSpriteRenderer���� Image Ÿ������ ����
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    void Start()
    {
        // TestWeapon ������Ʈ���� Gun ��ũ��Ʈ ã��
        if (TestWeapon != null)
        {

        }
        else
        {
            Debug.LogError("TestWeapon�� �������� �ʾҽ��ϴ�.");
        }
    }

    private void Update()
    {
        if (button1 != null)
        {
            if (equip1 == true)
            {
                ColorBlock colorBlock = button1.colors;
                colorBlock.normalColor = Color.white;
                colorBlock.highlightedColor = Color.cyan;
                colorBlock.pressedColor = Color.white;
                colorBlock.selectedColor = Color.white;
                colorBlock.disabledColor = Color.white;
                button1.colors = colorBlock;  // ����� ColorBlock�� ��ư�� ����
            }
            if (equip2 == true)
            {
                ColorBlock colorBlock = button2.colors;
                colorBlock.normalColor = Color.white;
                colorBlock.highlightedColor = Color.cyan;
                colorBlock.pressedColor = Color.white;
                colorBlock.selectedColor = Color.white;
                colorBlock.disabledColor = Color.white;
                button2.colors = colorBlock;  // ����� ColorBlock�� ��ư�� ����
            }
            if (equip3 == true)
            {
                ColorBlock colorBlock = button3.colors;
                colorBlock.normalColor = Color.white;
                colorBlock.highlightedColor = Color.cyan;
                colorBlock.pressedColor = Color.white;
                colorBlock.selectedColor = Color.white;
                colorBlock.disabledColor = Color.white;
                button3.colors = colorBlock;  // ����� ColorBlock�� ��ư�� ����
            }
            if (equip4 == true)
            {
                ColorBlock colorBlock = button4.colors;
                colorBlock.normalColor = Color.white;
                colorBlock.highlightedColor = Color.cyan;
                colorBlock.pressedColor = Color.white;
                colorBlock.selectedColor = Color.white;
                colorBlock.disabledColor = Color.white;
                button4.colors = colorBlock;  // ����� ColorBlock�� ��ư�� ����
            }
        }
    }

    // ��ư�� Ŭ������ �� ����Ǵ� �Լ�
    public void OnChangeSprite1()
    {
        if (equip1 == true)
        {
            GloveWeapon.SetActive(false);
            SwordWeapon.SetActive(false);
            playerSpriteRenderer.sprite = newSprite1;  // �÷��̾��� ��������Ʈ�� ���ο� ������ ����
            aim.OnAim();
            aim.Button();
        }
    }

    public void OnChangeSprite2()
    {
        if (equip2 == true)
        {
            GloveWeapon.SetActive(false);
            SwordWeapon.SetActive(true);
            aim.ButtonDown();

        }
    }

    public void OnChangeSprite3()
    {
        if (equip3 == true)
        {
            GloveWeapon.SetActive(true);
            SwordWeapon.SetActive(false);
            aim.ButtonDown();
        }
    }

    public void OnChangeSprite4()
    {
        if (equip4 == true)
        {
            GloveWeapon.SetActive(false);
            SwordWeapon.SetActive(false);
            aim.ButtonDown();
        }
    }
}



