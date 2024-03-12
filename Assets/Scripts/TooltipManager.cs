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
    public TextMeshProUGUI itemNutritionalValue;

    public void ShowTooltip(Item _item, float xoffset) {
        itemName.text = _item.itemName;
        itemType.text = _item.itemType.ToString();
        //itemDescription.text = _item.
        itemCoinValue.text = _item.baseCoinValue.ToString();
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
        itemCoinValue.text = "";
        itemNutritionalValue.text = "";
    }
}
