using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Create/Upgrade/New Speed Upgrade")]
public class Upgrade_Speed : UpgradeObject
{
    public float speed;

    public override void PerformAction()
    {
        PlayerControls.instance.roadSpeed = speed;
    }

    public override void FillTooltip() { }
}
