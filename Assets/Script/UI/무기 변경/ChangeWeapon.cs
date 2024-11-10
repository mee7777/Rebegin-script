using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
{
    public GameObject TestWeapon;  // Gun 스크립트를 가지고 있는 무기 오브젝트
    public Aim aim;

    public bool equip1;
    public bool equip2;
    public bool equip3;
    public bool equip4;

    public SpriteRenderer playerSpriteRenderer;  // 플레이어 스프라이트 렌더러
    public Sprite newSprite1;  // 새로 적용할 스프라이트
    public Sprite newSprite2;
    public Sprite newSprite3;
    public Sprite newSprite4;

    // buttonSpriteRenderer들을 Image 타입으로 변경
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    public Image AttackImage;
    public Sprite newAttackImage1;
    public Sprite newAttackImage2;
    public Sprite newAttackImage3;
    public Sprite newAttackImage4;

    void Start()
    {
        // TestWeapon 오브젝트에서 Gun 스크립트 찾기
        if (TestWeapon != null)
        {

        }
        else
        {
            Debug.LogError("TestWeapon이 설정되지 않았습니다.");
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
                button1.colors = colorBlock;  // 변경된 ColorBlock을 버튼에 적용
            }
            if (equip2 == true)
            {
                ColorBlock colorBlock = button2.colors;
                colorBlock.normalColor = Color.white;
                colorBlock.highlightedColor = Color.white;
                colorBlock.pressedColor = Color.white;
                colorBlock.selectedColor = Color.white;
                colorBlock.disabledColor = Color.white;
                button2.colors = colorBlock;  // 변경된 ColorBlock을 버튼에 적용
            }
            if (equip3 == true)
            {
                ColorBlock colorBlock = button3.colors;
                colorBlock.normalColor = Color.white;
                colorBlock.highlightedColor = Color.white;
                colorBlock.pressedColor = Color.white;
                colorBlock.selectedColor = Color.white;
                colorBlock.disabledColor = Color.white;
                button3.colors = colorBlock;  // 변경된 ColorBlock을 버튼에 적용
            }
            if (equip4 == true)
            {
                ColorBlock colorBlock = button4.colors;
                colorBlock.normalColor = Color.white;
                colorBlock.highlightedColor = Color.white;
                colorBlock.pressedColor = Color.white;
                colorBlock.selectedColor = Color.white;
                colorBlock.disabledColor = Color.white;
                button4.colors = colorBlock;  // 변경된 ColorBlock을 버튼에 적용
            }
        }
    }

    // 버튼을 클릭했을 때 실행되는 함수
    public void OnChangeSprite1()
    {
        if (equip1 == true)
        {
            playerSpriteRenderer.sprite = newSprite1;  // 플레이어의 스프라이트를 새로운 것으로 변경
            AttackImage.sprite = newAttackImage1;
            aim.OnAim();
        }
    }

    public void OnChangeSprite2()
    {
        if (equip2 == true)
        {
            AttackImage.sprite = newAttackImage2;
            if (playerSpriteRenderer != null && newSprite2 != null)
            {
                playerSpriteRenderer.sprite = newSprite2; // 플레이어의 스프라이트를 새로운 것으로 변경
            }
        }
    }

    public void OnChangeSprite3()
    {
        if (equip3 == true)
        {
            AttackImage.sprite = newAttackImage3;
            if (playerSpriteRenderer != null && newSprite3 != null)
            {
                playerSpriteRenderer.sprite = newSprite3; // 플레이어의 스프라이트를 새로운 것으로 변경
            }
        }
    }

    public void OnChangeSprite4()
    {
        if (equip4 == true)
        {
            AttackImage.sprite = newAttackImage4;
            if (playerSpriteRenderer != null && newSprite4 != null)
            {
                playerSpriteRenderer.sprite = newSprite4; // 플레이어의 스프라이트를 새로운 것으로 변경
            }
        }
    }
}



