using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarCutFallDown : MonoBehaviour
{
    private RectTransform rectTransform;
    private float fallDownTimer;
    private float fadeTimer;
    private Image image;
    private Color color;

    private void Awake()
    {
        rectTransform = transform.GetComponent<RectTransform>();
        color = transform.GetComponent<Image>().color;
        image = transform.GetComponent<Image>();
        fallDownTimer = 0.3f;
        fadeTimer = 0.5f;
        
    }

    void Update()
    {
        fallDownTimer -= Time.deltaTime;
        if (fallDownTimer <= 0)
        {
            float fallSpeed = 150f;
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - fallSpeed * Time.deltaTime);
        }

        fadeTimer -= Time.deltaTime;
        if (fadeTimer <= 0)
        {
            float alphaFadeSpeed = 5f;
            color.a -= alphaFadeSpeed * Time.deltaTime;
            image.color = color;

            if (color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
