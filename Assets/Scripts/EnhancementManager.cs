using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnhancementManager : MonoBehaviour
{
    public const int FOCUS_FIRE_BASE_DAMAGE_MULTIPLIER = 5;

    private int PERCENT_CHANCE_LEGENDARY = 2;
    private int PERCENT_CHANCE_EPIC = 10;
    private int PERCENT_CHANCE_RARE = 25;
    private int PERCENT_CHANCE_UNCOMMON = 70;
    private Enhancement currentEnhancement = null;
    private Enhancement currentEnhancementRoll = null;
    private int currentCost = 100;

    public Canvas enhancementDropCanvas;
    public Image enhancementSlot;
    public Image enhancementSlotCurrentImage;
    public Image enhancementSlotCurrentQualityImage;
    public Slider enhancementSlotTimerSlider;
    public Image enhancementDropIcon;
    public Image enhancementDropQuality;

    public int GetCurrentCost()
    {
        return currentCost;
    }

    public void IncreaseCost(int currentScore)
    {
        currentCost = Math.Max(currentCost, (int)((float)currentScore * 0.1f));
    }

    public Enhancements.EnhancementType GetEnhancementType()
    {
        return currentEnhancement.type;
    }
    public void EquipRolledEnhancement()
    {
        currentEnhancement = currentEnhancementRoll;
        SetNoRoll();
    }

    public void SetCurrentEnhancement(Enhancements.EnhancementType enhancementType, Enhancements.EnhancementQuality quality, int baseDamage)
    {
        currentEnhancement = new Enhancement(enhancementType, quality, baseDamage);
    }

    public bool IsEnhancementEquipped()
    {
        return currentEnhancement != null;
    }

    public bool IsEnhancementDrop()
    {
        return currentEnhancementRoll != null;
    }

    public int GetCurrentEnhancementBaseDamage()
    {
        if (currentEnhancement != null)
        {
            return currentEnhancement.baseDamage;
        }
        return 0;
    }

    public void SetCurrentEnhancementBaseDamage(int baseDamage)
    {
        if (currentEnhancement != null)
        {
            currentEnhancement.baseDamage = baseDamage;
        }
    }

    public Enhancements.EnhancementType GetRolledType()
    {
        return currentEnhancementRoll.type;
    }

    public Enhancements.EnhancementQuality GetRolledQuality()
    {
        return currentEnhancementRoll.quality;
    }

    public int GetRolledDamage()
    {
        return currentEnhancementRoll.baseDamage;
    }

    public void RollNewEnhancement(int baseDamage)
    {
        Enhancements.EnhancementType rolledType = Enhancements.EnhancementType.Potency;
        int typeRoll = UnityEngine.Random.Range(1, 5);
        if (typeRoll == 1)
        {
            rolledType = Enhancements.EnhancementType.Potency;
        }
        else if (typeRoll == 2)
        {
            rolledType = Enhancements.EnhancementType.Focus;
        }
        else if (typeRoll == 3)
        {
            rolledType = Enhancements.EnhancementType.Timed;
        }
        else if (typeRoll == 4)
        {
            rolledType = Enhancements.EnhancementType.Precision;
        }

        int damageRoll = baseDamage;

        Enhancements.EnhancementQuality rolledQuality = RollQuality();

        if (rolledQuality == Enhancements.EnhancementQuality.Legendary)
        {
            damageRoll += (int)((float)damageRoll * (float)UnityEngine.Random.Range(1, 26) / 100);
        }
        else if (rolledQuality == Enhancements.EnhancementQuality.Epic)
        {
            damageRoll += (int)((float)damageRoll * (float)UnityEngine.Random.Range(1, 21) / 100);
        }
        else if (rolledQuality == Enhancements.EnhancementQuality.Rare)
        {
            damageRoll += (int)((float)damageRoll * (float)UnityEngine.Random.Range(1, 16) / 100);
        }
        else if (rolledQuality == Enhancements.EnhancementQuality.Uncommon)
        {
            damageRoll += (int)((float)damageRoll * (float)UnityEngine.Random.Range(1, 11) / 100);
        }
        else
        {
            damageRoll += (int)((float)damageRoll * (float)UnityEngine.Random.Range(1, 6) / 100);
        }

        // TODO: roll for unique bonus effects?

        currentEnhancementRoll = new Enhancement(rolledType, rolledQuality, damageRoll);
    }

    public Enhancements.EnhancementQuality RollQuality()
    {
        int rolledPercent = UnityEngine.Random.Range(1, 101);

        if (rolledPercent <= PERCENT_CHANCE_LEGENDARY)
        {
            return Enhancements.EnhancementQuality.Legendary;
        }
        else if (rolledPercent <= PERCENT_CHANCE_EPIC)
        {
            return Enhancements.EnhancementQuality.Epic;
        }
        else if (rolledPercent <= PERCENT_CHANCE_RARE)
        {
            return Enhancements.EnhancementQuality.Rare;
        }
        else if (rolledPercent <= PERCENT_CHANCE_UNCOMMON)
        {
            return Enhancements.EnhancementQuality.Uncommon;
        }
        return Enhancements.EnhancementQuality.Common;
    }

    public void SetEnhancementDropCanvasValues()
    {
        Enhancements.EnhancementType rolledType = GetRolledType();
        Enhancements.EnhancementQuality rolledQuality = GetRolledQuality();

        Transform typeTextTransform = enhancementDropCanvas.transform.Find("TypeText");
        if (typeTextTransform)
        {
            TMP_Text typeTextTMPText = typeTextTransform.GetComponent<TMP_Text>();
            typeTextTMPText.text = "Type: " + GetRolledType().ToString();
        }
        Transform damageTextTransform = enhancementDropCanvas.transform.Find("DamageText");
        if (damageTextTransform)
        {
            TMP_Text damageTextTMPText = damageTextTransform.GetComponent<TMP_Text>();
            int rolledDamage = GetRolledDamage();
            string damageString = rolledDamage.ToString();
            if (rolledType == Enhancements.EnhancementType.Focus)
            {
                damageString = (rolledDamage * FOCUS_FIRE_BASE_DAMAGE_MULTIPLIER).ToString() + "/s";
            }
            damageTextTMPText.text = "Damage: " + damageString;
        }
        if (rolledType == Enhancements.EnhancementType.Potency)
        {
            enhancementDropIcon.sprite = GameAssets.i.enhancementTypePotency;
        }
        else if (rolledType == Enhancements.EnhancementType.Focus)
        {
            enhancementDropIcon.sprite = GameAssets.i.enhancementTypeFocus;
        }
        else if (rolledType == Enhancements.EnhancementType.Timed)
        {
            enhancementDropIcon.sprite = GameAssets.i.enhancementTypeTimed;
        }
        else if (rolledType == Enhancements.EnhancementType.Precision)
        {
            enhancementDropIcon.sprite = GameAssets.i.enhancementTypePrecision;
        }

        if (rolledQuality == Enhancements.EnhancementQuality.Legendary)
        {
            enhancementDropQuality.sprite = GameAssets.i.enhancementQualityLegendary;
        }
        else if (rolledQuality == Enhancements.EnhancementQuality.Epic)
        {
            enhancementDropQuality.sprite = GameAssets.i.enhancementQualityEpic;
        }
        else if (rolledQuality == Enhancements.EnhancementQuality.Rare)
        {
            enhancementDropQuality.sprite = GameAssets.i.enhancementQualityRare;
        }
        else if (rolledQuality == Enhancements.EnhancementQuality.Uncommon)
        {
            enhancementDropQuality.sprite = GameAssets.i.enhancementQualityUncommon;
        }
        else if (rolledQuality == Enhancements.EnhancementQuality.Common)
        {
            enhancementDropQuality.sprite = GameAssets.i.enhancementQualityCommon;
        }
    }

    public void SetNoRoll()
    {
        currentEnhancementRoll = null;
    }

    public void SetEnhancementObjectsInactive()
    {
        enhancementDropCanvas.gameObject.SetActive(false);
        enhancementSlot.gameObject.SetActive(false);
        enhancementSlotCurrentImage.gameObject.SetActive(false);
        enhancementSlotCurrentQualityImage.gameObject.SetActive(false);
        enhancementSlotTimerSlider.gameObject.SetActive(false);
    }

    public void SetDropCanvasActive(bool active)
    {
        enhancementDropCanvas.gameObject.SetActive(active);
    }

    public void SetEnhancementSlotActive(bool active)
    {
        enhancementSlot.gameObject.SetActive(active);
    }

    public void SetEnhancementSlotSpriteActive(bool active)
    {
        enhancementSlotCurrentImage.gameObject.SetActive(active);
    }

    public void SetEnhancementSlotQualitySpriteActive(bool active)
    {
        enhancementSlotCurrentQualityImage.gameObject.SetActive(active);
    }

    public void SetEnhancementSlotTimerSliderActive(bool active)
    {
        enhancementSlotTimerSlider.gameObject.SetActive(active);
    }

    public void SetEnhancementSlotTimerSliderValue(float value)
    {
        enhancementSlotTimerSlider.value = value;
    }

    public void SetEnhancementSlotSprite(Sprite sprite)
    {
        enhancementSlotCurrentImage.sprite = sprite;
    }

    public void SetEnhancementSlotQualitySprite(Sprite sprite)
    {
        enhancementSlotCurrentQualityImage.sprite = sprite;
    }

}
