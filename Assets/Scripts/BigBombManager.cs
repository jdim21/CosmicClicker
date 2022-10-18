using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BigBombManager: MonoBehaviour
{
    public TMP_Text bigBombsText;
    int bigBombDamagePercentage = 0;
    int bigBombs = 0;

    void Start()
    {
        bigBombsText.text = "BigBombs: " + bigBombs.ToString();
    }

    public int GetBigBombs()
    {
        return bigBombs;
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
