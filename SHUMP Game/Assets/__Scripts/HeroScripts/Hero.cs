using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{//setting up all initial necaessary variables
    static public Hero S;
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 4f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    public Weapon[] weapons;
    public AudioClip shootSound;
    public AudioClip powerUpSound;
    public AudioClip heroDamageSound;
    public GameObject leftWeapon, rightWeapon;
    private float _shieldLevel = 4;//setting initial shield level
    private AudioSource _source;
    private GameObject _lastTriggerGo = null;
    private float _powerUpTime = 0;
    public static int TIME = 0;
    public static bool CHECK = false;
    public static int PICK = 0;

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
        _source = GetComponent<AudioSource>();//assigning the audio source component

    }

    private void Start()
    {
        leftWeapon.SetActive(false);//making the left and right guns not active at the start
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
            fireDelegate();//will make the ship fire
            _source.PlayOneShot(shootSound,0.3f);//play the shooting sound
        }

        if (Time.time - _powerUpTime > TIME + 1) {
            TIME++;
            TextManager.UpdateGun();
        }

        if (Time.time - _powerUpTime >= 10 && CHECK == true || CHECK == false)
        {//if the hero has had the power up for 10 seconds
            CHECK = false;
            leftWeapon.SetActive(false);//guns become unactive
            rightWeapon.SetActive(false);
            TIME = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        print("Triggered: "+other.gameObject.name);

        if (go == _lastTriggerGo && go.tag!="EnemyBoss")//if the same object hits it again and its not the enemy boss do nothing
        {
            return;
        }
        _lastTriggerGo = go;//last trigger set to the current game object
        if (go.tag == "Enemy" || go.tag == "ProjectileEnemy")//if hero is hit by either a ship or projectile...
        {
            _source.PlayOneShot(heroDamageSound, 1f);
            _shieldLevel--;//decreasing the shield level when the ship is hit by an enemy
            Destroy(go);//destroying the enemy when hit
            if (_shieldLevel < 0)
            {
                Destroy(gameObject);//destroying the hero ship
                Main.S.DelayedRestart(gameRestartDelay);//restarting the game
            }
        }
        else if (go.tag == "EnemyBoss")//this enemy isn't destroyed on contact with Hero
        {
            print("touched boss");
            _source.PlayOneShot(heroDamageSound, 1f);
            _shieldLevel--;//decreasing the shield level when the ship is hit by an enemy
            if (_shieldLevel < 0)
            {
                Destroy(gameObject);//destroying the hero ship
                Main.S.DelayedRestart(gameRestartDelay);//restarting the game
            }
        }

        else if (go.tag == "PowerUp")
        {//the hero has touched a power up
            AbsorbPowerUp(go);//call this function to decide what the power up does
        }
        else
        {//something else hit the enemy
            print("Triggered by non Enemy" + go.name);
        }
    }

    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();//getting the power up component
        _source.PlayOneShot(powerUpSound, 0.9f);
        PICK = Random.Range(0, 3);
        switch (PICK)
        {
            case 0:// the last pick was a bomb or this is the first power up
                Bomb.CHECK = false;//there is no bomb
                leftWeapon.SetActive(true);//making the other guns active
                rightWeapon.SetActive(true);
                CHECK = true;
                Weapon.GUN = "Multi";
                TextManager.UpdateGun();//update the weapon text
                _powerUpTime = Time.time;//the current time is when the power up was absorbed
                //PICK = 1;//next power up will be bomb
                break;


            case 1://bomb power up case
                Bomb.CHECK = true;//bomb will become active
                //PICK = 0;//next power up will be multi
                CHECK = false;
                break;

            case 2://shield power up case
                _shieldLevel = 4;//setting shield back to maximum level
                break;
        }
        pu.Absorbedby(gameObject);//power up will be destroyed by this funciton
    }


    public float shieldLevel//getter for _shield
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
