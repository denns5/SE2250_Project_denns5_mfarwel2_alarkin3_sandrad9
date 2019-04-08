using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : Enemy
{
    public GameObject explosion;
    private int _health = 2;
    private int _points = 20;
    private GameObject _lastTriggeredGo = null;

    void Update()
    {
        Move();
    }

    public override void Move()
    {//adjusting the position of the enemy whenever Move() is called(every frame).
        Vector3 tempPos = transform.position;
        tempPos.y -= (speed + ScoreManager.LEVEL) * Time.deltaTime;//will cause the enemy to move down
        pos = tempPos;
    }

    int getPoints()
    {
        return _points;
    }

    public override void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        if (otherGO.tag == "ProjectileHero")
        {
            if (otherGO == _lastTriggeredGo)
            {
                return;
            }
            _lastTriggeredGo = otherGO;
            Destroy(otherGO);
            if (_health <= 1)
            {
                ScoreManager.UpdateScore(_points);//update the score
                TextManager.UpdateText();//update the text
                Main.S.ShipDestoryed(this,0);//telling main that a ship was destroyed to possibly spawn a power up
                print("Enemy 0 killed");
                _health = 0;
                Destroy(gameObject);//destroying the enemy
                Instantiate(explosion, transform.position, transform.rotation);
            }
            else
            {//if the enemy has more health...
                _health = _health - 1;//decrease health by 1
                print("Enemy 0 hit " + _health);
            }
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }
}
