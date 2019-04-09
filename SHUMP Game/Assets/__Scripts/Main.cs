using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    static public Main S;
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;
    public GameObject[] prefabEnemies;//creating a array to store the four enemies
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;
    public WeaponDefinition[] weaponDefinitions;
    public GameObject prefabPowerUp;
    public AudioClip killSound;
    public AudioClip gameOverSound;
    public Button restartButton;//buttons to control end of game functions
    public Button menuButton;

    private BoundsCheck _bndCheck;
    private AudioSource _source;//used to play audio clips
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
        _source = GetComponent<AudioSource>();//getting the AudioSource component to play audio clips
        //setting these buttons to false as they shouly only be active at the end of the game
        restartButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
    }
    public void SpawnEnemy()
    {
      
        int ndx = Random.Range(0, prefabEnemies.Length-1);//getting a random index number based on the length of the array of enemies
        if (ScoreManager.LEVEL == _bossSpawn)
        {//at the start of every level(with the exception of the first), a boss will spawn
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

    public void DelayedRestart()
    {
        _source.PlayOneShot(gameOverSound, 1.5f);//playing the gameover sound
        ScoreManager.GameOverScore();//displaying proper gameover text
        TextManager.GameOverText();
        restartButton.gameObject.SetActive(true);//setting these buttons to true at the end of the game
        menuButton.gameObject.SetActive(true);
    }

    static public WeaponDefinition GetWeaponDefinition(WeaponType weaponType)
    {
        if (WEAP_DICT.ContainsKey(weaponType))
        {
            return (WEAP_DICT[weaponType]);//if there contains a key of the weapon type, return it
        }

        return (new WeaponDefinition());//else return a new definition
    }

    public void ShipDestroyed(Enemy e)
    {
        _source.PlayOneShot(killSound, 1f);
        if (Random.value <= e.powerUpDropChance)
        {
            // if powerup was just created, don't create a new one from same destroyed ship
            if (Time.time - _powerupDelay < 0.1f) return;
            else
            {
                //spawn a power up
                GameObject go = Instantiate(prefabPowerUp) as GameObject;
                //set the power up to the position of the destroyed ship
                go.transform.position = e.transform.position;
                _powerupDelay = Time.time;//new delay is the current time
            }
        }
    }
}
