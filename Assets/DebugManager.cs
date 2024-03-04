using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class DebugManager : MonoBehaviour
{
    public GameObject debugPanel;
    public PlayerControls playerControls;
    public NavMeshAgent navMeshAgent;
    public QuestDatabase questDatabase;

    [Header("Tutorial Debug")]
    public SplineContainer tutorialRoad;
    public GameObject tutorialCam;
    public GameObject tutorialManager;

    public SplineContainer willowberryRoad;
    public GameObject willowberryCam;
    public GameObject willowberryTrigger1;
    public GameObject willowberryTrigger2;

    public Transform playerControlTransform;
    public Transform playerFollowTransform;
    public GameObject thistlewoodSideGate;
    public GameObject thistlewoodSideGateEast;
    public GameObject thistlewoodSideRoad;
    public GameObject lakeviewBridgeRoad;
    public GameObject lakeviewBridgeBroken;
    public GameObject lakeviewBridgeRepaired;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            TogglePanel(!debugPanel.activeInHierarchy);
            //debugPanel.SetActive(!debugPanel.activeInHierarchy);
        }
    }

    public void TogglePanel(bool toggle)
    {
        debugPanel.SetActive(toggle);
    }

    public void SpeedUp() {
        //playerControls.speed = 8;
        //navMeshAgent.speed = 8;
        playerControls.ChangeSpeed(5, true);
        NotificationManager.instance.DisplayNotification("Player speed set to: " + (playerControls.currentSpeed + 5));
    }

    public void SlowDown()
    {
        //playerControls.speed = 3;
        //navMeshAgent.speed = 3;
        playerControls.ChangeSpeed(5, false);
        NotificationManager.instance.DisplayNotification("Player speed set to: " + (playerControls.currentSpeed - 5));
    }

    public void SetTime(int time) {
        TimeManager.instance.time = time;
        TimeManager.instance.Advance(0);
        NotificationManager.instance.DisplayNotification("Time set to: " + time + " hours");
    }

    public void ClearQuests() {
        questDatabase.ClearCompletion();
        NotificationManager.instance.DisplayNotification("Quests cleared");
    }

    public void ToggleHunger()
    {
        PlayerHungerManager.instance.drainHunger = !PlayerHungerManager.instance.drainHunger;
        NotificationManager.instance.DisplayNotification("Hunger drain is now: " + PlayerHungerManager.instance.drainHunger);
    }

    [ContextMenu("Turn Off Tutorial")]
    public void TurnOffTutorial() {
        playerFollowTransform.position = new Vector3(-398, 0.5f, -365);
        playerControlTransform.position = new Vector3(-399, 0, -362); 
        willowberryCam.SetActive(true);
        playerControls.currentCam = willowberryCam;
        playerControls.roadSpline = willowberryRoad;
        willowberryTrigger1.SetActive(false);
        willowberryTrigger2.SetActive(false);
        tutorialRoad.gameObject.SetActive(false);
        tutorialManager.SetActive(false);
    }

    [ContextMenu("Turn On Tutorial")]
    public void TurnOnTutorial()
    {
        playerFollowTransform.position = new Vector3(-469,0.5f,-411);
        playerControlTransform.position = new Vector3(-467,0,-412);
        willowberryCam.SetActive(false);
        playerControls.currentCam = tutorialCam;
        playerControls.roadSpline = tutorialRoad;
        willowberryTrigger1.SetActive(true);
        willowberryTrigger2.SetActive(true);
        tutorialRoad.gameObject.SetActive(true);
        tutorialManager.SetActive(true);
    }

    [ContextMenu("Reset Level For Build")]

    public void ResetForBuild()
    {
        TurnOnTutorial();
        questDatabase.SetIDs();
        //set up player inventory
        thistlewoodSideGate.transform.localEulerAngles = new Vector3(0, 0, 0);
        thistlewoodSideGateEast.transform.localEulerAngles = new Vector3(0, 0, 0);
        thistlewoodSideRoad.SetActive(false);
        lakeviewBridgeRoad.SetActive(false);
        lakeviewBridgeBroken.SetActive(true);
        lakeviewBridgeRepaired.SetActive(false);
    }
}
