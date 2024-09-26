using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
{
    public GameObject TestWeapon;  // Gun 스크립트를 가지고 있는 무기 오브젝트
    private Gun gun;  // Gun 스크립트 참조

    public bool equip1;
    public bool equip2;
    public bool equip3;
    public bool equip4;

    public SpriteRenderer playerSpriteRenderer;  // 플레이어 스프라이트 렌더러
    public Sprite newSprite1;  // 새로 적용할 스프라이트
    public Sprite newSprite2;
    public Sprite newSprite3;
    public Sprite newSprite4;

    void Start()
    {
        // TestWeapon 오브젝트에서 Gun 스크립트 찾기
        if (TestWeapon != null)
        {
            gun = TestWeapon.GetComponent<Gun>();
            if (gun == null)
            {
                Debug.LogError("TestWeapon에서 Gun 컴포넌트를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogError("TestWeapon이 설정되지 않았습니다.");
        }
    }

    // 버튼을 클릭했을 때 실행되는 함수
    public void OnChangeSprite1()
    {
        if (equip1 == true)
        {
            if (playerSpriteRenderer != null && newSprite1 != null)
            {
                playerSpriteRenderer.sprite = newSprite1;  // 플레이어의 스프라이트를 새로운 것으로 변경

                if (gun != null)
                {
                    gun.CanAttack = true;  // Gun 스크립트의 CanAttack 값 변경
                    Debug.Log("CanAttack 상태: " + gun.CanAttack);  // 로그로 출력하여 상태 확인
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
                playerSpriteRenderer.sprite = newSprite2; // 플레이어의 스프라이트를 새로운 것으로 변경
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
                playerSpriteRenderer.sprite = newSprite3; // 플레이어의 스프라이트를 새로운 것으로 변경
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
                playerSpriteRenderer.sprite = newSprite4; // 플레이어의 스프라이트를 새로운 것으로 변경
            }
        }
    }
}


