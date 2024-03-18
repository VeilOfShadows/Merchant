using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class TownController : MonoBehaviour
{
    public SplineContainer nearestRoad;
    public Transform respawnLocation;
    public PlayerControls playerControls;
    public HouseController[] housesInTown;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            PlayerManager.instance.respawnLocation = this;
            playerControls.SetVillageSpeed();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerControls.SetRoadSpeed();
        }
    }

    public void ToggleHouseLights(bool toggle) 
    {
        for (int i = 0; i < housesInTown.Length; i++)
        {
            housesInTown[i].ToggleLights(toggle);
        }    
    }
}
