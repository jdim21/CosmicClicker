using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;

    private int healthAmount;
    private int healthAmountMax;

    public void SetHealth(int healthAmount)
    {
        healthAmountMax = healthAmount;
        this.healthAmount = healthAmount;
    }

    public void Damage(int amount)
    {
        healthAmount -= amount;
        if (healthAmount < 0)
        {
            healthAmount = 0;
        }
        if (OnDamaged != null)
        {
            OnDamaged(this, EventArgs.Empty);
        }
    }

    public float GetHealth()
    {
        return healthAmount;
    }

    public float GetHealthNormalized()
    {
        return (float)healthAmount / (float)healthAmountMax;
    }
}
