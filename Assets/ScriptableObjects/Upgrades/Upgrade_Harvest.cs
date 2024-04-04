using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Create/Upgrade/New Harvest Upgrade")]
public class Upgrade_Harvest : UpgradeObject
{
    public float effectChance;
    public float extraHarvestAmount;

    public override void PerformAction()
    {
        PlayerManager.instance.harvestUpgrade = this;
    }

    public override void FillTooltip() 
    {
    
    }

    public int EffectRoll() {
        int roll = Random.Range(0, 100);
        Debug.Log(roll);
        if (roll <= effectChance)
        {
            return (int)extraHarvestAmount;
        }
        return 0;
    }
}
