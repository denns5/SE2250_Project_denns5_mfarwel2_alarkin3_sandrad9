using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    static private TextManager T;
    public float levelStartDelay = 2f, pickupStartDelay = 1f;
    public Text gameOverGT, highScoreGT, scoreGT, level, fixedLevel, gunGT, shield;

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

    //Set up the ui texts after awake to mitigate null reference errors
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

        //Set up Shield UI Text
        go = GameObject.Find("Shield");
        shield = go.GetComponent<Text>();

        //Set up WeaponTracker UI Text
        go = GameObject.Find("WeaponTracker");
        gunGT = go.GetComponent<Text>();
        UpdateGun();

        //Set up HighScore UI Text
        go = GameObject.Find("HighScore");
        highScoreGT = go.GetComponent<Text>();

        //Set up Level UI Text
        go = GameObject.Find("Level");
        level = go.GetComponent<Text>();
        UpdateLevel();

        //Set up FixedLevel UI Text
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

    void GameOver()//called when the hero dies
    {
        UpdateHighScore();//updating the high score
        gameOverGT.text = "Game Over!\nYou got to Level: " + ScoreManager.LEVEL+"\nHighScore: "+ScoreManager.HIGH_SCORE;//displaying what level the player reached
        highScoreGT.gameObject.SetActive(true);//show the highscore 
        gameOverGT.gameObject.SetActive(true);//show gameover text
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

    public void UpdateT() //Called consistently to handle updates to each value the text is tracking
    {
        scoreGT.text = "Your score: " + ScoreManager.SCORE;//shows players current score
        fixedLevel.text = "Level: " + ScoreManager.LEVEL;//shows players current level
        shield.text = "Shield: " + Hero.S.shield.ToString();//shows players current shield level
    }

    public static void UpdateGun()
    {
        try
        { // try-catch stops an error from breaking your program 
            T.UpdateG();
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("TextManager:UpdateText() called while T=null.\n" + nre);
        }
    }

    public void UpdateG()
    {
        gunGT.text = "Current gun: " + Weapon.GUN;
        if (Hero.S.multiActive == true)
        { 
        int i = 10;
            i = i - Hero.S.timeIncrement;
            gunGT.text = "Current gun: " + Weapon.GUN + ", " + i.ToString() + " seconds left";//show current weapon type and time left
        }
    }


    public void UpdateHighScore()
    {
        // Set up the HighScore UI Text
        GameObject go = GameObject.Find("HighScore");
        if (go != null)
        {
            highScoreGT = go.GetComponent<Text>();  
        }
        highScoreGT.gameObject.SetActive(true);//setting this text toe active
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
        level.text = "Level: " + ScoreManager.LEVEL;//level displayed is the current level
        if (ScoreManager.LEVEL == 2) //Informs user of new weapon unlock
        {
            level.text += "\nRockets Unlocked";
        }
        level.gameObject.SetActive(true);
        Invoke("HideLevelText", levelStartDelay);//will only be displayed for a short period of time
    }

    //Used to ensure level text is not active for the whole game
    private void HideLevelText()
    {
        level.gameObject.SetActive(false);//taking away the text after it has displayed for 2 seconds
    }

}
