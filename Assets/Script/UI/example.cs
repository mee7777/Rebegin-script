using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class example : MonoBehaviour
{
    Rigidbody2D rigid; //�����̵��� ���� ���� ���� 

    private void Awake()
    {

        rigid = GetComponent<Rigidbody2D>(); //���� �ʱ�ȭ 

    }

    // Update is called once per frame
    public float maxSpeed; //�ִ� �ӷ� ���� 

    void FixedUpdate()
    {
        //Move by Key
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //Max speed Right
        if (rigid.velocity.x > maxSpeed)  //���������� �̵� (+) , �ִ� �ӷ��� ������ 
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); //�ش� ������Ʈ�� �ӷ��� maxSpeed 

        //Max speed left
        else if (rigid.velocity.x < maxSpeed * (-1)) // �������� �̵� (-) 
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y); //y���� ������ �����̹Ƿ� 0���� ������ �θ� �ȵ� 

    }
}
