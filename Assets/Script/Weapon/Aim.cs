using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    public VariableJoystick js;
    public float speed; // 조이스틱에 의해 움직일 오브젝트의 속도.
    Vector3 moveVec;

    // Update is called once per frame
    void Update()
    {
        // 스틱이 향해있는 방향을 저장해준다.
        moveVec = new Vector3(js.Horizontal, js.Vertical, 0).normalized;

        moveVec = moveVec * speed;

        // 오브젝트의 위치를 dir 방향으로 이동시킨다.
        rigid.velocity = new Vector3(moveVec.x, moveVec.y, 0);
        if (js.Horizontal == 0)
        {
            
        }
    }

    
}
