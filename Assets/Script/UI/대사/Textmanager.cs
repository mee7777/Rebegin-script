using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public TextDictionary TextDictionary;
    public GameObject talkPanel;
    public TMP_Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public float delay = 0.05f;
    private string targetText;
    public int talkIndex;
    public bool isOpen;
    string OpenMessage = "어딘가 막힌 길이 뚫린 것 같다";

    public void Action(GameObject scanObj)
    {
        Debug.Log("텍스트 출력");
        if (isAction)
        {
            isAction = false;
            talkPanel.SetActive(true);
            scanObject = scanObj;
            ObjData objData = scanObject.GetComponent<ObjData>();
            Talk(objData.id, objData.isNpc);
        }
        else
        {
            isAction = true;
            talkPanel.SetActive(false);
        }
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = TextDictionary.GetTalk(id, talkIndex);

        if (isNpc)
        {
            targetText = talkData;
        }
        else
        {
            targetText = talkData;
        }

        talkText.text = "";  // 기존 텍스트 초기화
        StartCoroutine(TextPrint(delay));  // 타이핑 효과 실행
    }

    IEnumerator TextPrint(float d)
    {
        int count = 0;
        while (count < targetText.Length)
        {
            talkText.text += targetText[count].ToString();
            count++;
            yield return new WaitForSeconds(d);  // 지정한 딜레이만큼 기다림
        }
    }

    public void OpenDoor()
    {
        if (isOpen)
        {
            talkPanel.SetActive(true);
            targetText = OpenMessage;  // 문이 열렸을 때 텍스트 설정
            talkText.text = "";  // 기존 텍스트 초기화
            StartCoroutine(TextPrint(delay));  // 타이핑 효과 실행
        }
        else
        {
            talkPanel.SetActive(false);
        }
    }
}



