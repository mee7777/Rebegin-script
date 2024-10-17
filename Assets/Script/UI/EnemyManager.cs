using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int deathCount = 0;  // 죽은 적의 수
    public int totalEnemies = 5;  // 적의 총 수 (조건에 따라 수정 가능)
    public GameObject door;  // 문 오브젝트를 연결

    // 적이 죽을 때 호출될 함수
    public void IncreaseDeathCount()
    {
        deathCount++;  // 죽은 적의 수 증가

        // 모든 적이 죽었을 때 문 열기
        if (deathCount >= totalEnemies)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        // 문이 열리는 동작 (예: 애니메이션 또는 Transform 이동)
        Debug.Log("문이 열립니다!");
        door.GetComponent<DoorController>().OpenDoor();  // 문 여는 동작 실행
    }
}
