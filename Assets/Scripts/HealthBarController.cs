using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarController : MonoBehaviour
{
    private const float BAR_WIDTH = 1000f;

    private Image barImage;
    private Transform damagedBarTemplate;
    private int healthAmount;
    private int healthAmountMax;
    public TMP_Text healthBarText;

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        damagedBarTemplate = transform.Find("DamagedBarTemplate");
    }

    private void Start()
    {
        ResetHealth(100);
    }

    public void ResetHealth(int health)
    {
        SetHealth(health);
        SetHealthNormalized(GetHealthNormalized());
        UpdateHealthBarText();
    }

    public bool IsDestroyed()
    {
        return GetHealthNormalized() == 0;
    }

    private void HealthSystem_OnDamaged()
    {
        float beforeDamagedBarFillAmount = barImage.fillAmount;
        Transform damagedBar = Instantiate(damagedBarTemplate, transform);
        damagedBar.gameObject.SetActive(true);
        SetHealthNormalized(GetHealthNormalized());
        damagedBar.GetComponent<RectTransform>().anchoredPosition = new Vector2((barImage.fillAmount * BAR_WIDTH), damagedBar.GetComponent<RectTransform>().anchoredPosition.y);
        damagedBar.GetComponent<Image>().fillAmount = beforeDamagedBarFillAmount - barImage.fillAmount;
        damagedBar.gameObject.AddComponent<HealthBarCutFallDown>();
    }

    private void SetHealthNormalized(float healthNormalized)
    {
        barImage.fillAmount = healthNormalized;
    }
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
        HealthSystem_OnDamaged();
        UpdateHealthBarText();
    }

    private void UpdateHealthBarText()
    {
        healthBarText.text = healthAmount.ToString() + " / " + healthAmountMax.ToString();
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
