using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;

    public float jumpForce = 10f;
    public float skillChargeTime = 3f; // 스킬이 나가는 시간
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Button actionButton;
    public Button jumpButton;
    public Text buttonText;
    public GameObject WeaponPoint;
    public GameObject Weaponmanager;
    
    public GameObject[] weapons;
    public bool[] hasWeapon;

    private Rigidbody2D rb;
    
    private bool moveLeft;
    private bool moveRight;
    private float attackHoldTime;
    private bool isAttacking;    
    private bool isHit;
    public bool Dropping = false;
    public bool isJump = false;
    bool isPicking;

    private GameObject nearObject;


   
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        attackHoldTime = 0f;
        isAttacking = false;        
    }

    void Update()
    {
        
    }

    public void Jump()
    {
        if (!isJump)
        {
            isJump = true;
            rigid.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Equals("Ground"))
        {
            isJump = false;
        }
    }



    public void UseSkill()
    {
        // 스킬 로직
        Debug.Log("Using Skill!");
    }

    public void OnMoveButtonClick()
    {
        moveLeft = !moveLeft;
        moveRight = !moveRight;
    }


    public void OnJumpButtonClick()
    {
        Jump();
    }
        
    public void Pickup (GameObject Weapon)
    {
        if (isPicking) Drop();
        SetEquip(Weapon, true);
        isPicking = true;
    }

    public void Drop()
    {
        GameObject Weapon = WeaponPoint.GetComponentInChildren<Rigidbody2D>().gameObject;
        SetEquip(Weapon, false);
        Weapon.transform.SetParent(Weaponmanager.transform);
        isPicking = false;
        Destroy(Weapon);
    }

    void SetEquip (GameObject Weapon, bool isEquip)
    {
        Collider2D[] WeaponColliders = Weapon.GetComponents<Collider2D>();
        Rigidbody2D WeaponRigidbody = Weapon.GetComponent<Rigidbody2D>();

        foreach(Collider2D WeaponCollider in WeaponColliders)
        {
            WeaponCollider.enabled = !isEquip;
        }
        WeaponRigidbody.isKinematic = isEquip;
    }
}

