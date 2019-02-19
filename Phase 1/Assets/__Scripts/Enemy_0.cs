using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void Move()
    {
        Vector3 tempPos = transform.position;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
}
