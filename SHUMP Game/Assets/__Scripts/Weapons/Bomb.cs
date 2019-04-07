using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public Rigidbody bomb;//declaring all necessary variables
    public static bool CHECK;
    private float _timer;
    private int count;
    void Start()
    {
        CHECK = false;//initially the CHECK is set to false
    }

    // Update is called once per frame
    void Update()
    {
       _timer += Time.deltaTime;
        if (_timer >= 1f)
        {
            GameObject[] gameObj;
            gameObj = GameObject.FindGameObjectsWithTag("Enemy");//create an array of all gameobjects with the tag "Enemy"
           
            foreach (GameObject obj in gameObj)//for every object in the array destory it and increase the count
            {
                Destroy(obj);
                count++;
            }
            Destroy(gameObject);//destroy the bomb
            if (count > 0)//if at least one enemy is on screen
            {
                ScoreManager.UpdateScore(50);//update the score and the text
                TextManager.UpdateText();
            }
        }
    }
}
