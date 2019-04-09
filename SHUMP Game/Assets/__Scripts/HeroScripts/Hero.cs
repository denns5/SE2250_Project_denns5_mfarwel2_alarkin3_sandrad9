using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{//setting up all initial necaessary variables
    static public Hero S;
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    public AudioClip powerUpSound;
    public AudioClip heroDamageSound;
    public AudioClip shieldPowerUp;
    public GameObject leftWeapon, rightWeapon;
    public float shield = 4;//setting initial shield level
    private AudioSource _source;
    private GameObject _lastTriggerGo = null;
    private float _powerUpTime = 0; //Will track the time the powerup becomes active relative to total runtime
    public int timeIncrement = 0;
    public bool multiActive = false; //Variabe used to signal when multiweapon is active 
    public int pick = 0; //Variabe used for switch statement for pickups
    public GameObject explosionEnemy, explosionHero;

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
        shield = 4;
    }

    private void Start()
    {
        //making the left and right guns not active at the start
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
            fireDelegate();//will make the ship fire
        }

        //
        if (Time.time - _powerUpTime > timeIncrement + 1) {
            timeIncrement++; //Increment the counter
            TextManager.UpdateGun(); //Updates the countdown for when the multi weapon is active that is displayed by text
        }

        //If the multi weapon exceeds 10 seconds or is deactivated, deactivate it
        if (Time.time - _powerUpTime >= 10 && multiActive == true || multiActive == false)
        {//if the hero has had the power up for 10 seconds
            multiActive = false;
            leftWeapon.SetActive(false);//guns become unactive
            rightWeapon.SetActive(false);
            timeIncrement = 0;
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
            _source.PlayOneShot(heroDamageSound, 2f);
            shield--;//decreasing the shield level when the ship is hit by an enemy
            Destroy(go);//destroying the enemy when hit
            if (go.tag == "Enemy"){
                Instantiate(explosionEnemy, go.transform.position, go.transform.rotation);
            }
        
        if (shield > -1)
                TextManager.UpdateText();
            if (shield  < 0)
            {
                Destroy(gameObject);//destroying the hero ship
                Instantiate(explosionHero, transform.position, transform.rotation);
                Main.S.DelayedRestart();//restarting the game
            }
        }

        else if (go.tag == "EnemyBoss")//this enemy isn't destroyed on contact with Hero
        {
            print("touched boss");
            _source.PlayOneShot(heroDamageSound, 2f);
            shield--;//decreasing the shield level when the ship is hit by an enemy
            if (shield < 0)
            {
                //_source.PlayOneShot(gameOverSound, 2f);
                Instantiate(explosionHero, transform.position, transform.rotation);
                Destroy(gameObject);//destroying the hero ship
                Main.S.DelayedRestart();//restarting the game
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
        _source.PlayOneShot(powerUpSound, 1.5f);
        pick = Random.Range(0, 3);
        switch (pick)
        {
            case 0://When pickup is a multiweapon
                Bomb.CHECK = false;//Incase last pickup was a bomb
                leftWeapon.SetActive(true);//making the other guns active
                rightWeapon.SetActive(true);
                multiActive = true; //Check used 
                timeIncrement = 0;
                Weapon.GUN = "Multi";
                TextManager.UpdateGun();//update the weapon text
                _powerUpTime = Time.time;//the current time is when the power up was absorbed
                break;

            case 1://bomb power up case
                Bomb.CHECK = true;//bomb will become active
                multiActive = false;
                break;

            case 2://shield power up case
                shield = 4;//setting shield back to maximum level
                _source.PlayOneShot(shieldPowerUp, 1.5f);
                TextManager.UpdateText();
                break;
        }
        pu.Absorbedby();//power up will be destroyed by this funciton
    }
    
}
