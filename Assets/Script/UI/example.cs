using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class example : MonoBehaviour
{
    Rigidbody2D rigid; //물리이동을 위한 변수 선언 

    private void Awake()
    {

        rigid = GetComponent<Rigidbody2D>(); //변수 초기화 

    }

    // Update is called once per frame
    public float maxSpeed; //최대 속력 변수 

    void FixedUpdate()
    {
        //Move by Key
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //Max speed Right
        if (rigid.velocity.x > maxSpeed)  //오른쪽으로 이동 (+) , 최대 속력을 넘으면 
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); //해당 오브젝트의 속력은 maxSpeed 

        //Max speed left
        else if (rigid.velocity.x < maxSpeed * (-1)) // 왼쪽으로 이동 (-) 
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y); //y값은 점프의 영향이므로 0으로 제한을 두면 안됨 

    }
}
