using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public PlayerSOConnections so;
    public PlayerControls playerControls;
    public TownController respawnLocation;
    public PlayerFollow playerFollow;
    public DeathCanvas deathCanvas;

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
    public UserInterface playerInventoryUI;
    public GameObject merchantUI;
    public UserInterface merchantInventoryUI;

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

    #region Shop Methods
    public void EnterShop() 
    {
        //merchantInventory.SyncNewInventory();
        inShop = true;
        playerUI.SetActive(true);
        playerInventoryUI.SyncNew(playerInventory);
        merchantUI.SetActive(true);
        merchantInventoryUI.SyncNew(currentVendor.inventory);
        PriceManager.instance.SetItems(currentVendor.inventory);
        //playerInventoryUI.SyncMarketPrices();
        //merchantInventoryUI.SyncMarketPrices();

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
