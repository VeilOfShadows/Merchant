using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DebugManager : MonoBehaviour
{
    public GameObject debugPanel;
    public PlayerControls playerControls;
    public NavMeshAgent navMeshAgent;
    public QuestDatabase questDatabase;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            debugPanel.SetActive(!debugPanel.activeInHierarchy);
        }
    }

    public void SpeedUp() {
        playerControls.speed = 8;
        navMeshAgent.speed = 8;
        NotificationManager.instance.DisplayNotification("Player speed set to 8");
    }

    public void SlowDown()
    {
        playerControls.speed = 3;
        navMeshAgent.speed = 3;
        NotificationManager.instance.DisplayNotification("Player speed set to 3");
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
}
