using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_1 : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 1f, 0);
    public Quaternion rotateQ;


    private void Awake()
    {
        rotateQ = Quaternion.Euler(rotationSpeed * Time.deltaTime);
    }


    void Update()
    {
        transform.rotation *= rotateQ;
    }
}
