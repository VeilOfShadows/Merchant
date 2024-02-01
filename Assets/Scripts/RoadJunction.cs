using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using Cinemachine;
using Unity.PlasticSCM.Editor.WebApi;

public class RoadJunction : MonoBehaviour
{
    public SplineContainer junctionRoad;
    public SplineContainer defaultRoad;
    public List<GameObject> roadSwitchers = new List<GameObject>();
    public PlayerControls playerControls;
    public RoadSwitch defaultRoadSwitch;
    public CinemachineVirtualCamera cam;
    //public List<GameObject> cameras = new List<GameObject>();

    //public List<int> roads = new List<int>();

    //public int currentRoad = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("SWITCHING ROAD");
            playerControls = other.GetComponent<PlayerControls>();
            if (CheckDirection(playerControls))
            {
                for (int i = 0; i < roadSwitchers.Count; i++)
                {
                    roadSwitchers[i].SetActive(true);                
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CheckDirection(playerControls))
            {
                playerControls.currentRoad = defaultRoad.Splines[0];
                //cam.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = defaultRoadSwitch.dolly;
                playerControls = null;
                Debug.Log("LEAVING ROAD");
                for (int i = 0; i < roadSwitchers.Count; i++)
                {
                    roadSwitchers[i].SetActive(false);
                }
            }
        }
    }

    public void SwitchRoad(GameObject roadSwitcher, Spline newRoad)
    {
        for (int i = 0; i < roadSwitchers.Count; i++)
        {
            if (roadSwitchers[i] == roadSwitcher)
            {
                roadSwitchers[i].SetActive(false);
            }
            else
            {
                roadSwitchers[i].SetActive(true);
            }
        }
        playerControls.currentRoad = newRoad;
    }

    public bool CheckDirection(PlayerControls player) {
        if (player.currentRoad != junctionRoad.Splines[0])
        {
            return false;
        }
        return true;
    }
}
