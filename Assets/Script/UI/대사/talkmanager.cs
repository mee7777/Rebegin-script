using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    public Text talkText; // UI Text 컴포넌트로 텍스트 출력

    // Start is called before the first frame update
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1, new string[] { "안녕하세요. 반갑습니다1" });
        talkData.Add(2, new string[] { "안녕하세요. 반갑습니다2" });
        talkData.Add(3, new string[] { "안녕하세요. 반갑습니다3" });
    }

    public void StartTalk(int id, int talkIndex)
    {
        string talk = talkData[id][talkIndex];
        StartCoroutine(TypeText(talk)); // 코루틴 시작
    }

    IEnumerator TypeText(string talk)
    {
        talkText.text = ""; // 기존 텍스트 초기화
        foreach (char letter in talk.ToCharArray())
        {
            talkText.text += letter; // 한 글자씩 추가
            yield return new WaitForSeconds(0.1f); // 0.1초 대기 후 다음 글자 출력
        }
    }
}

