using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public Rigidbody bomb;//declaring all necessary variables
    public static bool CHECK;
    public AudioClip bombSound;
    private AudioSource _src;
    private float _timer;
    private int _count;
    void Start()
    {
        CHECK = false;//initially the CHECK is set to false
        _src = GetComponent<AudioSource>();
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
                _count++;
            }
            _src.PlayOneShot(bombSound, 0.9f);
            Destroy(gameObject);//destroy the bomb
            if (_count > 0)//if at least one enemy is on screen
            {
                ScoreManager.UpdateScore(50);//update the score and the text
                TextManager.UpdateText();
            }
        }
    }
}
