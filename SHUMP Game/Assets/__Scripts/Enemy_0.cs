using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : Enemy
{
    public float powerUpDropChance = 1f;
    
    private int _health = 2;
    private int _points = 200;
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

    public override void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        if (otherGO.tag == "ProjectileHero")
        {
            Destroy(otherGO);
            if (_health == 1)
            {
                ScoreManager.UpdateScore(_points);
                TextManager.UpdateText();
                Destroy(gameObject);
                Main.S.ShipDestoryed(this);
            }
            else
            {
                _health = _health - 1;
            }
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }
}
