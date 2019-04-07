using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    static private TextManager T;
    public float levelStartDelay = 2f;
    public Text gameOverGT, scoreCounterGT, levelCounterGT, weaponTypeGT, nextLevel;
    public Weapon weaponType;

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
        scoreCounterGT = go.GetComponent<Text>();
        UpdateScoreCT();

        //Set up LevelCounter UI Text
        go = GameObject.Find("LevelCounter");
        levelCounterGT = go.GetComponent<Text>();
        UpdateLevelCT();

        //Set up WeaponType UI Text
        go = GameObject.Find("WeaponType");
        weaponTypeGT = go.GetComponent<Text>();
        UpdateWeaponTT("Blaster");

        //Set up NextLevel UI Text
        go = GameObject.Find("NextLevel");
        nextLevel = go.GetComponent<Text>();
        UpdateNextLevelT();
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
        //Hide scoreCounterGT, levelCounterGT, weaponTypeGT UI Text
        scoreCounterGT.gameObject.SetActive(false);
        levelCounterGT.gameObject.SetActive(false);
        weaponTypeGT.gameObject.SetActive(false);

        //Set GameOver UI Text
        gameOverGT.text = "Game Over!\nYou got to Level: " + ScoreManager.LEVEL + "\nHigh Score: " + ScoreManager.HIGH_SCORE;
        
        //Display GameOver UI Text   
        gameOverGT.gameObject.SetActive(true);
    }

    public static void UpdateScoreCounterText()
    {
        try
        { // try-catch stops an error from breaking your program 
            T.UpdateScoreCT();
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("TextManager:UpdateText() called while T=null.\n" + nre);
        }
    }

    public void UpdateScoreCT()
    {
        scoreCounterGT.text = "Your score: " + ScoreManager.SCORE;
    }

    public static void UpdateLevelCounterText()
    {
        try
        { // try-catch stops an error from breaking your program 
            T.UpdateLevelCT();
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("TextManager:UpdateText() called while T=null.\n" + nre);
        }
    }

    public void UpdateLevelCT()
    {
        levelCounterGT.text = "Level: " + ScoreManager.LEVEL;
    }

    public static void UpdateWeaponTypeText(string type)
    {
        try
        { // try-catch stops an error from breaking your program 
            T.UpdateWeaponTT(type);
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("TextManager:UpdateText() called while T=null.\n" + nre);
        }
    }

    public void UpdateWeaponTT(string type)
    {
        weaponTypeGT.text = "Weapon Type: " + type;
    }

    public static void UpdateNextLevelText()
    {
        try
        { // try-catch stops an error from breaking your program 
            T.UpdateNextLevelT();
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("TextManager:UpdateLevel() called while T=null.\n" + nre);
        }
    }

    public void UpdateNextLevelT()
    {
        //Set up NextLevel UI Text
        nextLevel.text = "Level: " + ScoreManager.LEVEL;

        //Display NextLevel UI Text
        nextLevel.gameObject.SetActive(true);

        //Call function to hide NextLevel UI Text after levelStartDelay
        Invoke("HideLevelText", levelStartDelay);
    }

    private void HideLevelText()
    {
        //Hide NextLevel UI Text
        nextLevel.gameObject.SetActive(false);
    }
}
