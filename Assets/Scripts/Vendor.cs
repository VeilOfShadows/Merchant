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

    #region Triggers
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            PlayerManager.instance.inRangeOfShop = true;
            PlayerManager.instance.currentVendor = this;
            promptCanvas.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.instance.inRangeOfShop = false;
            ExitShop();            
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
        PlayerManager.instance.currentVendor = null;
        PlayerManager.instance.ExitShop();
        promptCanvas.SetActive(true);
    }
    #endregion
}
