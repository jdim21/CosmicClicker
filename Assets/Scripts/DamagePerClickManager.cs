using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePerClickManager : MonoBehaviour
{
    public TMP_Text damagePerClickText;

    private int damagePerClick = 5;

    void Start()
    {
        damagePerClickText.text = "DamagePerClick: " + damagePerClick.ToString();
    }

    public int GetDamagePerClick()
    {
        return damagePerClick;
    }

    public void UpdateDamagePerClick(int newDamagePerClick)
    {
        damagePerClick = newDamagePerClick;
        damagePerClickText.text = "DamagePerClick: " + damagePerClick.ToString();
    }
}
