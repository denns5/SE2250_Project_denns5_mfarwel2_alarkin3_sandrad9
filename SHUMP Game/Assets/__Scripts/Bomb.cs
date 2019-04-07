using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public Rigidbody bomb;
    public static bool CHECK;
    private float _timer;
    private int count;
    void Start()
    {
        CHECK = false;
    }

    // Update is called once per frame
    void Update()
    {
       _timer += Time.deltaTime;
        if (_timer >= 1f)
        {
            GameObject[] gameObj;
            gameObj = GameObject.FindGameObjectsWithTag("Enemy");
           
            foreach (GameObject obj in gameObj)
            {
                Destroy(obj);
                count++;
            }
            Destroy(gameObject);
            if (count > 0)
            {
                ScoreManager.UpdateScore(50);
                TextManager.UpdateScoreCounterText();
            }
           // check = false;
        }
    }
}
