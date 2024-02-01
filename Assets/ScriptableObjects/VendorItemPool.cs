using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct VendorItems {
    public ItemObject itemObject;
    public Item item;
    public int amountMin;
    public int amountMax;
    public float percentageChance;
}

[CreateAssetMenu(fileName = "Vendor Item Pool", menuName = "Create/Vendors/New Item Pool")]
public class VendorItemPool : ScriptableObject
{
    public ItemDatabase database;
    public VendorItems[] vendorItems;

    [ContextMenu("Sync")]
    public void Sync() {
        for (int i = 0; i < vendorItems.Length; i++)
        {
            vendorItems[i].item = database.FindItemObject(vendorItems[i].itemObject);
        }
    }
}
