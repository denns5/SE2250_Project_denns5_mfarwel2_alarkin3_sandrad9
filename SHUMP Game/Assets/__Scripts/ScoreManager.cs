﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static private ScoreManager S;
    static public int HIGH_SCORE = 0;
    private int _score = 0;

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

    }

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

    public void UpdateS(int p)
    {
        _score += p;
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
        {
            PlayerPrefs.SetInt("SHUMPHighScore", _score);
        }
    }

    static public int SCORE { get { return S._score; } }
}