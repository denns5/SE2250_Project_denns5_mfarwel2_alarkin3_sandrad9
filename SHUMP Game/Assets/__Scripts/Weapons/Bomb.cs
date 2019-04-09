using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour //Class will be attached to the bomb prefab
{
    public Rigidbody bomb;//declaring all necessary variables
    public static bool CHECK;
    public GameObject explosionBomb, explosionEnemy;
    private float _timer;
    private int _count;
    void Start()
    {
        CHECK = false;//Right when a bomb is created the check is set to false to ensure only one bomb is created
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
            Destroy(gameObject);//destroy the bomb
            Instantiate(explosionBomb, transform.position, transform.rotation);
            if (_count > 0)//if at least one enemy is on screen
            {
                ScoreManager.UpdateScore(50);//update the score and the text
                TextManager.UpdateText();
            }
        }
    }

    //If bomb collides with gameobject before timer passes one it will still blow up all the other enemies.
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Hero")
            _timer = 2;
    }
}
