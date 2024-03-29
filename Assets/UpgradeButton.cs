using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public RectTransform rect;
    Vector2 position;
    public bool purchased = false;
    public UpgradeObject upgrade;
    public UpgradeButton nextUpgrade;
    public Image buttonIcon;
    public Image icon;
    public Color green;
    public Color grey;
    public Color red;
    public UpgradeTooltipManager tooltipManager;
    bool displayTooltip = true;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        position = rect.position;
        position.y = rect.position.y;
        //position.x += 100;
    }

    public void PurchaseUpgrade() {
        if (purchased) { return; }
        
        if (upgrade != null)
        {
            if (!CheckIfUpgradePurchaseable()) { return; }

            buttonIcon.color = green;
            GetComponent<Button>().interactable = false;
            if (nextUpgrade != null)
            {
                nextUpgrade.GetComponent<Button>().interactable = true;
                nextUpgrade.icon.color = grey;
                nextUpgrade.buttonIcon.color = grey;
            }
            for (int i = 0; i < upgrade.requiredItems.Length; i++)
            {
                PlayerManager.instance.playerInventory.FindItemInInventory(upgrade.requiredItems[i].requiredItem.data).RemoveAmount(upgrade.requiredItems[i].requiredAmount);
            }
            upgrade.PerformAction();
            UnHighlight();
            displayTooltip = false;
        }
        else
        {
            Debug.Log("No Assigned Upgrade");
        }
    }

    public bool CheckIfUpgradePurchaseable() 
    {
        for (int i = 0; i < upgrade.requiredItems.Length; i++)
        {
            if (PlayerManager.instance.playerInventory.FindItemInInventory(upgrade.requiredItems[i].requiredItem.data).amount < upgrade.requiredItems[i].requiredAmount)
            {
                return false;
            }
        }

        return true;
    }

    public void Highlight()
    {
        if (!displayTooltip) { return; }
        if (upgrade == null) { return; }

        tooltipManager.ShowTooltip(upgrade, new Vector2(rect.position.x -225, rect.position.y));
    }

    public void UnHighlight()
    {
        if (!displayTooltip) { return; }
        if (upgrade == null) { return; }

        tooltipManager.HideTooltip();
    }
}
