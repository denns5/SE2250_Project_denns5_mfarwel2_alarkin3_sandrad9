using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S;
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;
    public GameObject[] prefabEnemies;//creating a array to store the three enemies
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;
    public WeaponDefinition[] weaponDefinitions;
    public GameObject prefabPowerUp;
    public WeaponType[] powerUpFrequency = new WeaponType[] { WeaponType.simple, WeaponType.simple, WeaponType.blaster};//simple will be twice as likley as blaster
    public AudioClip killSound;

    private BoundsCheck _bndCheck;
    private AudioSource _source;

    void Awake()
    {
        S = this;
        _bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);//calling the spawn enemy function once every 2 seconds

        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach (WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }
        _source = GetComponent<AudioSource>();
    }

    public void SpawnEnemy()
    {
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate(prefabEnemies[ndx]);//will generate a random number between 0 and 2 to access a raondom enemy in the array using the random index number.
        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        Vector3 pos = Vector3.zero;
        float xMin = -_bndCheck.camWidth + enemyPadding;//creating a max and min value for the possible starting points of the ships
        float xMax = _bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = _bndCheck.camHeight + enemyPadding;//setting the y component of the enemies to be at the top of the boundry
        go.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);//calling the invoke again
    }

    public void DelayedRestart(float delay)
    {
        ScoreManager.GameOverScore();
        TextManager.GameOverText();
        Invoke("Restart", delay);
    }
    public void Restart()
    {
        SceneManager.LoadScene("SHUMP Game");//reloading the scene
    }

    static public WeaponDefinition GetWeaponDefinition(WeaponType weaponType)
    {
        if (WEAP_DICT.ContainsKey(weaponType))
        {
            return (WEAP_DICT[weaponType]);
        }

        return (new WeaponDefinition());
    }

    public void ShipDestoryed(Enemy e)
    {
        _source.PlayOneShot(killSound, 0.5f);
        if (Random.value <= e.powerUpDropChance)
        {//havent created this variable in Enemy yet
            int index = Random.Range(0, powerUpFrequency.Length);
            WeaponType puType = powerUpFrequency[index];
            //spawn a power up
            GameObject go = Instantiate(prefabPowerUp) as GameObject;
            PowerUp pu = go.GetComponent<PowerUp>();
            pu.SetType(puType);
            //set the power up to the position of the destroyed ship
            go.transform.position = e.transform.position;
        }
    }

}
