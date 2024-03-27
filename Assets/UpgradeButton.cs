using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public bool purchased = false;
    public UpgradeObject upgrade;
    public UpgradeButton nextUpgrade;
    public Image buttonIcon;
    public Image icon;
    public Color green;
    public Color grey;
    public Color red;

    public void PurchaseUpgrade() {
        if (purchased)
        {
            return;
        }
        
        if (upgrade != null)
        {
            buttonIcon.color = green;
            GetComponent<Button>().interactable = false;
            if (nextUpgrade != null)
            {
                nextUpgrade.GetComponent<Button>().interactable = true;
                nextUpgrade.icon.color = grey;
                nextUpgrade.buttonIcon.color = grey;
            }
            upgrade.PerformAction();
        }
        else
        {
            Debug.Log("No Assigned Upgrade");
        }
    }
}
