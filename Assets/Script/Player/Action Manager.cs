using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActionManager : MonoBehaviour
{ 
    public GameObject actionManager;

    private void Start()
    {
        actionManager = GetComponent<GameObject>();
        
    }
}
