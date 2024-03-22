using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PriceManager : MonoBehaviour
{
    public static PriceManager instance;
    public ItemPriceMultiplier[] modifiedItems;
    public ItemDatabase itemDatabase;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public bool CheckIfPriceIsModified(Item _item)
    {
        for (int i = 0; i < modifiedItems.Length; i++)
        {
            if (_item.itemID == modifiedItems[i].item.data.itemID)
            {
                return true;
            }           
        }
        return false;
    }

    //true = higher, false = lower;
    public bool CheckHigherOrLower(Item _item)
    {
        for (int i = 0; i < modifiedItems.Length; i++)
        {
            if (_item.itemID == modifiedItems[i].item.data.itemID)
            {
                if (modifiedItems[i].priceMultiplierPercent > 0)
                {
                    //price is higher
                    return true;
                }
                else
                {
                    //price is lower
                    return false;
                }
            }
        }
        //false == lower
        return false;
    }

    public int GetAdjustedPrice(Item _item)
    {
        for (int i = 0; i < modifiedItems.Length; i++)
        {
            if (_item.itemID == modifiedItems[i].item.data.itemID)
            {
                return Mathf.RoundToInt(_item.baseCoinValue * ((modifiedItems[i].priceMultiplierPercent + 100) / 100));
            }
        }
        return _item.baseCoinValue;
    }

    public int GetModifier(Item _item) 
    {
        for (int i = 0; i < modifiedItems.Length; i++)
        {
            if (modifiedItems[i].item.data.itemID == _item.itemID)
            {
                return (int)modifiedItems[i].priceMultiplierPercent;
            }
        }
        return 0;
    }
}
