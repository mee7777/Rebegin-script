using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveGet : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform

    public GameObject Player;

    private GameObject PlayerController;

    public GameObject PlayerManager; // PlayerManager 오브젝트 참조
    private ChangeWeapon changeWeapon; // ChangeWeapon 스크립트 참조

    // Start is called before the first frame update
    void Start()
    {
        // PlayerManager에서 ChangeWeapon 스크립트 찾기
        if (PlayerManager != null)
        {
            changeWeapon = PlayerManager.GetComponent<ChangeWeapon>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ChangeWeapon 스크립트의 equip1을 true로 설정
            if (changeWeapon != null)
            {
                changeWeapon.equip3 = true;
                gameObject.SetActive(false);
            }
        }
    }
}
