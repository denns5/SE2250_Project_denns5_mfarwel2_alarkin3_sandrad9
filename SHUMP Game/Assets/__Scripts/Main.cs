using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S;
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;
    public GameObject[] prefabEnemies;//creating a array to store the four enemies
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;
    public WeaponDefinition[] weaponDefinitions;
    public GameObject prefabPowerUp;
    public WeaponType[] powerUpFrequency = new WeaponType[] { WeaponType.bomb, WeaponType.multi, WeaponType.shield };//Make an array of possible power ups
    public AudioClip killSound;
  //  public AudioClip music;
    private BoundsCheck _bndCheck;
    private AudioSource _source;
    private int _bossSpawn = 2;
    private float _powerupDelay = 0;

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
       // _source.PlayOneShot(music, 0.6f);//playing the background music
    }
    public void SpawnEnemy()
    {
      
        int ndx = Random.Range(0, prefabEnemies.Length-1);
        if (ScoreManager.LEVEL == _bossSpawn)
        {
            ndx = 3;
            _bossSpawn += 1;
        }
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
        ScoreManager.GameOverScore();//displaying proper gameover text
        TextManager.GameOverText();
        Invoke("Restart", delay);//invoke the restart method after delay seconds
    }
    public void Restart()
    {
        SceneManager.LoadScene("SHUMP Game");//reloading the scene
    }

    static public WeaponDefinition GetWeaponDefinition(WeaponType weaponType)
    {
        if (WEAP_DICT.ContainsKey(weaponType))
        {
            return (WEAP_DICT[weaponType]);//if there contains a key of the weapon type, return it
        }

        return (new WeaponDefinition());//else return a new definition
    }

    public void ShipDestoryed(Enemy e, int type)
    {
        //print("Ship Destroyed!" + type);
        _source.PlayOneShot(killSound, 0.5f);
        if (Random.value <= e.powerUpDropChance)
        {//havent created this variable in Enemy yet
            // if powerup was just created, don't create a new one from same destroyed ship
            if (Time.time - _powerupDelay < 0.1f) return;
            else
            {
                int index = Random.Range(0, powerUpFrequency.Length);//get a random index in the array to access a random power up
                WeaponType puType = powerUpFrequency[index];
                //spawn a power up
                GameObject go = Instantiate(prefabPowerUp) as GameObject;
                PowerUp pu = go.GetComponent<PowerUp>();

                //set the power up to the position of the destroyed ship
                go.transform.position = e.transform.position;

                _powerupDelay = Time.time;//new delay is the current time
            }
        }
    }
}
