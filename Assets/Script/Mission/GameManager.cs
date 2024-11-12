using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int targetMonsterCount = 5;

    // 현재 잡은 몬스터 수
    private int currentMonsterCount = 0;

    // 미션이 클리어되었는지 여부
    private bool isMissionCleared = false;

    // 몬스터를 잡을 때 호출되는 함수
    public void MonsterKilled()
    {
        // 이미 미션이 클리어되었으면 함수 종료
        if (isMissionCleared) return;

        // 몬스터 카운트 증가
        currentMonsterCount++;

        // 목표 몬스터 수에 도달하면 미션 클리어
        if (currentMonsterCount >= targetMonsterCount)
        {
            isMissionCleared = true;
            Debug.Log("MissionClear");
        }
    }
}
