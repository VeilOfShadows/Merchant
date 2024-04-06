using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    //public PlayerSOConnections so;
    public PlayerControls playerControls;
    public TownController respawnLocation;
    public PlayerFollow playerFollow;

    [Header("Upgrades")]
    public Upgrade_Price priceUpgrade;
    public Upgrade_Harvest harvestUpgrade;

    [Header("Inventory")]
    public Inventory playerInventory;
    public Inventory newInventory;
    public InventorySlot itemToAdd;

    [Header("Shop Details")]
    public bool inRangeOfShop = false;
    public bool inShop = false;
    public bool inDialogue = false;
    public Vendor currentVendor;
    public MerchantInventoryInterface merchantInventory;

    [Header("UI")]
    public GameObject playerUI;
    public UserInterface playerInventoryUI;
    public GameObject merchantUI;
    public UserInterface merchantInventoryUI;
    public GameObject upgradeUI;

    [Header("Misc")]
    public GameObject playerCartFire;
    public AudioTracker currentAudioTrackerZone;
    public DeathCanvas deathCanvas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        playerInventory.Sync();
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
        playerInventoryUI.SyncNew(playerInventory);
        if (inShop)
        {
            currentVendor.DeactivateShopCam();
            ExitShop();
        }
    }

    public void DisableInventoryUI()
    {
        playerUI.SetActive(false);
        if (inShop)
        {
            currentVendor.DeactivateShopCam();
            ExitShop();
        }
    }

    [ContextMenu("NEW")]
    public void NEWINVENT() 
    {
        newInventory.SyncWithNewInventory(playerInventory);
    }

    #region Shop Methods
    public void EnterShop() 
    {
        Debug.Log("E");
        //merchantInventory.SyncNewInventory();
        inShop = true;
        playerUI.SetActive(true);
        playerInventoryUI.SyncNew(playerInventory);
        merchantUI.SetActive(true);
        merchantInventoryUI.SyncNew(currentVendor.inventory);
        PriceManager.instance.SetItems(currentVendor.inventory);
        //playerInventoryUI.SyncMarketPrices();
        //merchantInventoryUI.SyncMarketPrices();

        //playerInventoryUI.merchantInterface = merchantUI.GetComponentInChildren<MerchantInventoryInterface>();
        playerInventoryUI.inShop = true;
        DialogueUIManager.instance.EndDialogue();
    }

    public void ExitShop()
    {
        inShop = false;
        inDialogue = false;
        playerUI.SetActive(false);
        merchantUI.SetActive(false);
        //playerInventoryUI.merchantInterface = null;
        TooltipManager.instance.HideTooltip();
        playerInventoryUI.inShop = false;
        PriceManager.instance.modifiedItems.Clear();
        //currentVendor.ExitShop();
    }

    public void OpenUpgradeMenu() { 
        upgradeUI.SetActive(true);
        playerUI.SetActive(true);
        playerInventoryUI.SyncNew(playerInventory);
    }

    public void CloseUpgradeMenu()
    {
        upgradeUI.SetActive(false);
        ExitShop();
    }

    public void DeactivateUI() {
        upgradeUI.SetActive(false);
        playerUI.SetActive(false);
        merchantUI.SetActive(false);
    }
    #endregion

    [ContextMenu("ADD")]
    public void AddItem() {
        playerInventory.AddItem(itemToAdd.item, itemToAdd.amount);
    }

    public void Die() 
    {
        deathCanvas.PlayFadeOut();
        playerControls.canControl = false;
        playerControls.roadSpline = null;
        playerControls.currentCam.SetActive(false);
        playerFollow.follow = false;
        playerFollow.agent.enabled = false;
    }

    public void Respawn(DeathCanvas deathCanvas)
    {
        transform.position = respawnLocation.respawnLocation.position;
        playerFollow.transform.position = respawnLocation.respawnLocation.position;

        NotificationManager.instance.DisplayNotification("Oh dear, you passed out from exhaustion and were rushed to the village.", true, 3f);
        PlayerHungerManager.instance.ResetHunger();

        playerControls.roadSpline = respawnLocation.nearestRoad;
        playerControls.currentRoad = playerControls.roadSpline.Splines[0]; 
        
        playerControls.currentCam = playerControls.roadSpline.GetComponent<RoadCamera>().camera;
        playerControls.currentCam.SetActive(true);

        playerControls.canControl = true;
        playerFollow.follow = true;
        playerFollow.agent.enabled = true;
        deathCanvas.PlayFadeIn();
    }

    //public IEnumerator
}
