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
        int buyPrice = slotsOnInterface[obj].item.baseCoinValue;
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

    public override void OnDragStart(GameObject obj)
    { }
    public override void OnDragEnd(GameObject obj)
    { }
    public override void OnDrag(GameObject obj)
    { }
}
