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

    #region Triggers
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            PlayerManager.instance.inRangeOfShop = true;
            PlayerManager.instance.currentVendor = this;
            PlayerManager.instance.merchantInventory.inventory = inventory;
            promptCanvas.SetActive(true);

            if (questToComplete != null)
            {
                if (PlayerQuestManager.instance.CheckIfQuestActive(questToComplete))
                {
                    if (!PlayerQuestManager.instance.CheckIfQuestCompleted(questToComplete))
                    {
                        if (!PlayerQuestManager.instance.CheckIfQuestHandedIn(questToComplete))
                        {
                            PlayerQuestManager.instance.CompleteQuest(questToComplete);
                        }
                    }
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
            ExitShop(); 
            DialogueUIManager.instance.EndDialogue();
        }
    }
    #endregion

    #region Shop Methods
    public void EnterShop() 
    {
        cam.SetActive(true);
                
        promptCanvas.SetActive(false);        
    }

    public void ExitShop()
    { 
        cam.SetActive(false);
        PlayerManager.instance.ExitShop();
        promptCanvas.SetActive(true);
    }
    #endregion
}
