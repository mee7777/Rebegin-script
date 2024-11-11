using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveGet : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform

    public GameObject Player;

    private GameObject PlayerController;

    public GameObject PlayerManager; // PlayerManager ������Ʈ ����
    private ChangeWeapon changeWeapon; // ChangeWeapon ��ũ��Ʈ ����

    // Start is called before the first frame update
    void Start()
    {
        // PlayerManager���� ChangeWeapon ��ũ��Ʈ ã��
        if (PlayerManager != null)
        {
            changeWeapon = PlayerManager.GetComponent<ChangeWeapon>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ChangeWeapon ��ũ��Ʈ�� equip1�� true�� ����
            if (changeWeapon != null)
            {
                changeWeapon.equip3 = true;
                gameObject.SetActive(false);
            }
        }
    }
}
