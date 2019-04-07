using UnityEngine;
using System.Collections;

public class Boss_WeaponMid : MonoBehaviour
{
    [SerializeField]
    GameObject bullet1, bullet2, bullet3;
    float fireRate;
    float nextFire;

    void Start()
    {
        fireRate = 2f;
        nextFire = Time.time;
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
            Instantiate(bullet1, transform.position, Quaternion.identity);
            Instantiate(bullet2, transform.position, Quaternion.identity);
            Instantiate(bullet3, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
