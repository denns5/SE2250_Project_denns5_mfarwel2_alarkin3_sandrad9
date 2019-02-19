using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    private float randomSpeed;
    // Start is called before the first frame update
    void Start()
    {
        float r=Random.Range(0.0f, 1.0f);
        if (r <0.5f)
        {
            randomSpeed = -speed;
        }
        else
        {
            randomSpeed = speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    override public void Move()
    {
        Vector3 tempPos = pos;
        tempPos.x += randomSpeed * Time.deltaTime;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
}
