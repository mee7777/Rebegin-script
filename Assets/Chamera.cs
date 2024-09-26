using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamera : MonoBehaviour
{
    [SerializeField] GameObject target;
    
    private Vector3 dir;
    void Start()
    {
        dir = Camera.main.transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position =  target.transform.position + dir;
    }
}
