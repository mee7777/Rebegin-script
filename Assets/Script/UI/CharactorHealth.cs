using UnityEngine;

public class CharactorHealth : MonoBehaviour
{
    public Player playerStats; // Player ��ũ��Ʈ ����
    public SpriteRenderer healthSpriteRenderer; // Health SpriteRenderer ����
    public Sprite firstImage; // Count �̻��� �� ����� ù ��° �̹���
    public Sprite secondImage; // Count ������ �� ����� �� ��° �̹���

    public int Count = 2; // ù ��° �̹����� ����� ���� ��

    void Start()
    {
        UpdateHealthUI(); // �ʱ� ��������Ʈ ����
    }

    void Update()
    {
        UpdateHealthUI(); // �� �����Ӹ��� ��������Ʈ ������Ʈ
    }

    // Health ���¿� ���� ��������Ʈ ����
    void UpdateHealthUI()
    {
        int currentHealth = playerStats.currentHealth;
        // Health�� Count���� Ŭ �� ù ��° ��������Ʈ�� ����
        if (currentHealth > Count)
        {
            healthSpriteRenderer.sprite = firstImage;
        }
        // Health�� Count ������ �� �� ��° ��������Ʈ�� ����
        else
        {
            healthSpriteRenderer.sprite = secondImage;
        }
    }
}

