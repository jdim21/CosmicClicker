using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusOnSpawnManager : MonoBehaviour
{
    private int bonusOnSpawn = 0;
    private int currentCost = 200;
    public int GetBonusOnSpawn()
    {
        return bonusOnSpawn;
    }

    public int GetCurrentCost()
    {
        return currentCost;
    }

    public void IncreaseCost()
    {
        currentCost = (int)((float)currentCost * 1.2f);
    }

    public void SetBonusOnSpawn(int newBonusOnSpawn)
    {
        bonusOnSpawn = newBonusOnSpawn;
    }

    public void AddToBonusOnSpawn(int damageToAdd)
    {
        bonusOnSpawn += damageToAdd;
    }
}
