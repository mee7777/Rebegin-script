using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class move_heart : MonoBehaviour
{
    public Animator animator;
    public VariableJoystick js;
    public float speed; // ���̽�ƽ�� ���� ������ ������Ʈ�� �ӵ�.
    SpriteRenderer rend;

    private void Awake()
    {
        maxHp = 50;
        nowHp = 50;
        animator = GetComponent<Animator>();
        

        rend = GetComponent<SpriteRenderer>();
        
    }
    void FixedUpdate()
    {
        // ��ƽ�� �����ִ� ������ �������ش�.
        Vector3 dir = new Vector3(js.Horizontal, js.Vertical, 0);

        // Vector�� ������ ���������� ũ�⸦ 1�� ���δ�. ���̰� ����ȭ ���� ������ 0���� ����.
        dir.Normalize();

        // ������Ʈ�� ��ġ�� dir �������� �̵���Ų��.
        transform.position += dir * speed * Time.deltaTime;

       if(js.Horizontal > 0)
        {
            transform.localScale = new Vector3(0.2736f, 0.2736f, 0);
            animator.SetBool("Move", true);
        }
        else if(js.Horizontal < 0)
        {
            transform.localScale = new Vector3(-0.2736f, 0.2736f, 0);
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }


    }

    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public float atkSpeed = 1;
    public bool attacked = false;
    public Image nowHpbar;

    

}


