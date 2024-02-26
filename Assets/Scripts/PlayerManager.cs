using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public PlayerSOConnections so;

    [Header("Inventory")]
    public Inventory playerInventory;
    public InventorySlot itemToAdd;

    [Header("Shop Details")]
    public bool inRangeOfShop = false;
    public Vendor currentVendor;
    public MerchantInventoryInterface merchantInventory;

    [Header("UI")]
    public GameObject playerUI;
    public GameObject merchantUI;

    public GameObject playerCartFire;
    public AudioTracker currentAudioTrackerZone;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (inRangeOfShop)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //inRangeOfShop = false;
                DialogueUIManager.instance.StartDialogue(currentVendor.vendorDialogueController);
                currentVendor.EnterShop();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //inRangeOfShop = false;
                ExitShop();
                DialogueUIManager.instance.EndDialogue();
                currentVendor.ExitShop();
            }
        }
    }

    #region Shop Methods
    public void EnterShop() 
    {
        playerUI.SetActive(true);
        merchantUI.SetActive(true);

        playerUI.GetComponentInChildren<UserInterface>().merchantInterface = merchantUI.GetComponentInChildren<MerchantInventoryInterface>();
        playerUI.GetComponentInChildren<UserInterface>().inShop = true;
        DialogueUIManager.instance.EndDialogue();
    }

    public void ExitShop()
    {
        playerUI.SetActive(false);
        merchantUI.SetActive(false);
        playerUI.GetComponentInChildren<UserInterface>().merchantInterface = null;
        playerUI.GetComponentInChildren<UserInterface>().inShop = false;
    }
    #endregion

    [ContextMenu("ADD")]
    public void AddItem() {
        playerInventory.AddItem(itemToAdd.item, itemToAdd.amount);
    }
}
