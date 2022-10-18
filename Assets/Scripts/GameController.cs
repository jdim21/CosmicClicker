using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private const float CLICKABLE_SPAWN_BOUNDARY_X = 6.8f;
    private const float CLICKABLE_SPAWN_BOUNDARY_Y = 3.25f;
    private const int BASE_HEALTH = 100;
    private const float LOOT_BAG_FADE_TIMER_MAX = 4.0f;
    private const float AUTO_CLICKER_DAMAGE_TIMER_MAX = 1.0f;
    private Transform currentClickable;
    private Transform currentLootBag;
    public AutoClickerManager autoClickerManager;
    public BackgroundClickableManager backgroundClickableManager;
    public BigBombManager bigBombManager;
    public BonusOnSpawnManager bonusOnSpawnManager;
    public DamagePerClickManager damagePerClickManager;
    public HealthBarController healthBarController;
    public ScoreManager scoreManager;
    
    private float lootBagFadeTimer;
    private SpriteRenderer lootBagImage;
    private Color lootBagColor;
    private int clickablesDestroyed = 0;
    private int clicksTotal = 0;
    private int clicksTotalCurrClickable = 0;
    private Vector3 currentClickableVelocity;
    private float lootBagFlickerSpeed = 0.05f;
    private float autoClickerDamageTimer;

    void Start()
    {
        lootBagFadeTimer = LOOT_BAG_FADE_TIMER_MAX;
        autoClickerDamageTimer = AUTO_CLICKER_DAMAGE_TIMER_MAX;
        SpawnNewClickable();
    }

    void Update()
    {
        HandleAutoClicker();

        HandleClickableMovement();

        HandleNewClick();

        HandleLootBag();
    }

    private void HandleCurrentClickableDestroyed()
    {
        int rand = UnityEngine.Random.Range(1, 10);
        if (rand <= 5)
        {
            SpawnNewLootBag();
        }
        Destroy(currentClickable.gameObject);
        clickablesDestroyed++;
        Invoke("SpawnNewClickable", 0.3f);
    }

    private void HandleClickableMovement()
    {
        if (currentClickable != null)
        {
            currentClickable.position += currentClickableVelocity * Time.deltaTime;
            if (Math.Abs(currentClickable.position.x) > CLICKABLE_SPAWN_BOUNDARY_X) 
            {
                currentClickableVelocity.x *= -1;
            }
            if (Math.Abs(currentClickable.position.y) > CLICKABLE_SPAWN_BOUNDARY_Y) 
            {
                currentClickableVelocity.y *= -1;
            }
        }
    }

    private void HandleNewClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (backgroundClickableManager.GetBackgroundClickable() != null &&
                hit.collider == backgroundClickableManager.GetBackgroundClickable().GetComponent<PolygonCollider2D>())
            {
                backgroundClickableManager.ClickedBackgroundClickable();
            }

            if (currentClickable != null &&
                hit.collider == currentClickable.GetComponent<PolygonCollider2D>()) 
            {
                clicksTotal++;
                clicksTotalCurrClickable++;
                int currDamagePerClick = damagePerClickManager.GetDamagePerClick();
                scoreManager.addToScore((int)Math.Min((float)currDamagePerClick, (float)healthBarController.GetHealth()));
                Vector3 spawnLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                spawnLocation.z = -1;
                DamagePopup.Create(spawnLocation, currDamagePerClick);
                healthBarController.Damage(currDamagePerClick);
                
                if (healthBarController.IsDestroyed())
                {
                    HandleCurrentClickableDestroyed();
                }
            }
            else if (currentLootBag != null &&
                hit.collider == currentLootBag.GetComponent<PolygonCollider2D>()) 
            {
                int randUpgradeDamage = UnityEngine.Random.Range(1, (int)((float)damagePerClickManager.GetDamagePerClick() * 0.3f));
                randUpgradeDamage = Math.Max(1, randUpgradeDamage);
                damagePerClickManager.UpdateDamagePerClick(damagePerClickManager.GetDamagePerClick() + randUpgradeDamage);
                Vector3 spawnLocation = currentLootBag.position;
                LootBagPopup.Create(spawnLocation, "+" + randUpgradeDamage.ToString() + " DPC");
                Destroy(currentLootBag.gameObject);
            }
        }
    }

    private void HandleLootBag()
    {
        if (currentLootBag != null)
        {
            lootBagFadeTimer -= Time.deltaTime;
            if (lootBagFadeTimer <= 0)
            {
                float lootBagFadeSpeed = 5f;
                lootBagColor.a -= lootBagFadeSpeed * Time.deltaTime;
                lootBagImage.color = lootBagColor;

                if (lootBagColor.a <= 0)
                {
                    Destroy(currentLootBag.gameObject);
                }
            }
            UnityEngine.Rendering.Universal.Light2D lootBagLight = currentLootBag.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
            if (lootBagLight.intensity >= 1.0f)
            {
                lootBagFlickerSpeed = -0.05f;
            }
            else if (lootBagLight.intensity <= 0.25f)
            {
                lootBagFlickerSpeed = 0.05f;
            }
            lootBagLight.intensity += lootBagFlickerSpeed;
        }
    }
    private void HandleAutoClicker()
    {
        autoClickerDamageTimer -= Time.deltaTime;
        if (autoClickerDamageTimer <= 0 && currentClickable != null)
        {
            int autoClickerDamagePerSecond = autoClickerManager.GetAutoClickerDamagerPerSecond();
            if (autoClickerDamagePerSecond > 0)
            {
                clicksTotal++;
                clicksTotalCurrClickable++;
                scoreManager.addToScore((int)Math.Min((float)autoClickerDamagePerSecond, (float)healthBarController.GetHealth()));
                Vector3 spawnLocation = currentClickable.position;
                DamagePopup.Create(spawnLocation, autoClickerDamagePerSecond);
                healthBarController.Damage(autoClickerDamagePerSecond);
                
                if (healthBarController.IsDestroyed())
                {
                    HandleCurrentClickableDestroyed();
                }
            }
            autoClickerDamageTimer = AUTO_CLICKER_DAMAGE_TIMER_MAX;
        }
    }

    public void LaunchBigBomb()
    {
        if (bigBombManager.GetBigBombs() >= 1)
        {
            bigBombManager.UsedBigBomb();
            int currBigBombDamage = (int)(bigBombManager.GetBigBombDamagePercentage() * (float)healthBarController.GetMaxHealth() / 100f);
            scoreManager.addToScore((int)Math.Min((float)currBigBombDamage, (float)healthBarController.GetHealth()));
            Vector3 spawnLocation = currentClickable.position;
            DamagePopup.Create(spawnLocation, currBigBombDamage);
            healthBarController.Damage(currBigBombDamage);
            
            if (healthBarController.IsDestroyed())
            {
                HandleCurrentClickableDestroyed();
            }
        }
    }

    private Transform GetNextClickable()
    {
        Transform nextClickable;
        int rand = UnityEngine.Random.Range(1, 12);
        if (rand == 1)
        {
            nextClickable = GameAssets.i.pfCottonCandyPlanet;
        }
        else if (rand == 2)
        {
            nextClickable = GameAssets.i.pfMashedPotatoes;
        }
        else if (rand == 3)
        {
            nextClickable = GameAssets.i.pfBlueGreenPlanet;
        }
        else if (rand == 4)
        {
            nextClickable = GameAssets.i.pfGreenPlanet;
        }
        else if (rand == 5)
        {
            nextClickable = GameAssets.i.pfGreenPurplePlanet;
        }
        else if (rand == 6)
        {
            nextClickable = GameAssets.i.pfGreenRedPlanet;
        }
        else if (rand == 7)
        {
            nextClickable = GameAssets.i.pfRedBluePlanet;
        }
        else if (rand == 8)
        {
            nextClickable = GameAssets.i.pfRedPlanet;
        }
        else if (rand == 9)
        {
            nextClickable = GameAssets.i.pfYellowBluePlanet;
        }
        else if (rand == 10)
        {
            nextClickable = GameAssets.i.pfYellowPlanet;
        }
        else
        {
            nextClickable = GameAssets.i.pfYellowRedPlanet;
        }
        return nextClickable;
    }

    private void SpawnNewClickable()
    {
        float currentClickableVelocityX = UnityEngine.Random.Range(-0.3f, 0.3f);
        float currentClickableVelocityY = UnityEngine.Random.Range(-0.3f, 0.3f);
        currentClickableVelocity = new Vector3(currentClickableVelocityX, currentClickableVelocityY, 0);
        float newX = UnityEngine.Random.Range(-7f, 7f);
        float newY = UnityEngine.Random.Range(-3.5f, 2.5f);
        Vector3 spawnLocation = new Vector3(newX, newY, 0);
        Transform clickableToSpawn = GetNextClickable();
        currentClickable = Instantiate(clickableToSpawn, spawnLocation, Quaternion.identity); 
        healthBarController.ResetHealth(GetNextClickableHealth());
        clicksTotalCurrClickable = 0;
        if (bonusOnSpawnManager.GetBonusOnSpawn() > 0)
        {
            HandleBonusOnSpawn();
        }
    }

    private void HandleBonusOnSpawn()
    {
        clicksTotal++;
        clicksTotalCurrClickable++;
        int bonusOnSpawnDamage = bonusOnSpawnManager.GetBonusOnSpawn();
        scoreManager.addToScore((int)Math.Min((float)bonusOnSpawnDamage, (float)healthBarController.GetHealth()));
        Vector3 bonusOnSpawnLocation = currentClickable.position;
        DamagePopup.Create(bonusOnSpawnLocation, bonusOnSpawnDamage);
        healthBarController.Damage(bonusOnSpawnDamage);
        
        if (healthBarController.IsDestroyed())
        {
            HandleCurrentClickableDestroyed();
        }
    }

    private int GetNextClickableHealth()
    {
        const float GAP_CLOSE_TO_TARGET_CLICKS_FACTOR = 0.3f;
        if (clickablesDestroyed == 0)
        {
            Debug.Log("Init health: " + BASE_HEALTH.ToString());
            return BASE_HEALTH;
        }
        // int targetClicks = (BASE_HEALTH + clickablesDestroyed) * damagePerClickManager.GetDamagePerClick();
        float targetClicks = 20f + (clickablesDestroyed / 10 * 20f);
        int gapFromPreviousClickable = (int)targetClicks - clicksTotalCurrClickable;
        int gapCloseDelta = (int)((float)gapFromPreviousClickable * GAP_CLOSE_TO_TARGET_CLICKS_FACTOR);
        int nextClickableHealth = (clicksTotalCurrClickable + gapCloseDelta) * damagePerClickManager.GetDamagePerClick();
        Debug.Log("(targetClicks: " + targetClicks.ToString() + 
               "), (previousTotalClicks: " + clicksTotalCurrClickable.ToString() + 
               "), (damagePerClick: " + damagePerClickManager.GetDamagePerClick() + 
               "), (gapFromPreviousClickable: " + gapFromPreviousClickable.ToString() + 
               "), (gapCloseDelta: " + gapCloseDelta.ToString() + 
               "), (nextClickableHealth: " + nextClickableHealth.ToString() + 
               "), (clicks it will take: " + (nextClickableHealth / damagePerClickManager.GetDamagePerClick()).ToString());
        return nextClickableHealth;
    }

    private void SpawnNewLootBag()
    {
        if (currentLootBag != null)
        {
            Destroy(currentLootBag.gameObject);
        }
        float newX = currentClickable.position.x;
        float newY = currentClickable.position.y - 0.5f;
        Vector3 spawnLocation = new Vector3(newX, newY, 0);
        currentLootBag = Instantiate(GameAssets.i.pfLootBag, spawnLocation, Quaternion.identity); 
        lootBagFadeTimer = LOOT_BAG_FADE_TIMER_MAX;
        lootBagColor = currentLootBag.GetComponent<SpriteRenderer>().color;
        lootBagImage = currentLootBag.GetComponent<SpriteRenderer>();
    }
}
