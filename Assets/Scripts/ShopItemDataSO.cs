using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Objects/New Shop Item", order = 1)]
public class ShopItemDataSO : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int baseCost;

    public ItemTypes selectedItemType = new ItemTypes();
    public enum ItemTypes
    {
        Unset,
        AutoClick,
        BonusOnSpawn,
        BigBomb,
        EnhancementLootDrop,
    }
    
}
