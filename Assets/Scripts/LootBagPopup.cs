using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootBagPopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private float moveXAcceleration = 0.08f;
    private float moveXSpeed = 0f;

    public static LootBagPopup Create(Vector3 spawnLocation, string lootBagPopupText)
    {
        Transform lootBagPopupTransform = Instantiate(GameAssets.i.pfLootBagText, spawnLocation, Quaternion.identity); 
        LootBagPopup lootBagPopup = lootBagPopupTransform.GetComponent<LootBagPopup>();
        lootBagPopup.Setup(lootBagPopupText);
        return lootBagPopup;
    }

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(string lootBagPopupText)
    {
        textMesh.SetText(lootBagPopupText);
        textColor = textMesh.color;
        disappearTimer = 0.4f;
        moveXSpeed = 0f;
    }

    private void Update()
    {
        float moveYSpeed = 1f;
        moveXSpeed += moveXAcceleration;
        transform.position += new Vector3(moveXSpeed, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
