using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int deathCount = 0;  // ���� ���� ��
    public int totalEnemies = 5;  // ���� �� �� (���ǿ� ���� ���� ����)
    public GameObject door;  // �� ������Ʈ�� ����

    // ���� ���� �� ȣ��� �Լ�
    public void IncreaseDeathCount()
    {
        deathCount++;  // ���� ���� �� ����

        // ��� ���� �׾��� �� �� ����
        if (deathCount >= totalEnemies)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        // ���� ������ ���� (��: �ִϸ��̼� �Ǵ� Transform �̵�)
        Debug.Log("���� �����ϴ�!");
        door.GetComponent<DoorController>().OpenDoor();  // �� ���� ���� ����
    }
}
