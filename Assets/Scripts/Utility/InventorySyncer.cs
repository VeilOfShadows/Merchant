using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySyncer : MonoBehaviour
{
    public List<Inventory> inventories = new List<Inventory>();
    public List<VendorItemPool> vendorItemPools = new List<VendorItemPool>();

    private void Start()
    {
        //Sync();
    }

    [ContextMenu("Sync")]
    void Sync() {
        for (int i = 0; i < inventories.Count; i++)
        {
            inventories[i].Sync();
        }

        for (int i = 0; i < vendorItemPools.Count; i++)
        {
            vendorItemPools[i].Sync();
        }
    }
}
