using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour
{
    Text valueText;

    // Start is called before the first frame update
    void Start()
    {
        valueText = GetComponent<Text>();
    }

    public void valueUpdate(float value)
    {
        valueText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
