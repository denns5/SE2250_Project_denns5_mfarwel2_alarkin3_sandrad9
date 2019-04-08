using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    private float _randomSpeed;
    private int _points = 10;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {//will randomly set the initial direction of the enemy to left or right

        int r = Random.Range(0, 2);
        if (r == 0)
        {
            _randomSpeed = -(speed + ScoreManager.LEVEL);
        }
        else
        {
            _randomSpeed = (speed + ScoreManager.LEVEL);
        }
    }

 //Called once per frame
    void Update()
    {
        Move();
    }

    override public void Move()
    {
        Vector3 tempPos = pos;//adjusting the position of the enemy whenever Move() is called(every frame).
        tempPos.x += _randomSpeed * Time.deltaTime;//will cause the enemy to move right or left
        tempPos.y -= (speed + ScoreManager.LEVEL) * Time.deltaTime;//will cause the enemy to move down
        pos = tempPos;//the resulting Vector3 will be in a 45 degree angle left or right.
    }


    public override void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        if (otherGO.tag == "ProjectileHero") //If projectile is a hero projectile kill
        {
            ScoreManager.UpdateScore(_points);
            TextManager.UpdateText();
            Destroy(otherGO);
            Destroy(gameObject);
            Main.S.ShipDestoryed(this,1);
            Instantiate(explosion, transform.position, transform.rotation);
            print("Enemy 1 killed");
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }
}
