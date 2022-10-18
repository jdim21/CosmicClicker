using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopTemplate : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text costText;
    public int currCost;
    public Image itemIcon;
    public ShopItemDataSO.ItemTypes itemType;
}
