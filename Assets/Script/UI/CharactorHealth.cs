using UnityEngine;

public class CharactorHealth : MonoBehaviour
{
    public Player playerStats; // Player 스크립트 참조
    public SpriteRenderer healthSpriteRenderer; // Health SpriteRenderer 참조
    public Sprite firstImage; // Count 이상일 때 사용할 첫 번째 이미지
    public Sprite secondImage; // Count 이하일 때 사용할 두 번째 이미지

    public int Count = 2; // 첫 번째 이미지로 변경될 기준 값

    void Start()
    {
        UpdateHealthUI(); // 초기 스프라이트 설정
    }

    void Update()
    {
        UpdateHealthUI(); // 매 프레임마다 스프라이트 업데이트
    }

    // Health 상태에 따라 스프라이트 변경
    void UpdateHealthUI()
    {
        int currentHealth = playerStats.currentHealth;
        // Health가 Count보다 클 때 첫 번째 스프라이트로 설정
        if (currentHealth > Count)
        {
            healthSpriteRenderer.sprite = firstImage;
        }
        // Health가 Count 이하일 때 두 번째 스프라이트로 설정
        else
        {
            healthSpriteRenderer.sprite = secondImage;
        }
    }
}

