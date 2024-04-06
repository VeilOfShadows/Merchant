using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vendor Inventory", menuName = "Create/Vendors/New Inventory")]
public class VendorInventory : Inventory
{
    public VendorItemPool itemPool;

    [ContextMenu("Restock Shop")]
    public void RestockShop() {
        itemPool.Sync();
        Clear();
        int roll;
        for (int i = 0; i < itemPool.vendorItems.Length; i++)
        {
            roll = Random.Range(0, 100);
            if (roll <= itemPool.vendorItems[i].percentageChance)
            {
                //Debug.Log("You rolled: " + roll);
                AddItem(itemPool.vendorItems[i].item, Random.Range(itemPool.vendorItems[i].amountMin, itemPool.vendorItems[i].amountMax));
            }
        }

        for (int i = 0; i < demandItems.Length; i++)
        {
            demandItems[i].priceModifier = Mathf.RoundToInt(Random.Range(demandItems[i].priceMultiplierRange.x, demandItems[i].priceMultiplierRange.y));
        }

        Sync();
    }

    public override void AddItem(Item _item, int _amount)
    {
        InventorySlot slot = FindItemInInventory(_item);

        if (_item.itemType == ItemType.QuestItem)
        {
            if (slot == null)
            {
                SetEmptySlot(_item, _amount);
                return;
                //return true;
            }
        }
        else if (!database.items[_item.itemID].data.stackable || slot == null)
        {
            SetEmptySlot(_item, _amount);
            return;
            //return true;
        }
        slot.AddAmount(_amount);
        EvaluateWeight();

        if (MerchantInventoryInterface.instance != null)
        {
            MerchantInventoryInterface.instance.SyncWithInventory();
        }
        //PlayerManager.instance.playerInventoryUI.SyncWithInventory();
        //return true;
    }
}
