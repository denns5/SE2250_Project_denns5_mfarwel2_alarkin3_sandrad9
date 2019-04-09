using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class Enemy_Boss : Enemy
{
    public GameObject explosion;
    private int _health = 10; 
    private int _points = 100;

    // Start is called before the first frame update

    void Awake()
    {
    }

    void Update()
    {
        Move();
    }

    public override void Move()
    {//adjusting the position of the enemy whenever Move() is called(every frame).
        Vector3 tempPos = transform.position;
        tempPos.y -= (1f + ScoreManager.LEVEL) * Time.deltaTime;//will cause the enemy to move down
        pos = tempPos;
    }

    public override void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        if (otherGO.tag == "ProjectileHero")//if hit by a hero projectile...
        {   
            Destroy(otherGO);//destroy the projetile
            if (_health <= 1)
            {
                ScoreManager.UpdateScore(_points);//update score and text
                TextManager.UpdateText();
                Main.S.ShipDestroyed(this);//telling main that ship is destroyed
                print("Enemy boss killed");
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);//destroy the boss
            }
            else
            {
                _health = _health - 1;//health is decreased by 1
                print("Enemy boss hit " + _health);
            }
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }
}
