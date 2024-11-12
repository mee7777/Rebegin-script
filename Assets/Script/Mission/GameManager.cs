using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int targetMonsterCount = 5;

    // ���� ���� ���� ��
    private int currentMonsterCount = 0;

    // �̼��� Ŭ����Ǿ����� ����
    private bool isMissionCleared = false;

    // ���͸� ���� �� ȣ��Ǵ� �Լ�
    public void MonsterKilled()
    {
        // �̹� �̼��� Ŭ����Ǿ����� �Լ� ����
        if (isMissionCleared) return;

        // ���� ī��Ʈ ����
        currentMonsterCount++;

        // ��ǥ ���� ���� �����ϸ� �̼� Ŭ����
        if (currentMonsterCount >= targetMonsterCount)
        {
            isMissionCleared = true;
            Debug.Log("MissionClear");
        }
    }
}
