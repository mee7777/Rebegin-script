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

    // Update is called once per frame
    public void Action(GameObject scanObj)
    {
        Debug.Log("�ؽ�Ʈ���");
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

        talkText.text = "";  // ���� �ؽ�Ʈ �ʱ�ȭ
        StartCoroutine(TextPrint(delay));  // Ÿ���� ȿ�� ����
    }

    IEnumerator TextPrint(float d)
    {
        int count = 0;
        while (count < targetText.Length)
        {
            talkText.text += targetText[count].ToString();
            count++;
            yield return new WaitForSeconds(d);  // ������ �����̸�ŭ ��ٸ�
        }
    }
}


