using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public ShopManager shopManager;

    int score = 0;

    void Start()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public void addToScore(int toAdd)
    {
        score = score + toAdd;
        scoreText.text = GetNumPrinted(score);
        shopManager.CheckPurchasable();
    }

    public string GetNumPrinted(int num)
    {
        string returnStr = "";
        if (num >= 1000000000)
        {
            returnStr = "SCORE: " + ((float)score / 1000000000f).ToString() + "B";
        }
        else if (score >= 1000000)
        {
            returnStr = "SCORE: " + ((float)score / 1000000f).ToString() + "M";
        }
        else if (score >= 1000)
        {
            returnStr = "SCORE: " + ((float)score / 1000f).ToString() + "K";
        }
        else
        {
            returnStr = "SCORE: " + score.ToString();
        }
        return returnStr;
    }

    public void SubFromScore(int toSub)
    {
        score = score - toSub;
        scoreText.text = GetNumPrinted(score);
    }
}
