using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    public VendorInventory inventory;
    public VendorItemPool pool;
    public GameObject cam;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log("ENTERED");
            cam.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ENTERED");
            cam.SetActive(false);
        }
    }
}
