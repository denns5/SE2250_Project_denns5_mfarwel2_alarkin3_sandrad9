using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    static private TextManager T;
    public float levelStartDelay = 2f;
    public Text gameOverGT, highScoreGT, scoreGT, level, fixedLevel, gunGT;

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
        
        // Set up GameOver UI Text
        GameObject go = GameObject.Find("GameOver");
        if (go != null)
        {
            gameOverGT = go.GetComponent<Text>();
            gameOverGT.gameObject.SetActive(false);
        }

        //Set up ScoreCounter UI Text
        go = GameObject.Find("ScoreCounter");
        scoreGT = go.GetComponent<Text>();

        go = GameObject.Find("WeaponTracker");
        gunGT = go.GetComponent<Text>();

        //Set up Level UI Text
        go = GameObject.Find("Level");
        level = go.GetComponent<Text>();
        UpdateLevel();

        //Set up Level UI Text
        go = GameObject.Find("FixedLevel");
        fixedLevel = go.GetComponent<Text>();
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
        gameOverGT.text = "Game Over!\nYou got to Level: " + ScoreManager.LEVEL;
        scoreGT.gameObject.SetActive(false);
        UpdateHighScore();
        highScoreGT.gameObject.SetActive(true);
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
        fixedLevel.text = "Level: " + ScoreManager.LEVEL;

    }

    public static void UpdateGun(string gun)
    {
        try
        { // try-catch stops an error from breaking your program 
            T.UpdateG(gun);
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("TextManager:UpdateText() called while T=null.\n" + nre);
        }
    }
    public void UpdateG(string g)
    {
        gunGT.text = "Current gun: " + g;
    }

    public void UpdateHighScore()
    {
        // Set up the HighScore UI Text
        GameObject go = GameObject.Find("HighScore");
        if (go != null)
        {
            highScoreGT = go.GetComponent<Text>();
            highScoreGT.gameObject.SetActive(false);
        }
        string hScore = "High Score: " + ScoreManager.HIGH_SCORE;
        go.GetComponent<Text>().text = hScore;
    }

    public static void UpdateLevel()
    {
        try
        { // try-catch stops an error from breaking your program 
            T.UpdateL();
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("TextManager:UpdateLevel() called while T=null.\n" + nre);
        }
    }

    public void UpdateL()
    {
        level.text = "Level: " + ScoreManager.LEVEL;
        level.gameObject.SetActive(true);
        Invoke("HideLevelText", levelStartDelay);
    }

    private void HideLevelText()
    {
        level.gameObject.SetActive(false);
    }
}
