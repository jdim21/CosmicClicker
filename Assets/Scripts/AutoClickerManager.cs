using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClickerManager : MonoBehaviour
{
    private int autoClickerDamagePerSecond = 0;
    public int GetAutoClickerDamagerPerSecond()
    {
        return autoClickerDamagePerSecond;
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
