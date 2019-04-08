using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public AudioClip levelUpSound;
    static private ScoreManager S;
    static public int HIGH_SCORE = 0;
    private int _score = 0;
    private int _level = 1;
    private AudioSource _source;

    void Awake()
    {
        if (S == null)
        {
            S = this; // Set the private singleton 
        }
        else
        {
            Debug.LogError("ERROR: ScoreManager.Awake(): S is already set!");
        }

        //Ensure that there isnt any previous scores
        if (PlayerPrefs.GetInt("SHUMPGames") != 1)
        {
            PlayerPrefs.SetInt("SHUMPGames", 1);
            PlayerPrefs.SetInt("SHUMPHighScore", 0);
        }

        // Check for a high score in PlayerPrefs
        if (PlayerPrefs.HasKey("SHUMPHighScore"))
        {
            HIGH_SCORE = PlayerPrefs.GetInt("SHUMPHighScore");
        }

        //Reset score
        _score = 0;
        _source = GetComponent<AudioSource>();//getting the audio source component
    }

    //Update the game score method that can be called outside of this class
    public static void UpdateScore(int p)
    {
        try
        { // try-catch stops an error from breaking your program 
            S.UpdateS(p);
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("ScoreManager:UpdateScore() called while S=null.\n" + nre);
        }
    }

    //Update the game score method that increments the score based on the value passed in as a parameter
    public void UpdateS(int p)
    {
        _score = _score + p;

        if (_score > _level*200+Main.S.enemySpawnPerSecond*100)//will only update level when the score is 200 times the level
        {
            UpdateL();
        }
    }

    //Update the level method that can be called outside of this class
    public static void UpdateLevel()
    {
        try
        { // try-catch stops an error from breaking your program 
            S.UpdateL();
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("ScoreManager:UpdateLevel() called while S=null.\n" + nre);
        }
    }

    //Update the level method that increments the level
    public void UpdateL()
    {
        _level++;//increase level by 1
        TextManager.UpdateLevel();//update the text
        Main.S.enemySpawnPerSecond += 0.2f;//increasing the spawn rate every level;
        _source.PlayOneShot(levelUpSound, 1f);//playing the level up sound
    }

    public static void GameOverScore()
    {
        try
        { // try-catch stops an error from breaking your program 
            S.GameOver();
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("ScoreManager:GameOverScore() called while S=null.\n" + nre);
        }
    }

    public void GameOver()
    {
        if (HIGH_SCORE <= _score)
        {//if there is a new high score...
            PlayerPrefs.SetInt("SHUMPHighScore", _score);//set the new high score
            HIGH_SCORE = PlayerPrefs.GetInt("SHUMPHighScore");//get the new high score so it can be displayed when called
        }
    }

    static public int SCORE { get { return S._score; } }
    static public int LEVEL { get { return S._level; } }
}