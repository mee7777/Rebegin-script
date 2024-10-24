using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide : MonoBehaviour
{
    public GameObject talkPanel;

    // Start is called before the first frame update
    void Start()
    {
        talkPanel.SetActive(false);
    }
}
