using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToScore(int toAdd)
    {
        score = score + toAdd;
        if (score >= 1000000000)
        {
            scoreText.text = "SCORE: " + ((float)score / 1000000000f).ToString() + "B";
        }
        else if (score >= 1000000)
        {
            scoreText.text = "SCORE: " + ((float)score / 1000000f).ToString() + "M";
        }
        else if (score >= 1000)
        {
            scoreText.text = "SCORE: " + ((float)score / 1000f).ToString() + "K";
        }
        else
        {
            scoreText.text = "SCORE: " + score.ToString();
        }
    }
}
