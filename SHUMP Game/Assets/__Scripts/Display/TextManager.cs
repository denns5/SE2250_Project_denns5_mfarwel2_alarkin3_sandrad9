using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    static private TextManager T;
    public float levelStartDelay = 2f, pickupStartDelay = 1f;
    public Text gameOverGT, highScoreGT, scoreGT, level, fixedLevel, gunGT, shield, pickup;

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

        go = GameObject.Find("Shield");
        shield = go.GetComponent<Text>();

        go = GameObject.Find("WeaponTracker");
        gunGT = go.GetComponent<Text>();
        UpdateGun();

        go = GameObject.Find("PickupDisplay");
        level = go.GetComponent<Text>();
       // UpdatePickup();

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

    void GameOver()//called when the hero dies
    {
        gameOverGT.text = "Game Over!\nYou got to Level: " + ScoreManager.LEVEL;//displaying what level the player reached
        scoreGT.gameObject.SetActive(true);//turning off the score counter text
        UpdateHighScore();
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

    public void UpdateT()
    {
        scoreGT.text = "Your score: " + ScoreManager.SCORE;//shows players current score
        fixedLevel.text = "Level: " + ScoreManager.LEVEL;//shows players current level
        shield.text = "Shield: " + Hero.SHIELD.ToString();

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
        if (Hero.CHECK == true)
        { 
        int i = 10;
            i = i - Hero.TIME;
            gunGT.text = "Current gun: " + Weapon.GUN + ", " + i.ToString() + " seconds left";//show current weapon type
        }//show current weapon type
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
        level.text = "Level: " + ScoreManager.LEVEL;//level displayed is the current level
        level.gameObject.SetActive(true);
        Invoke("HideLevelText", levelStartDelay);//will only be displayed for a short period of time
    }

    private void HideLevelText()
    {
        level.gameObject.SetActive(false);//taking away the text after it has displayed for 2 seconds
    }

    public static void UpdatePickup()
    {
        try
        { // try-catch stops an error from breaking your program 
            T.UpdateP();
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("TextManager:UpdatePickup() called while T=null.\n" + nre);
        }
    }

    public void UpdateP()
    {
        pickup.text = "Pickup: ";
        /*if (Hero.SHIELD == 4 && Bomb.CHECK != true && Hero.CHECK != true)
        {
            pickup.text += "Shield";
        }
        else if (Hero.CHECK == true)
        {
            pickup.text += "Multi";
        }

        else 
        {
            pickup.text += "Bomb";
        }*/


        //level displayed is the current level
        pickup.gameObject.SetActive(true);
        Invoke("HidePickupText", pickupStartDelay);//will only be displayed for a short period of time
    }

    private void HidePickupText()
    {
       pickup.gameObject.SetActive(false);//taking away the text after it has displayed for 1 seconds
    }

}
