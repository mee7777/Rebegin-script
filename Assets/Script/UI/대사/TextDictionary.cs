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
        talkData.Add(100, new string[] { "테스트용입니다 1번" });
        talkData.Add(101, new string[] { "테스트용입니다 2번" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
