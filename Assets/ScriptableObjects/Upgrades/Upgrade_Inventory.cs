using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Create/Upgrade/New Inventory Upgrade")]
public class Upgrade_Inventory : UpgradeObject
{
    public Inventory inventory;

    public override void PerformAction()
    {
        inventory.SyncWithNewInventory(PlayerManager.instance.playerInventory);
    }
}
