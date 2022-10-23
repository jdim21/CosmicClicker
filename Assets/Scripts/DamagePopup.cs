using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private static int sortingOrder = 0;
    private bool isEnhancement = false;
    private float moveYSpeed = 2f;
    private float enhancementMoveXSpeed = 4f;
    private float enhancementAccXSpeed = -10f;

    public static DamagePopup Create(Vector3 spawnLocation, int damage, bool isEnhancement = false)
    {
        Transform damagePopupTransform = null;
        if (isEnhancement)
        {
            damagePopupTransform = Instantiate(GameAssets.i.pfEnhancementPopup, spawnLocation, Quaternion.identity); 
        }
        else
        {
            damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, spawnLocation, Quaternion.identity); 
        }
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        if (isEnhancement)
        {
            damagePopup.isEnhancement = true;
        }
        damagePopup.Setup(damage);
        return damagePopup;
    }

    private void Awake()
    {
        // textMesh = transform.GetComponent<TMP_Text>();
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        textColor = textMesh.color;
        disappearTimer = 0.5f;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }

    private void Update()
    {
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        if (isEnhancement)
        {
            enhancementMoveXSpeed += enhancementAccXSpeed * Time.deltaTime;
            transform.position += new Vector3(Math.Max(enhancementMoveXSpeed, 0), 0) * Time.deltaTime;
        }
        textMesh.color = textColor;

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 2f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
