using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActionManager : MonoBehaviour
{ 
    public GameObject actionManager;

    public Button WHB1;
    public Button WHB2;
    public Button WHB3;
    public Button WHB4;

    private bool areButtonsVisible = false;

    private void Start()
    {
        actionManager = GetComponent<GameObject>();

        WHB1.gameObject.SetActive(false);
        WHB2.gameObject.SetActive(false);
        WHB3.gameObject.SetActive(false);
        WHB4.gameObject.SetActive(false);
    }

    public void OnButtonClick()
    {
        areButtonsVisible = !areButtonsVisible;

        WHB1.gameObject.SetActive(areButtonsVisible);
        WHB2.gameObject.SetActive(areButtonsVisible);
        WHB3.gameObject.SetActive(areButtonsVisible);
        WHB4.gameObject.SetActive(areButtonsVisible);
    }
}
