using UnityEngine;
using System.Collections;

public class Boss_Weapon : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
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
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
