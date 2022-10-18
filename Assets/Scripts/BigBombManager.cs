using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BigBombManager: MonoBehaviour
{
    public TMP_Text bigBombsText;
    int bigBombDamagePercentage = 50;
    int bigBombs = 0;
    int currentCost = 500;

    void Start()
    {
        bigBombsText.text = "BigBombs: " + bigBombs.ToString();
    }

    public int GetBigBombs()
    {
        return bigBombs;
    }

    public int GetCurrentCost()
    {
        return currentCost;
    }

    public void IncreaseCost()
    {
        currentCost = (int)((float)currentCost * 1.1f);
    }

    public void UsedBigBomb()
    {
        bigBombs--;
        bigBombsText.text = "BigBombs: " + bigBombs.ToString();
    }

    public void AddBigBomb()
    {
        bigBombs++;
        bigBombsText.text = "BigBombs: " + bigBombs.ToString();
    }

    public int GetBigBombDamagePercentage()
    {
        return bigBombDamagePercentage;
    }

    public void SetBigBombDamagePercentage(int newPercentage)
    {
        bigBombDamagePercentage = newPercentage;
    }

    public void AddToBigBombDamagePercentage(int percentageToAdd)
    {
        bigBombDamagePercentage += percentageToAdd;
    }
}
