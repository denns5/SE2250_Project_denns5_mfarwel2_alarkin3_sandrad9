using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    private float _randomSpeed;
    private int _points = 100;

    // Start is called before the first frame update
    void Start()
    {//will randomly set the initial direction of the enemy to left or right

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
        Move();
    }

    override public void Move()
    {
        Vector3 tempPos = pos;//adjusting the position of the enemy whenever Move() is called(every frame).
        tempPos.x += _randomSpeed * Time.deltaTime;//will cause the enemy to move right or left
        tempPos.y -= speed * Time.deltaTime;//will cause the enemy to move down
        pos = tempPos;//the resulting Vector3 will be in a 45 degree angle left or right.
    }
    
    public override void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        if (otherGO.tag == "ProjectileHero")
        {
            Destroy(otherGO);
            ScoreManager.UpdateScore(_points);
            TextManager.UpdateText();
            Destroy(gameObject);

        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }
}
