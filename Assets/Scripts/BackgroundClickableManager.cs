using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundClickableManager : MonoBehaviour
{
    private const int ASTEROID_CLICK_FACTOR = 20;
    private const float VISIBLE_BOUNDARY_X = 15f;
    private const float VISIBLE_BOUNDARY_Y = 8f;
    private const float BACKGROUND_CLICKABLE_SPAWN_TIMER = 1.0f;
    private float backgroundClickableTimer;
    private float cometFlickerSpeed = 0.02f;
    private Transform backgroundClickable;
    private Vector3 velocity = Vector3.zero;
    public DamagePerClickManager damagePerClickManager;
    public ScoreManager scoreManager;

    public Transform GetBackgroundClickable()
    {
        return backgroundClickable;
    }

    public Transform Create()
    {
        Vector3 spawnLocation = new Vector3(-12.0f, 0f, 0f);
        int rand = UnityEngine.Random.Range(0, 2);
        Transform asset = GameAssets.i.pfComet1;
        if (rand == 0)
        {
            asset = GameAssets.i.pfComet1;
        }
        else if (rand == 1)
        {
            asset = GameAssets.i.pfAsteroid1;
        }
        Transform backgroundClickable = Instantiate(asset, spawnLocation, Quaternion.identity); 
        velocity = new Vector3(3.3f, 0f, 0f);
        return backgroundClickable;
    }
    void Start()
    {
        backgroundClickableTimer = BACKGROUND_CLICKABLE_SPAWN_TIMER;
    }

    public void ClickedBackgroundClickable()
    {
        if (backgroundClickable.gameObject.CompareTag("Comet"))
        {
            UnityEngine.Rendering.Universal.Light2D cometLight = backgroundClickable.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
            cometLight.intensity = 4f;
            cometLight.color = UnityEngine.Random.ColorHSV();
        }
        if (backgroundClickable.gameObject.CompareTag("Asteroid"))
        {
            int currDamagePerClick = damagePerClickManager.GetDamagePerClick();
            scoreManager.addToScore(currDamagePerClick * ASTEROID_CLICK_FACTOR);
            Vector3 spawnLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnLocation.z = -1;
            DamagePopup.Create(spawnLocation, currDamagePerClick * ASTEROID_CLICK_FACTOR);
            velocity = 20f * UnityEngine.Random.insideUnitCircle;
        }
    }

    void Update()
    {
        backgroundClickableTimer -= Time.deltaTime;
        if (backgroundClickable != null)
        {
            backgroundClickable.position += velocity * Time.deltaTime;
            if (IsOffScreen(backgroundClickable))
            {
                Destroy(backgroundClickable.gameObject);
                backgroundClickableTimer = BACKGROUND_CLICKABLE_SPAWN_TIMER;
            }
            else if (backgroundClickable.gameObject.CompareTag("Comet"))
            {
                UnityEngine.Rendering.Universal.Light2D cometLight = backgroundClickable.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
                if (cometLight.intensity >= 1f)
                {
                    cometFlickerSpeed = -0.02f;
                }
                else if (cometLight.intensity <= 0.5f)
                {
                    cometFlickerSpeed = 0.02f;
                }
                cometLight.intensity += cometFlickerSpeed;
            }
        }
        else if (backgroundClickableTimer <= 0)
        {
            backgroundClickable = Create();
        }
    }

    public bool IsOffScreen(Transform transform)
    {
        if (transform.position.x > VISIBLE_BOUNDARY_X ||
            transform.position.y > VISIBLE_BOUNDARY_Y ||
            transform.position.x < -VISIBLE_BOUNDARY_X ||
            transform.position.y < -VISIBLE_BOUNDARY_Y)
        {
            return true;
        }
        return false;
    }
}
