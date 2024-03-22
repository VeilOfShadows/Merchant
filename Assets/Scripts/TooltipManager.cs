using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
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

    public void ShowTooltip(Item _item, float xoffset, bool isPlayer) {
        itemName.text = _item.itemName;
        itemType.text = _item.itemType.ToString();
        //itemDescription.text = _item.

        if (PriceManager.instance.CheckIfPriceIsModified(_item))
        {
            itemPriceMultiplierValue.gameObject.SetActive(true);
            itemCoinValue.text = PriceManager.instance.GetAdjustedPrice(_item).ToString();

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
        }
        else
        {
            itemCoinValue.text = _item.baseCoinValue.ToString();
        }

        itemNutritionalValue.text = _item.nutritionalValue.ToString();
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
