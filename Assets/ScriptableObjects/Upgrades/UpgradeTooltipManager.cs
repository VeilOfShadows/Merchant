using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class UpgradeTooltipManager : MonoBehaviour
{
    public GameObject tooltip;
    public TooltipPositioner positioner;
    public TextMeshProUGUI upgradeName;
    public TextMeshProUGUI upgradeDescription;
    public TextMeshProUGUI coinText;
    public Image coinImage;
    public List<Transform> requiredItemObjects = new List<Transform>();
    public ItemObject coinObject;
    VerticalLayoutGroup layout;
    public Color grey;
    public Color red;

    private void Start()
    {
        HideTooltip();
        //tooltip.SetActive(false);
    }

    public void ShowTooltip(UpgradeObject upgrade, Vector2 offset)
    {
        upgradeName.text = upgrade.upgradeName;
        upgradeDescription.text = upgrade.upgradeDescription;
        //itemDescription.text = _item.      
        for (int i = 0; i < upgrade.requiredItems.Length; i++)
        {
            if (upgrade.requiredItems[i].requiredItem == coinObject)
            {
                coinImage.transform.parent.gameObject.SetActive(true);
                InventorySlot coin = PlayerManager.instance.playerInventory.FindItemInInventory(upgrade.requiredItems[i].requiredItem.data);
                if (coin != null)
                {
                    coinText.text = coin.amount + "/" + upgrade.requiredItems[i].requiredAmount.ToString();
                    coinImage.sprite = coinObject.data.uiDisplay;
                    if (coin.amount < upgrade.requiredItems[i].requiredAmount)
                    {
                        coinText.color = red;
                        //coinImage.color = red;
                    }
                    else
                    {
                        coinText.color = grey;
                        //coinImage.color = grey;
                    }
                }
                else
                {
                    coinText.text = 0 + "/" + upgrade.requiredItems[i].requiredAmount.ToString();
                    coinImage.sprite = coinObject.data.uiDisplay;
                    coinText.color = red;
                    //coinImage.color = red;
                }
            }
            else
            {
                requiredItemObjects[i].gameObject.SetActive(true);
                //get player amount, get required amount
                InventorySlot temp = PlayerManager.instance.playerInventory.FindItemInInventory(upgrade.requiredItems[i].requiredItem.data);
                if (temp != null)
                {
                    requiredItemObjects[i].GetComponentInChildren<TextMeshProUGUI>().text = temp.amount.ToString() + "/" + upgrade.requiredItems[i].requiredAmount.ToString();
                    requiredItemObjects[i].GetComponentInChildren<Image>().sprite = upgrade.requiredItems[i].requiredItem.data.uiDisplay;
                    if (temp.amount < upgrade.requiredItems[i].requiredAmount)
                    {
                        requiredItemObjects[i].GetComponentInChildren<TextMeshProUGUI>().color = red;
                        //requiredItemObjects[i].GetComponentInChildren<Image>().color = red;
                    }
                    else
                    {
                        requiredItemObjects[i].GetComponentInChildren<TextMeshProUGUI>().color = grey;
                        //requiredItemObjects[i].GetComponentInChildren<Image>().color = grey;
                    }
                }
                else
                {
                    requiredItemObjects[i].GetComponentInChildren<TextMeshProUGUI>().text = 0 + "/" + upgrade.requiredItems[i].requiredAmount.ToString();
                    requiredItemObjects[i].GetComponentInChildren<Image>().sprite = upgrade.requiredItems[i].requiredItem.data.uiDisplay;
                    
                    requiredItemObjects[i].GetComponentInChildren<TextMeshProUGUI>().color = red;
                    //requiredItemObjects[i].GetComponentInChildren<Image>().color = red;                    
                }
            }
        }
        tooltip.GetComponent<RectTransform>().position = offset;
        //positioner.offset.x = xoffset;
        layout = tooltip.GetComponent<VerticalLayoutGroup>();
        //tooltip.SetActive(true);
        StartCoroutine(Pls());
        //layout.enabled = false;
        //layout.enabled = true;
    }

    public IEnumerator Pls() 
    {

        //layout.enabled = false;
        yield return null;
        //layout.enabled = true;
        tooltip.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)layout.transform);
        yield return null;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            layout.enabled = !layout.enabled;

        }
        //    Vector3 pos;
        //    pos.x = MouseData.slotHoveredOver.transform.position.x - rect.sizeDelta.x;

        //    pos.y = MouseData.slotHoveredOver.transform.position.y;
        //    pos += offset;
        //    rect.position = pos;
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
        upgradeName.text = "";
        upgradeDescription.text = "";
        coinImage.transform.parent.gameObject.SetActive(false);
        for (int i = 0; i < requiredItemObjects.Count; i++)
        {
            requiredItemObjects[i].gameObject.SetActive(false);
        }
        //itemDescription.text = _item.
    }
}
