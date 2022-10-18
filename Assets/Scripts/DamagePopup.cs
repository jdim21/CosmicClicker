using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    // private TMP_Text textMesh;
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private static int sortingOrder = 0;

    public static DamagePopup Create(Vector3 spawnLocation, int damage)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, spawnLocation, Quaternion.identity); 
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
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
        float moveYSpeed = 2f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

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
