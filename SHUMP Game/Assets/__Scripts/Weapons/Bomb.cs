using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour //Class will be attached to the bomb prefab
{
    public Rigidbody bomb;//declaring all necessary variables
    public static bool CHECK;
    public AudioClip bombHit;
    private AudioSource _src;
    public GameObject explosionBomb, explosionEnemy;
    private float _timer;
    private int _count;
    void Start()
    {
        CHECK = false;//Right when a bomb is created the check is set to false to ensure only one bomb is created
        _src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       _timer += Time.deltaTime;
        if (_timer >= 1f) //Once a second is passed the bomb will explode
        {
            GameObject[] gameObj;
            gameObj = GameObject.FindGameObjectsWithTag("Enemy");//create an array of all gameobjects with the tag "Enemy", Boss will not be killed by bomb
           
            foreach (GameObject obj in gameObj)//for every object in the array destory it and increase the count
            {
                Instantiate(explosionEnemy, obj.transform.position, obj.transform.rotation); //Add explosion effect
                Destroy(obj);
                _count++;
            }
            _src.PlayOneShot(bombHit, 1f); 
            Destroy(gameObject);//destroy the bomb
            Instantiate(explosionBomb, transform.position, transform.rotation);
            if (_count > 0)//if at least one enemy is on screen
            {
                ScoreManager.UpdateScore(50);//update the score and the text
                TextManager.UpdateText();
            }
        }
    }

    private void OnCollisionEnter(Collision coll) //Ensures that the bomb will go off if it collides with enemy before timer hits 1 second
    {
        if (coll.gameObject.tag != "Hero")
        {
            _timer = 2;
        }

    }
}
