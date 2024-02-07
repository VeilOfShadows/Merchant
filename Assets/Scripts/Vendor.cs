using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    public VendorInventory inventory;
    public VendorItemPool pool;
    public GameObject cam;
    public GameObject merchantUI;
    public GameObject playerUI;
    public GameObject promptCanvas;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log("ENTERED");
            promptCanvas.SetActive(true);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                cam.SetActive(true);
                playerUI.SetActive(true);
                merchantUI.SetActive(true);
                playerUI.GetComponentInChildren<UserInterface>().merchantInterface = merchantUI.GetComponentInChildren<MerchantInventoryInterface>();
                playerUI.GetComponentInChildren<UserInterface>().inShop = true;
                promptCanvas.SetActive(false);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ENTERED");
            cam.SetActive(false);
            playerUI.SetActive(false);
            merchantUI.SetActive(false);
            promptCanvas.SetActive(false);
            playerUI.GetComponentInChildren<UserInterface>().merchantInterface = null;
            playerUI.GetComponentInChildren<UserInterface>().inShop = false;
        }
    }
}
