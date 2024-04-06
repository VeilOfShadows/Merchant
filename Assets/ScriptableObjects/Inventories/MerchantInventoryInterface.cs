using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantInventoryInterface : UserInterface
{
    public static MerchantInventoryInterface instance;

    public override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
    }
    //public Inventory playerInventory;

    public override void OnClick(GameObject obj) {
        int buyPrice = PriceManager.instance.GetAdjustedPrice(slotsOnInterface[obj].item, false);
        //int buyPrice = slotsOnInterface[obj].item.baseCoinValue;
        if (PlayerManager.instance.playerInventory.AttemptPurchase(slotsOnInterface[obj].item, buyPrice))
        {
            slotsOnInterface[obj].RemoveAmount(1);
            syncedInventory.AddItem(syncedInventory.coinItem.data, buyPrice);
        }
        else
        {
            NotificationManager.instance.DisplayNotification("You do not have enough gold.", true, 1.4f);
        }
        SyncWithInventory();
    }

    //public override void SyncMarketPrices()
    //{
    //    for (int i = 0; i < syncedInventory.slots.Length; i++)
    //    {
    //        if (slotsOnInterface[inventory.slots[i].slotDisplay].item != null)
    //        {
    //            if (PriceManager.instance.CheckIfPriceIsModified(syncedInventory.slots[i].item))
    //            {
    //                if (PriceManager.instance.CheckHigherOrLower(syncedInventory.slots[i].item))
    //                {
    //                    inventory.slots[i].slotDisplay.transform.GetChild(3).gameObject.SetActive(true);
    //                }
    //                else
    //                {
    //                    inventory.slots[i].slotDisplay.transform.GetChild(2).gameObject.SetActive(true);
    //                }
    //            }
    //        }
    //    }
    //}

    public override void OnEnter(GameObject obj)
    {
        MouseData.slotHoveredOver = obj;
        growTween.Complete();
        growTween = obj.transform.DOScale(1.2f, .3f);
        if (slotsOnInterface[obj].item != null)
        {
            if (slotsOnInterface[obj].item.itemID != -1)
            {                
               tooltipManager.ShowTooltip(slotsOnInterface[obj].item, tooltipOffset, false);                
            }
        }
    }
    public override void OnDragStart(GameObject obj)
    { }
    public override void OnDragEnd(GameObject obj)
    { }
    public override void OnDrag(GameObject obj)
    { }
}