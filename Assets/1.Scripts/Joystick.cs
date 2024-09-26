using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class move_heart : MonoBehaviour
{
    public Animator animator;
    public VariableJoystick js;
    public float speed; // 조이스틱에 의해 움직일 오브젝트의 속도.
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
        // 스틱이 향해있는 방향을 저장해준다.
        Vector3 dir = new Vector3(js.Horizontal, js.Vertical, 0);

        // Vector의 방향은 유지하지만 크기를 1로 줄인다. 길이가 정규화 되지 않을시 0으로 설정.
        dir.Normalize();

        // 오브젝트의 위치를 dir 방향으로 이동시킨다.
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


