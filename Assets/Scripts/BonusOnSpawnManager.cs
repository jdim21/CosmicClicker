using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusOnSpawnManager : MonoBehaviour
{
    private int bonusOnSpawn = 0;
    public int GetBonusOnSpawn()
    {
        return bonusOnSpawn;
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
