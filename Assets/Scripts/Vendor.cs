using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    [Header("Inventory")]
    public VendorInventory inventory;
    public VendorItemPool pool;

    [Header("Dialogue Controller")]
    public CharacterDialogueController vendorDialogueController;

    [Header("Misc")]
    public GameObject cam;    
    public GameObject promptCanvas;

    public QuestSO questToComplete;
    public QuestDatabase questDatabase;

    private void Start()
    {
        if (inventory != null)
        {
            inventory.RestockShop();
        }
    }

    #region Triggers
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            PlayerManager.instance.inRangeOfShop = true;
            PlayerManager.instance.currentVendor = this;
            //PlayerManager.instance.merchantInventoryUI.SyncNew(inventory);

            //PlayerManager.instance.merchantInventory.inventory = inventory;
            promptCanvas.SetActive(true);

            if (questToComplete != null)
            {
                if (questDatabase.GetQuestStatus(questToComplete.questID, QuestStatus.Accepted))
                {
                    questDatabase.SetQuestStatus(questToComplete.questID, QuestStatus.Completed);
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.instance.currentVendor = null;
            PlayerManager.instance.merchantInventory.inventory = null;
            PlayerManager.instance.inRangeOfShop = false;
            PlayerManager.instance.ExitShop();
            DialogueUIManager.instance.EndDialogue();
            cam.SetActive(false);
            promptCanvas.SetActive(false);
        }
    }
    #endregion

    #region Shop Methods
    public void ActivateShopCam() 
    {
        cam.SetActive(true);
                
        promptCanvas.SetActive(false);        
    }

    public void DeactivateShopCam()
    { 
        cam.SetActive(false);
        //PlayerManager.instance.ExitShop();
        promptCanvas.SetActive(true);
    }
    #endregion
}
