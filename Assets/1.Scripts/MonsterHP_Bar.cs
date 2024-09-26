using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHP_Bar : MonoBehaviour
{
    public string enemyName;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public int atkSpeed;

    public GameObject prfHpBar;
    public GameObject canvas;

    private RectTransform hpBar;
    private Image nowHpbar;

    public float height = 1.7f;
    public float Xchange = 0f;

    void Start()
    {
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        if (name.Equals("Enemy1"))
        {
            SetEnemyStatus("Enemy1", 100, 10, 1);
        }
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + Xchange, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
        if (nowHp <= 0)
        {
            Destroy(this.gameObject);
            Destroy(hpBar.gameObject);
        }
    }

    private void SetEnemyStatus(string _enemyName, int _maxHp, int _atkDmg, int _atkSpeed)
    {
        enemyName = _enemyName;
        maxHp = _maxHp;
        nowHp = _maxHp;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            Bullet bullet = col.GetComponent<Bullet>();
            nowHp -= bullet.Dmg;
            Debug.Log(nowHp);
        }
    }
}
