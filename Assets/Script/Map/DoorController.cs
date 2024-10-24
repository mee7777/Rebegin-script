using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject textmanager;
    private TextManager text;

    private void Start()
    {
        text = textmanager.GetComponent<TextManager>();
    }
    public void OpenDoor()
    {
        // ���� ������ ���� (�ִϸ��̼� �Ǵ� �̵�)
        Debug.Log("���� �����ϴ�!");
        // ���⿡ ���� ������ �ڵ� �߰� (��: Transform �̵� �Ǵ� �ִϸ��̼� ����)
        gameObject.SetActive(false);  // ���÷� ���� ��Ȱ��ȭ�Ͽ� ������ ȿ��
        text.isOpen = true;
        text.OpenDoor();
        Invoke("closeDoor", 2f);

    }

    void closeDoor()
    {
        text.isOpen = false;
        text.OpenDoor();
    }


}
