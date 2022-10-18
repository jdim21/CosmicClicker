using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopManager : MonoBehaviour, IPointerClickHandler
{
    private Vector3 SHOP_POSITION_CLOSED = new Vector3(1150f, 0, 0);
    private Vector3 SHOP_POSITION_OPEN = new Vector3(750f, 0, 0);
    public Transform shopPanel;
    private bool isShopOpen;
    public ShopItemDataSO[] shopItemsSO;
    public ShopTemplate[] shopTemplates;
    public TMP_Text currDescription;
    public TMP_Text currItemCost;
    public Button purchaseButton;
    private ShopItemDataSO.ItemTypes currItemType;
    private ShopTemplate currItemTemplate;
    public AutoClickerManager autoClickerManager;
    public BigBombManager bigBombManager;
    public BonusOnSpawnManager bonusOnSpawnManager;
    public ScoreManager scoreManager;


    void Start()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopTemplates[i].gameObject.SetActive(true);
        }
        isShopOpen = false;
        shopPanel.position = transform.position + SHOP_POSITION_CLOSED;
        currDescription.text = "***";
        currItemType = ShopItemDataSO.ItemTypes.Unset;
        LoadShopItems();
        purchaseButton.interactable = false;
    }

    public void Purchase()
    {
        Debug.Log("Purchase() called with currItemType: " + currItemType.ToString());
        int costOfPurchase = 0;
        if (currItemType == ShopItemDataSO.ItemTypes.AutoClick)
        {
            costOfPurchase = autoClickerManager.GetCurrentCost();
            autoClickerManager.AddToAutoClickerDamagePerSecond(1); 
            autoClickerManager.IncreaseCost();
            currItemCost.text = "COST: " + autoClickerManager.GetCurrentCost().ToString();
            currItemTemplate.currCost = autoClickerManager.GetCurrentCost();
        }
        else if (currItemType == ShopItemDataSO.ItemTypes.BonusOnSpawn)
        {
            costOfPurchase = bonusOnSpawnManager.GetCurrentCost();
            bonusOnSpawnManager.IncreaseCost();
            bonusOnSpawnManager.AddToBonusOnSpawn(5);
            currItemCost.text = "COST: " + bonusOnSpawnManager.GetCurrentCost().ToString();
            currItemTemplate.currCost = bonusOnSpawnManager.GetCurrentCost();
        }
        else if (currItemType == ShopItemDataSO.ItemTypes.BigBomb)
        {
            costOfPurchase = bigBombManager.GetCurrentCost();
            bigBombManager.IncreaseCost();
            currItemTemplate.currCost = bigBombManager.GetCurrentCost();
            bigBombManager.AddBigBomb();
            currItemCost.text = "COST: " + bigBombManager.GetCurrentCost().ToString();
            currItemTemplate.currCost = bigBombManager.GetCurrentCost();
        }
        scoreManager.SubFromScore(costOfPurchase);
        CheckPurchasable();
    }

    public void LoadShopItems()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopTemplates[i].titleText.text = shopItemsSO[i].itemName;
            shopTemplates[i].descriptionText.text = shopItemsSO[i].description;
            shopTemplates[i].costText.text = "COST: " + shopItemsSO[i].baseCost.ToString();
            shopTemplates[i].itemIcon.sprite = shopItemsSO[i].icon;
            shopTemplates[i].itemType = shopItemsSO[i].selectedItemType;
            shopTemplates[i].currCost = shopItemsSO[i].baseCost;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedGO = eventData.pointerCurrentRaycast.gameObject;
        if (clickedGO != null)
        {
            string nameClicked = clickedGO.name;
            if (nameClicked == "ShopToggle")
            {
                ToggleShopDrawer();
            }

            ShopTemplate currShopTemplate = null;
            currShopTemplate = clickedGO.GetComponent<ShopTemplate>();
            if (currShopTemplate == null)
            {
                currShopTemplate = clickedGO.transform.parent.GetComponent<ShopTemplate>();
            }
            if (currShopTemplate != null)
            {
                if (currShopTemplate.descriptionText != null)
                {
                    currDescription.text = currShopTemplate.descriptionText.text;
                    currItemType = currShopTemplate.itemType;
                    currItemTemplate = currShopTemplate;
                    currItemCost = currShopTemplate.costText;
                    CheckPurchasable();
                }
            }
        }
    }

    public void CheckPurchasable()
    {
        if (currItemTemplate != null && scoreManager.GetScore() >= currItemTemplate.currCost)
        {
            Debug.Log("score, currCost: " + scoreManager.GetScore() + ", " + currItemTemplate.currCost.ToString());
            purchaseButton.interactable = true;
        }
        else
        {
            purchaseButton.interactable = false;
        }
    }

    private void ToggleShopDrawer()
    {
        if (isShopOpen)
        {
            shopPanel.position = transform.position + SHOP_POSITION_CLOSED;
            purchaseButton.interactable = false;
            currDescription.text = "";
            currItemType = ShopItemDataSO.ItemTypes.Unset;
        }
        else
        {
            shopPanel.position = transform.position + SHOP_POSITION_OPEN;
        }
        isShopOpen = !isShopOpen;
    }
}
