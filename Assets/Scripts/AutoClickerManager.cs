using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClickerManager : MonoBehaviour
{
    private int autoClickerDamagePerSecond = 0;
    private int currentCost = 100;
    public int GetAutoClickerDamagerPerSecond()
    {
        return autoClickerDamagePerSecond;
    }

    public int GetCurrentCost()
    {
        return currentCost;
    }

    public void IncreaseCost()
    {
        currentCost = (int)((float)currentCost * 1.5f);
    }

    public void SetAutoClickerDamagerPerSecond(int newDamagePerSecond)
    {
        autoClickerDamagePerSecond = newDamagePerSecond;
    }

    public void AddToAutoClickerDamagePerSecond(int damageToAdd)
    {
        autoClickerDamagePerSecond += damageToAdd;
    }
}
