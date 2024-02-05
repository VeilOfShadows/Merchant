using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vendor Inventory", menuName = "Create/Vendors/New Inventory")]
public class VendorInventory : Inventory
{
    public VendorItemPool itemPool;

    [ContextMenu("Restock Shop")]
    public void RestockShop() {
        Clear();
        int roll;
        for (int i = 0; i < itemPool.vendorItems.Length; i++)
        {
            roll = Random.Range(0, 100);
            if (roll <= itemPool.vendorItems[i].percentageChance)
            {
                Debug.Log("You rolled: " + roll);
                AddItem(itemPool.vendorItems[i].item, Random.Range(itemPool.vendorItems[i].amountMin, itemPool.vendorItems[i].amountMax));
            }
        }
        Sync();
    }
}
