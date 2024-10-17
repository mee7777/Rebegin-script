using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerHealth playerHealth;  // PlayerHealth 스크립트 참조
    public int damage = 1;  // 몬스터가 주는 피해량

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