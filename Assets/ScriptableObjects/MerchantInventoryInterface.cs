using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantInventoryInterface : UserInterface
{
    public Inventory playerInventory;

    public override void OnClick(GameObject obj) {
        int buyPrice = slotsOnInterface[obj].item.baseCoinValue;
        if (playerInventory.AttemptPurchase(slotsOnInterface[obj].item, buyPrice))
        {
            slotsOnInterface[obj].RemoveAmount(1);
        }
    }
}
