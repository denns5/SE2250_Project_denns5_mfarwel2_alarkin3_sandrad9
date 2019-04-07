using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class Enemy_Boss : Enemy
{
    public AudioClip killSound;

    private int _health = 10;
    private int _points = 100;
    private float _delayBetweenHits = 0;
    private AudioSource _src;

    // Start is called before the first frame update

    void Awake()
    {
        _src = GetComponent<AudioSource>();
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

    int getPoints()
    {
        return _points;
    }

    public override void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        if (otherGO.tag == "ProjectileHero")//if hit by a hero projectile...
        {
            print("Collision with boss");
            _src.PlayOneShot(killSound, 1f);//play kill sound
            Destroy(otherGO);//destroy the projetile
            if (Time.time - _delayBetweenHits < 0.1f) return;
            else if (_health <= 1)
            {
                ScoreManager.UpdateScore(_points);//update score and text
                TextManager.UpdateText();
                Main.S.ShipDestoryed(this, 0);//telling main that ship is destroyed
                print("Enemy boss killed");
                Destroy(gameObject);//destroy the boss
            }
            else
            {
                _health = _health - 1;//health is decreased by 1
                print("Enemy boss hit " + _health);
                _delayBetweenHits = Time.time;//resetting delay between hits
            }
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }
}
