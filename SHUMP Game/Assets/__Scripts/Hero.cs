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
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    private float _shieldLevel = 4;//setting initial shield level

    private GameObject _lastTriggerGo = null;

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
        fireDelegate += TempFire;

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
        }
    }

    void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();

        Projectile proj = projGO.GetComponent<Projectile>();
        proj.type = WeaponType.simple;
        float tSpeed = Main.GetWeaponDefinition(proj.type).velocity;
        rigidB.velocity = Vector3.up * tSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        print("Triggered: "+other.gameObject.name);

        if (go == _lastTriggerGo)
        {
            return;
        }
        _lastTriggerGo = go;
        if (go.tag == "Enemy")
        {
            _shieldLevel--;//decreasing the shield level when the ship is hit by an enemy
            Destroy(go);//destroying the enemy when hit
            if (_shieldLevel < 0)
            {
                Destroy(gameObject);//destroying the hero ship
                print("Shield Destroyed");
                Main.S.DelayedRestart(gameRestartDelay);//restarting the game
            }
        }
        else
        {
            print("Triggered by non Enemy" + go.name);
        }
    }
    
    public float shieldLevel
    {
        get
        {
            return _shieldLevel;
        }
        set
        {
        //do not need to use a setter at this poit
        }
    }
    
}