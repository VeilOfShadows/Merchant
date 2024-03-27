using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Create/Upgrade/New Base Upgrade")]
public class UpgradeObject : ScriptableObject
{
    public string upgradeName;
    [TextArea(5,10)]
    public string upgradeDescription;
    public ItemRequirement[] requiredItems;

    public virtual void PerformAction() { }
}
