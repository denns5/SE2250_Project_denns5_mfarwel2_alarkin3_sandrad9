using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy//deriving this class from the Enemy SuperClass
{

    public GameObject explosion;
    private float _randomSpeed;
    private float _directionChange = 50f;

    private int _health = 3;
    private int _points = 30;
    private GameObject _lastTriggeredGo = null;


    // Start is called before the first frame update
    void Start()
    {
        //this will randomly set the initial direction of the enemy to left or right
        int r = Random.Range(0, 2);
        if (r == 0)
        {
            _randomSpeed = -(speed) ;
        }
        else
        {
            _randomSpeed = (speed);
        }

    }

  /*  int getPoints()
    {
        return _points;
    }*/

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
        tempPos.y -= (speed + ScoreManager.LEVEL) * Time.deltaTime;//subtracting will cause the enemy to move down the screen.
        pos = tempPos;
    }

    public override void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        if (otherGO.tag == "ProjectileHero")
        {
            if (otherGO == _lastTriggeredGo) //Avoids same projectile triggering multiple times
            {
                return;
            }
            _lastTriggeredGo = otherGO;
            Destroy(otherGO);
            if (_health == 1)
            {
                ScoreManager.UpdateScore(_points);//update the score
                TextManager.UpdateText();//update the text
                _health = 0;
                Main.S.ShipDestoryed(this,2);//letting main know ship was destroyed to possibly spawn a power up
                print("Enemy 2 killed");
                Destroy(gameObject);//destroy the ship
                Instantiate(explosion, transform.position, transform.rotation);
            }
            else
            {//if the enemy have more lives
                _health = _health - 1;//decreasing the health by 1
                print("Enemy 2 hit " + _health);
            }

        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }
}
