using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public void OpenDoor()
    {
        // ���� ������ ���� (�ִϸ��̼� �Ǵ� �̵�)
        Debug.Log("���� �����ϴ�!");
        // ���⿡ ���� ������ �ڵ� �߰� (��: Transform �̵� �Ǵ� �ִϸ��̼� ����)
        gameObject.SetActive(false);  // ���÷� ���� ��Ȱ��ȭ�Ͽ� ������ ȿ��
    }
}
