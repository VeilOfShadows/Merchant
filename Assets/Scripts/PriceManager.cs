using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PriceManager : MonoBehaviour
{
    public static PriceManager instance;
    public List<ItemPriceMultiplier> modifiedItems = new List<ItemPriceMultiplier>();
    public ItemDatabase itemDatabase;
    public Upgrade_Price playerPriceUpgrade;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public bool CheckIfPriceIsModified(Item _item)
    {
        for (int i = 0; i < modifiedItems.Count; i++)
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
        //for (int i = 0; i < modifiedItems.Count; i++)
        //{
        //    if (_item.itemID == modifiedItems[i].item.data.itemID)
        //    {
                if (GetModifier(_item) > 0)
                {
                    //price is higher
                    return true;
                }
                else
                {
                    //price is lower
                    //if (playerPriceUpgrade != null)
                    //{
                    //    float temp = Mathf.RoundToInt(_item.baseCoinValue * ((playerPriceUpgrade.priceMultiplier + 100) / 100));
                    //    if (temp )
                    //    {

                    //    }
                    //}
                    return false;
                }
        //    }
        //}
        //false == lower
        //return false;
    }

    public int GetAdjustedPrice(Item _item)
    {
        for (int i = 0; i < modifiedItems.Count; i++)
        {
            if (_item.itemID == modifiedItems[i].item.data.itemID)
            {
                return modifiedItems[i].adjustedPrice;
            }
        }
        if (playerPriceUpgrade != null)
        {
            float temp = Mathf.RoundToInt(_item.baseCoinValue * ((playerPriceUpgrade.priceMultiplier + 100) / 100));
            int clampedPrice = Mathf.CeilToInt(_item.baseCoinValue * 0.25f);

            if (temp < clampedPrice)
            {
                temp = clampedPrice;
            }
            return (int)temp;
        }
        return _item.baseCoinValue;
    }

    public void SetItems(Inventory inventory) 
    {
        modifiedItems.Clear();
        for (int i = 0; i < inventory.demandItems.Length; i++)
        {
            modifiedItems.Add(inventory.demandItems[i]);
        }
        AdjustPrices();
    }

    public void AdjustPrices()
    {
        int price;
        int playerUpgradeAdjustedPrice;
        int demandAdjustedPrice;
        int clampedPrice;
        //playerPriceUpgrade = PlayerManager.instance.priceUpgrade;

        for (int i = 0; i < modifiedItems.Count; i++)
        {
            price = modifiedItems[i].item.data.baseCoinValue;
            if (playerPriceUpgrade != null)
            {
                playerUpgradeAdjustedPrice = Mathf.RoundToInt(modifiedItems[i].item.data.baseCoinValue * ((-playerPriceUpgrade.priceMultiplier + 100) / 100));
                demandAdjustedPrice = Mathf.RoundToInt(playerUpgradeAdjustedPrice * ((modifiedItems[i].priceMultiplierPercent + 100) / 100));
            }
            else
            {
                demandAdjustedPrice = Mathf.RoundToInt(modifiedItems[i].item.data.baseCoinValue * ((modifiedItems[i].priceMultiplierPercent + 100) / 100));
            }
            clampedPrice = Mathf.CeilToInt(modifiedItems[i].item.data.baseCoinValue * 0.25f);

            if(demandAdjustedPrice < clampedPrice)
            {
                demandAdjustedPrice = clampedPrice;
            }
            modifiedItems[i].adjustedPrice = demandAdjustedPrice;
            //if (_item.itemID == modifiedItems[i].item.data.itemID)
            //{
            //    return Mathf.RoundToInt(_item.baseCoinValue * ((modifiedItems[i].priceMultiplierPercent + 100) / 100));
            //}
        }
        //return _item.baseCoinValue;
    }

    public int GetModifier(Item _item) 
    {
        float adjustedPrice = GetAdjustedPrice(_item);
        for (int i = 0; i < modifiedItems.Count; i++)
        {
            if (modifiedItems[i].item.data.itemID == _item.itemID)
            {
                float temp = adjustedPrice - modifiedItems[i].item.data.baseCoinValue;
                temp /= modifiedItems[i].item.data.baseCoinValue;
                temp *= 100;
                return (int)temp;
            }
        }
        if (playerPriceUpgrade != null)
        {
            return (int)playerPriceUpgrade.priceMultiplier;
            //return priceMultiplier 
        }
        return 0;
    }
}
