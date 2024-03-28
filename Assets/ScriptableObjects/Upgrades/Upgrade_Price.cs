using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Create/Upgrade/New Inventory Upgrade")]
public class Upgrade_Price : UpgradeObject
{
    public float priceMultiplier;

    public override void PerformAction()
    {
        PlayerManager.instance.priceUpgrade = this;
    }

    public override void FillTooltip() 
    {
    
    }
}
