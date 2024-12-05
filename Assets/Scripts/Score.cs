using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public static class Score 
{
    public static event EventHandler OnHighScoreChanged;
    private static int score;
    public static void IntializeStatic()
    {
        OnHighScoreChanged = null;
        score = 0;
    }

    public static int GetScore()
    {
        return score;
    }

    public static void AddScore()
    {
        score += 100;
    }
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore", 0);
    }

    public static bool TrySetNewHighScore()
    {
        return TrySetNewHighScore(score);
    }
    public static bool TrySetNewHighScore(int score)
    {
        int highScore = GetHighScore();
        if (score > highScore) 
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
            if (OnHighScoreChanged != null)
            {
                OnHighScoreChanged(null, EventArgs.Empty);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
