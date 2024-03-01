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
    public bool inShop = false;
    public bool inDialogue = false;
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
                if (!inDialogue)
                {
                    inDialogue = true;
                    if (!inShop)
                    {
                        DialogueUIManager.instance.StartDialogue(currentVendor.vendorDialogueController);
                    }
                    currentVendor.ActivateShopCam();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //inRangeOfShop = false;
                ExitShop();
                DialogueUIManager.instance.EndDialogue();
                currentVendor.DeactivateShopCam();
            }
        }
    }

    public void ToggleInventoryUI() { 
        playerUI.SetActive(!playerUI.activeInHierarchy);
        if (inShop)
        {
            currentVendor.DeactivateShopCam();
            ExitShop();
        }
    }

    #region Shop Methods
    public void EnterShop() 
    {
        //merchantInventory.SyncNewInventory();
        inShop = true;
        playerUI.SetActive(true);
        merchantUI.SetActive(true);

        playerUI.GetComponentInChildren<UserInterface>().merchantInterface = merchantUI.GetComponentInChildren<MerchantInventoryInterface>();
        playerUI.GetComponentInChildren<UserInterface>().inShop = true;
        DialogueUIManager.instance.EndDialogue();
    }

    public void ExitShop()
    {
        inShop = false;
        inDialogue = false;
        playerUI.SetActive(false);
        merchantUI.SetActive(false);
        playerUI.GetComponentInChildren<UserInterface>().merchantInterface = null;
        playerUI.GetComponentInChildren<UserInterface>().inShop = false;
        //currentVendor.ExitShop();
    }
    #endregion

    [ContextMenu("ADD")]
    public void AddItem() {
        playerInventory.AddItem(itemToAdd.item, itemToAdd.amount);
    }
}
