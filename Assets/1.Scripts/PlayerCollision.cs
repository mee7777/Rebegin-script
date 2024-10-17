using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerHealth playerHealth;  // PlayerHealth ��ũ��Ʈ ����
    public int damage = 1;  // ���Ͱ� �ִ� ���ط�

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            PlayerInteraction playerInteraction = GetComponent<PlayerInteraction>();
            if (playerInteraction != null && playerInteraction.isInteracting)
            {
                playerInteraction.StopInteraction();
            }
        }
    }
}