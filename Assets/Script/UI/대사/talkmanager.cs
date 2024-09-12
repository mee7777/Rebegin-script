using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    public Text talkText; // UI Text ������Ʈ�� �ؽ�Ʈ ���

    // Start is called before the first frame update
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1, new string[] { "�ȳ��ϼ���. �ݰ����ϴ�1" });
        talkData.Add(2, new string[] { "�ȳ��ϼ���. �ݰ����ϴ�2" });
        talkData.Add(3, new string[] { "�ȳ��ϼ���. �ݰ����ϴ�3" });
    }

    public void StartTalk(int id, int talkIndex)
    {
        string talk = talkData[id][talkIndex];
        StartCoroutine(TypeText(talk)); // �ڷ�ƾ ����
    }

    IEnumerator TypeText(string talk)
    {
        talkText.text = ""; // ���� �ؽ�Ʈ �ʱ�ȭ
        foreach (char letter in talk.ToCharArray())
        {
            talkText.text += letter; // �� ���ھ� �߰�
            yield return new WaitForSeconds(0.1f); // 0.1�� ��� �� ���� ���� ���
        }
    }
}

