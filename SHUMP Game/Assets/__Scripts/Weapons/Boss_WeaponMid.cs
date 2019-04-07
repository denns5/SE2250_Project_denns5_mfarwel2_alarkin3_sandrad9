using UnityEngine;
using System.Collections;

public class Boss_WeaponMid : MonoBehaviour
{
    [SerializeField]//creating a serialize field
    GameObject bullet1, bullet2, bullet3;
    float fireRate;
    float nextFire;

    void Start()
    {
        fireRate = 2f;//fire rate is every 2 seconds
        nextFire = Time.time;//next fire is right now
    }

    // Update is called once per frame
    void Update()
    {
        FireCheck();
    }

    void FireCheck()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bullet1, transform.position, Quaternion.identity);//instantiate 3 bullets. each will come from a differen collar
            Instantiate(bullet2, transform.position, Quaternion.identity);
            Instantiate(bullet3, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;//setting the next fire to 2 seconds from now
        }
    }
}
