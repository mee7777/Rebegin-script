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
        talkData.Add(1, new string[] { "... 그 괴물같은 녀석?" });
        talkData.Add(2, new string[] { "알고야 말고! 어찌나 끔찍하던지..." });
        talkData.Add(3, new string[] { "꼭 조심해야 하네" });
        talkData.Add(10000, new string[] { "어딘가 막힌길이 뚫린것 같다" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
