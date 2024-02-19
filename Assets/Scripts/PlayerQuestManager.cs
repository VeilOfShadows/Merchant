using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestManager : MonoBehaviour
{
    public static PlayerQuestManager instance;

    public QuestSO temp;

    public List<QuestSO> activeQuestList = new List<QuestSO>();
    public List<QuestSO> completedQuestList = new List<QuestSO>();
    public List<QuestSO> handinQuestList = new List<QuestSO>();

    private void Awake()
    {
        if (instance == null)
        { 
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CompleteQuest(temp);
        }
    }

    public void AcceptQuest(QuestSO questToAccept) {
        if (questToAccept.questAccepted)
        {
            Debug.Log("Quest already Accepted");
            return;
        }

        if (questToAccept.questComplete)
        {
            Debug.Log("Quest already Completed");
            return;
        }

        if (!activeQuestList.Contains(questToAccept))
        {
            activeQuestList.Add(questToAccept);
            questToAccept.questAccepted = true;
        }
    }    

    public void CompleteQuest(QuestSO quest)
    { 
        activeQuestList.Remove(quest);
        completedQuestList.Add(quest);
        quest.readyForHandIn = true;
    }

    public void HandInQuest(QuestSO quest)
    {
        completedQuestList.Remove(quest);
        handinQuestList.Add(quest);
        quest.questComplete = true;
    }
    
    public bool CheckIfQuestReady(QuestSO questToCheck)
    {
        if (completedQuestList.Contains(questToCheck))
        {
            return true;
        }
        return false;
    }

    public bool CheckIfQuestCompleted(QuestSO questToCheck)
    {
        if (handinQuestList.Contains(questToCheck))
        {
            return true;
        }
        return false;
    }

    public bool CheckQuestAvailable(QuestSO questToCheck) {
        if (activeQuestList.Contains(questToCheck))
        { return false; }
        if (completedQuestList.Contains(questToCheck))
        { return false; }
        if (handinQuestList.Contains(questToCheck))
        { return false; }
        return true;
    }
}