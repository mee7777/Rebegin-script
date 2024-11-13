using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDictionary : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    // Start is called before the first frame update
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        talkData.Add(1, new string[] { "... �� �������� �༮?" });
        talkData.Add(2, new string[] { "�˰�� ����! ��� �����ϴ���..." });
        talkData.Add(3, new string[] { "�� �����ؾ� �ϳ�" });
        talkData.Add(10000, new string[] { "��� �������� �ո��� ����" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
