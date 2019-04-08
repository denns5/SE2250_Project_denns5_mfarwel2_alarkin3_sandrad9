using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public float rotationsPerSecond = 0.1f;

    public int levelShown = 0;

    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame to update the shield level
    void Update()
    {
        int currLevel = Mathf.FloorToInt(Hero.S.shield);
        if (levelShown != currLevel)
        {
            levelShown = currLevel;//making the shield have tghe correct look in accordance to its current level
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        }
        float shieldRotate = -(rotationsPerSecond * Time.time * 360) % 360;//making the shield rotate around the ship
        transform.rotation = Quaternion.Euler(0, 0, shieldRotate);
    }
}
