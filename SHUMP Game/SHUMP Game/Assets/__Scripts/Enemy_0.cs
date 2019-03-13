using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : Enemy
{
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void Move()
    {//adjusting the position of the enemy whenever Move() is called(every frame).
        Vector3 tempPos = transform.position;
        tempPos.y -= speed * Time.deltaTime;//will cause the enemy to move down
        pos = tempPos;
    }
}
