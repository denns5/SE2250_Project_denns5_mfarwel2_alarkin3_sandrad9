using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum WeaponType
{//declaring all of the weapon types
    none,
    blaster,
    simple,
    rocket,
    multi,
    bomb
}

[System.Serializable]
public class WeaponDefinition
{//creating a serializable class for the weapons 
    public WeaponType type = WeaponType.none;
    public string letter;
    public Color color = Color.white;
    public GameObject projectilePrefab;
    public Color projectileColor = Color.white;
    public float delayBetweenShots = 0;
    public float velocity = 20;
}

public class Weapon : MonoBehaviour
{
    static public Transform PROJECTILE_ANCHOR;

    [Header("Set Dynamically")]
    [SerializeField]
    private WeaponType _type = WeaponType.none;//declaring all necessary public and private variables
    public WeaponDefinition def;
    public GameObject collar;
    public float lastShotTime;
    private Renderer _collarRend;

    // Start is called before the first frame update
    void Start()
    {
        collar = transform.Find("Collar").gameObject;//getting the collor gameobject which is used to fire
        _collarRend = collar.GetComponent<Renderer>();

        SetType(_type);//setting the type of weapon

        if (PROJECTILE_ANCHOR == null)
        {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }

        GameObject rootGO = transform.root.gameObject;
        if (rootGO.GetComponent<Hero>() != null)
        {
            rootGO.GetComponent<Hero>().fireDelegate += Fire;//using the fire delegate for game operations
        }
        TextManager.UpdateGun("Simple");//setting the initial gun to Simple

    }

    public WeaponType type//Weapontype setter and getter as type is declared is private in the class
    {
        get { return (_type); }
        set
        { SetType(value); }
    }

    public void SetType(WeaponType weaponType)//called by the Weapon type setter to set the weapon
    {
        _type = weaponType;
        if (_type == WeaponType.none)
        {
            this.gameObject.SetActive(false);//if no weapon type is set, then there is currently no weapon so set false
            return;
        }
        else
            this.gameObject.SetActive(true);//if the weapon type is not none, then the weaon gameobject is active

        def = Main.GetWeaponDefinition(_type);//getting the correct weapon definition from Main
        _collarRend.material.color = def.color;
        lastShotTime = 0;
    }

    public void Fire()//called when the player wants to shoot
    {
        if (!gameObject.activeInHierarchy)//if the weapon is not attached to ship yet then dont shoot
        {
            return;
        }
        if (Time.time - lastShotTime < def.delayBetweenShots)//if the player tries to shoot again too fast, dont let them
        {
            return;
        }
        Projectile p = MakeProjectile();//creating a projectil eobject
        Vector3 vel = Vector3.up * def.velocity;//setting the velocity to be upward
        if (transform.up.y < 0)//making sure the projectile is always upward
        {
            vel.y = -vel.y;
        }

        switch (type)//switch statement which depends on the weapon type
        {
            case WeaponType.simple://just one projectile is created for simple
                p.rigid.velocity = vel;
                break;
           
           case WeaponType.rocket://just one projectile is created for rocket
                p.rigid.velocity = vel;
                break;

            case WeaponType.blaster://three projectiles will be created for the blaster
                // middle projectile
                p.rigid.velocity = vel;
                // right projectile
                p = MakeProjectile();
                p.transform.rotation = Quaternion.AngleAxis(30, Vector3.back);//making the projectile aim 30 degrees rotated
                p.rigid.velocity = p.transform.rotation * vel;
                // left projectile
                p = MakeProjectile();
                p.transform.rotation = Quaternion.AngleAxis(-30, Vector3.back);//making the projectile aim 30 degrees in the other direction
                p.rigid.velocity = p.transform.rotation * vel;
                break;

            case WeaponType.multi:
                break;

            case WeaponType.bomb:
                p.rigid.velocity = vel;//one bomb is created when shot
                break;

            case WeaponType.none:
                break;
        }

        // delete projectile after each switch statement executes 
        // so the new weapon starts fresh with correct number of projectiles
        Destroy(p,0);
    }

    public Projectile MakeProjectile()//called within Fire()
    {
        GameObject go = Instantiate<GameObject>(def.projectilePrefab);//instantiting the projectile prefab
        if (transform.parent.gameObject.tag == "Hero")//if the hero shoots the projectile
        {
            go.tag = "ProjectileHero";//setting the correct tag
            go.layer = LayerMask.NameToLayer("ProjectileHero");//setting the correct layer
        }
      
        else//the projectile was shot by an enemy
        {
            go.tag = "Projectile Enemy";//setting the correct tag to enemy
            go.layer = LayerMask.NameToLayer("Projectile Enemy");//setting the correct layer to enemy
        }
        go.transform.position = collar.transform.position;//starts at the collar
        go.transform.SetParent(PROJECTILE_ANCHOR, true);
        Projectile p = go.GetComponent<Projectile>();
        p.type = _type;//setting the projectile type
        lastShotTime = Time.time;//to be used to make sure there is a delay between shots
        return p;
    }

    private float _timer;//setting extra varibales to be used in update
    public static int gun;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Hero.CHECK == false && gun == 1 && Bomb.CHECK == false && type != WeaponType.bomb)
        {//if the player selects 'z' and there is no power up and no bomb then they have selected blaster
            SetType(WeaponType.simple);
            gun = 0;
            TextManager.UpdateGun("Simple");//update the weapon type text
        }

        if (Hero.CHECK == true && gun == 0)//if the hero has the blaster power up set type to blaster
        {
            SetType(WeaponType.blaster);
            TextManager.UpdateGun("Multi");//update the weapn type text
            gun = 1;
        }

        if (Input.GetKeyDown(KeyCode.X) && Hero.CHECK != true && ScoreManager.LEVEL >= 2)
        {//once the player has reached level 2 they can access the rocket by selecting 'X'
            SetType(WeaponType.rocket);
            TextManager.UpdateGun("Rocket");//update weapon text
        }


        if (Bomb.CHECK == true && type != WeaponType.bomb)//if the hero has the bomb power up and are not already set to bomb, set type to bomb
        {
            SetType(WeaponType.bomb);
            TextManager.UpdateGun("Bomb");//update weapon text
            Hero.CHECK = false;//after they shoot they no longer have the bomb
        }
        if (Bomb.CHECK == false && type == WeaponType.bomb)//if they no longer have the bomb and are still set on bomb, change to simple weapon type
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.2f)//will change 0.2 seconds after the bomb was shot
            {
                SetType(WeaponType.simple);
                TextManager.UpdateGun("Simple");//updating the weapon text
                gun = 0;
                _timer = 0;//settingh the timer back to zero
            }

        }

    }

}
