using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponJS : MonoBehaviour
{
    public VariableJoystick js;
    public float speed;
    public float MS;
    public float range;

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = new Vector3(js.Horizontal, js.Vertical, 0);
        dir.Normalize();
        transform.position += dir * speed * Time.deltaTime;

        GameObject closestMonster = FindClosestMonster();
        if (closestMonster != null)
        {
            Vector3 directionToMonster = closestMonster.transform.position - transform.position;
            directionToMonster.Normalize();

            transform.position += directionToMonster * MS * Time.deltaTime;
        }

        KeepWithinScreenBounds();
    }

    GameObject FindClosestMonster()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject closestMonster = null;
        float closestDistance = range;

        foreach (GameObject monster in monsters)
        {
            float distanceToMonster = Vector3.Distance(transform.position, monster.transform.position);
            if (distanceToMonster < closestDistance)
            {
                closestDistance = distanceToMonster;
                closestMonster = monster;
            }
        }

        return closestMonster;
    }

    void KeepWithinScreenBounds()
    {
        Vector3 pos = transform.position;
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(pos);

        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.0f, 1.0f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.0f, 1.0f);

        transform.position = mainCamera.ViewportToWorldPoint(viewportPos);
    }
}
