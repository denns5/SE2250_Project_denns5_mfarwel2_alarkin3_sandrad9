using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy//deriving this class from the Enemy SuperClass
{
    //public float powerUpDropChance = 1f;

    private float _randomSpeed;
    private float _directionChange = 50f;


    private int _health = 3;
    private int _points = 300;
    private float _delayBetweenHits = 0;


    // Start is called before the first frame update
    void Start()
    {
        //this will randomly set the initial direction of the enemy to left or right
        int r = Random.Range(0, 2);
        if (r == 0)
        {
            _randomSpeed = -speed;
        }
        else
        {
            _randomSpeed = speed;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Every frame, the directionChange variable is decreased by one so after 50 frames, the direction of the ship will change.
        if (_directionChange == 0)
        {
            _randomSpeed *= -1;
            _directionChange = 50f;
        }
        _directionChange--;
        Move();
    }
    override public void Move()//overiding the super class Move() method.
    {
        //adjusting the position of the enemy whenever Move() is called(every frame).
        Vector3 tempPos = pos;
        tempPos.x += 1.5f * _randomSpeed * Time.deltaTime;
        tempPos.y -= speed * Time.deltaTime;//subtracting will cause the enemy to move down the screen.
        pos = tempPos;
    }

    public override void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        if (otherGO.tag == "ProjectileHero")
        {
            Destroy(otherGO);
            if (Time.time - _delayBetweenHits < 0.1) return;
            else if (_health == 1)
            {
                ScoreManager.UpdateScore(_points);
                TextManager.UpdateText();
                Destroy(gameObject);
                Main.S.ShipDestoryed(this);
                print("Enemy 2 killed");
            }
            else
            {
                _health = _health - 1;
                print("Enemy 2 hit " + _health);
                _delayBetweenHits = Time.time;
            }
            // Destroy(gameObject);

        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }
}
