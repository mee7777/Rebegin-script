using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    public VariableJoystick js;
    public float speed; // ���̽�ƽ�� ���� ������ ������Ʈ�� �ӵ�.
    Vector3 moveVec;

    // Update is called once per frame
    void Update()
    {
        // ��ƽ�� �����ִ� ������ �������ش�.
        moveVec = new Vector3(js.Horizontal, js.Vertical, 0).normalized;

        moveVec = moveVec * speed;

        // ������Ʈ�� ��ġ�� dir �������� �̵���Ų��.
        rigid.velocity = new Vector3(moveVec.x, moveVec.y, 0);
        if (js.Horizontal == 0)
        {
            
        }
    }

    
}
