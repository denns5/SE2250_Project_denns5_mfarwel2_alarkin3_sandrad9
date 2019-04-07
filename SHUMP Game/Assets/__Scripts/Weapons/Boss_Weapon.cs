using UnityEngine;
using System.Collections;

public class Boss_Weapon : MonoBehaviour
{
    [SerializeField]//creating a serialize field
    GameObject bullet;
    float fireRate;
    float nextFire;

    void Start()
    {
        fireRate = 2f;//will fire every 2 seconds
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        FireCheck();
    }

    void FireCheck()
    {
        if (Time.time > nextFire)//will only fire every 2 seconds
        {
            Instantiate(bullet, transform.position, Quaternion.identity);//instantiate the bullet
            nextFire = Time.time + fireRate;//setting the time of next fire to 2 seconds later
        }
    }
}
