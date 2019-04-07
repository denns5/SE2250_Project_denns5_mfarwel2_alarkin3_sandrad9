﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S;
    //delcaring public variables that will determine how the ship will move.
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 4f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    public Weapon[] weapons;
    public AudioClip shootSound;
    public GameObject leftWeapon, rightWeapon;
    private float _shieldLevel = 4;//setting initial shield level
    private AudioSource _source;
    private GameObject _lastTriggerGo = null;
    private float _powerUpTime = 0;
<<<<<<< HEAD
    public static bool CHECK = false;
=======
    private bool _check = false;
>>>>>>> parent of 072a621... Pickups

    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    void Awake()
    {
        if (S == null)
        {
            S = this;//setting the singleton to this
        }
     

        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second hero");
        }
        _source = GetComponent<AudioSource>();

    }

    private void Start()
    {
        leftWeapon.SetActive(false);
        rightWeapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {//getting the inputs from the arrow keys of the keyboard
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        //adjusting the position of the ship according to these inputs
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        // Allow the ship to fire bullet
        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
        {
            fireDelegate();
            _source.PlayOneShot(shootSound,0.3f);
        }

        if (Time.time - _powerUpTime >= 10 && _check == true)
        {
<<<<<<< HEAD
            print("Hero check set false after time");
            CHECK = false;
=======
            _check = false;
>>>>>>> parent of 072a621... Pickups
            leftWeapon.SetActive(false);
            rightWeapon.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("On trigger enter called");
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        print("Triggered: "+ other.gameObject.name);

        if (go == _lastTriggerGo)
        {
            return;
        }
        _lastTriggerGo = go;
        if (go.tag == "Enemy" || go.tag == "ProjectileEnemy")

        {
            _shieldLevel--;//decreasing the shield level when the ship is hit by an enemy
            Destroy(go);//destroying the enemy when hit
            if (_shieldLevel < 0)
            {
                Destroy(gameObject);//destroying the hero ship
                Main.S.DelayedRestart(gameRestartDelay);//restarting the game
            }
        }

        else if (go.tag == "PowerUp")
        {
            AbsorbPowerUp(go);
        }
        else
        {
            print("Triggered by non Enemy" + go.name);
        }
    }

    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();
        Debug.Log("Here" + pu.type);
        leftWeapon.SetActive(true);
        rightWeapon.SetActive(true);
        _check = true;
        _powerUpTime = Time.time;
        switch (pu.type)
        {
            case WeaponType.speed:
                speed += 10;
                break;

            case WeaponType.simple:

<<<<<<< HEAD
      
        //int ndx = Random.Range(0, 2);
        //Debug.Log(ndx + " Absorbed value ");
        string type = pu.GetStringType();
        switch (type)
         {
             case "Multi":
                 Bomb.CHECK = false;
                 //Rocket.CHECK = false;
                 leftWeapon.SetActive(true);
                 rightWeapon.SetActive(true);
                print("Hero check set true");
                 CHECK = true;
                 _powerUpTime = Time.time;
                 break;

            /* case "Rocket":
                 Rocket.CHECK = true;
                 Bomb.CHECK = false;
                 _check = false;
                 break;*/

             case "Bomb":
                
                 Bomb.CHECK = true;
                //Rocket.CHECK = false;
                print("hero check set false. case 1");
                CHECK = false;
                 break;

         }
=======

                break;
                //fill in later when we create the different power up types
        }
>>>>>>> parent of 072a621... Pickups
        pu.Absorbedby(gameObject);
    }
    
    public float shieldLevel
    {
        get
        {
            return _shieldLevel;
        }
        set
        {
        //do not need to use a setter at this point
        }
    }
    
}
