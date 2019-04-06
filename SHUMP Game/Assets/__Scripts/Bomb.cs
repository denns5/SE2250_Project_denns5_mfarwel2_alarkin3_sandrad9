using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public Rigidbody bomb;
    private float timer;
    private int count;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            GameObject[] gameObj;
            gameObj = GameObject.FindGameObjectsWithTag("Enemy");
           
            foreach (GameObject obj in gameObj)
            {
                Destroy(obj);
                count++;
            }
            Destroy(gameObject);
            Hero.bmb = false;
            if (count > 0)
            {
                ScoreManager.UpdateScore(50);
                TextManager.UpdateText();
            }
        }
    }
}
