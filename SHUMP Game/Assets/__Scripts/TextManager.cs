using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    static private TextManager T;
    public Text gameOverGT, highScoreGT, scoreGT;

    void Awake()
    {
        if (T == null)
        {                                        
            T = this; // Set the private singleton 
        }
        else
        {
            Debug.LogError("ERROR: TextManager.Awake(): T is already set!");
        }
    }

    private void Start()
    {
        SetUpUITexts();
    }

    private void SetUpUITexts()
    {
        // Set up the HighScore UI Text
        GameObject go = GameObject.Find("HighScore");
        if (go != null)
        {
            highScoreGT = go.GetComponent<Text>();
        }
        string hScore = "High Score: " + ScoreManager.HIGH_SCORE;
        go.GetComponent<Text>().text = hScore;

        // Set up GameOver UI Text
        go = GameObject.Find("GameOver");
        if (go != null)
        {
            gameOverGT = go.GetComponent<Text>();
            gameOverGT.gameObject.SetActive(false);
        }

        //Set up ScoreCounter UI Text
        go = GameObject.Find("ScoreCounter");               
        scoreGT = go.GetComponent<Text>();
        UpdateT();
    }

    public static void GameOverText()
    {
        try
        { // try-catch stops an error from breaking your program 
            T.GameOver();
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("TextManager:GameOverText() called while T=null.\n" + nre);
        }
    }

    void GameOver()
    {
        gameOverGT.text = "Game Over";
        scoreGT.gameObject.SetActive(false);
        highScoreGT.gameObject.SetActive(false);
        gameOverGT.gameObject.SetActive(true);
    }

    public static void UpdateText()
    {
        try
        { // try-catch stops an error from breaking your program 
            T.UpdateT();
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("TextManager:UpdateText() called while T=null.\n" + nre);
        }
    }

    public void UpdateT()
    {
        scoreGT.text = "Your score: " + ScoreManager.SCORE;
    }
}
