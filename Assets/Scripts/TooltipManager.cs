using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;
    public GameObject tooltip;
    public TooltipPositioner positioner;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemType;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemCoinValue;
    public TextMeshProUGUI itemPriceMultiplierValue;
    public Color green;
    public Color red;
    public TextMeshProUGUI itemNutritionalValue;
    public Upgrade_Price playerPriceUpgrade;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; 
        }
    }

    public void ShowTooltip(Item _item, float xoffset, bool isPlayer) {
        itemName.text = _item.itemName;
        itemType.text = _item.itemType.ToString();
        
        if (!_item.tradeable || _item.itemType == ItemType.QuestItem)
        {
            itemNutritionalValue.text = "-";
            itemCoinValue.text = "-";
            positioner.offset.x = xoffset;
            tooltip.SetActive(true);
            return;
        }

        int temp = PriceManager.instance.GetAdjustedPrice(_item, isPlayer? true:false);

        if (temp == _item.baseCoinValue) 
        {
            if (_item.itemType != ItemType.Consumable)
            {
                itemNutritionalValue.text = "-";
            }
            else
            {
                itemNutritionalValue.text = _item.nutritionalValue.ToString();
            }
            itemCoinValue.text = temp.ToString();
            positioner.offset.x = xoffset;
            tooltip.SetActive(true);
            return;
        }

        itemPriceMultiplierValue.gameObject.SetActive(true);
        itemCoinValue.text = PriceManager.instance.GetAdjustedPrice(_item, isPlayer ? true : false).ToString();

        if (PriceManager.instance.CheckHigherOrLower(_item))
        {
            if (isPlayer)
            {
                itemPriceMultiplierValue.color = green;
                itemPriceMultiplierValue.text = ("+" + PriceManager.instance.GetModifier(_item) + "%");
            }
            else
            {
                itemPriceMultiplierValue.color = red;
                itemPriceMultiplierValue.text = ("+" + PriceManager.instance.GetModifier(_item) + "%");
            }
        }
        else
        {
            if (isPlayer)
            {
                itemPriceMultiplierValue.color = red;
                itemPriceMultiplierValue.text = (PriceManager.instance.GetModifier(_item) + "%");
            }
            else
            {
                itemPriceMultiplierValue.color = green;
                itemPriceMultiplierValue.text = (PriceManager.instance.GetModifier(_item) + "%");
            }
        }

        if (_item.itemType != ItemType.Consumable)
        {
            itemNutritionalValue.text = "-";
        }
        else
        {
            itemNutritionalValue.text = _item.nutritionalValue.ToString();
        }        

        positioner.offset.x = xoffset;
        tooltip.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
        itemName.text = "";
        itemType.text = "";
        //itemDescription.text = _item.
        itemPriceMultiplierValue.text = "";
        itemPriceMultiplierValue.gameObject.SetActive(false);
        itemCoinValue.text = "";
        itemNutritionalValue.text = "";
    }
}
