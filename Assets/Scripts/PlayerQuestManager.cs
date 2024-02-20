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

        if (questToAccept.questHandedIn)
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
        completedQuestList.Add(quest);
        activeQuestList.Remove(quest);
        quest.questCompleted = true;
    }

    public void HandInQuest(QuestSO quest)
    {
        Debug.Log(quest);
        handinQuestList.Add(quest);
        completedQuestList.Remove(quest);
        quest.questHandedIn = true;
    }

    public bool CheckIfQuestActive(QuestSO questToCheck)
    {
        if (questToCheck.questAccepted)
        {
            return true;
        }
        //if (activeQuestList.Contains(questToCheck))
        //{
        //    return true;
        //}
        return false;
    }
    
    public bool CheckIfQuestCompleted(QuestSO questToCheck)
    {
        if (questToCheck.questCompleted)
        {
            return true;
        }
        //if (completedQuestList.Contains(questToCheck))
        //{
        //    return true;
        //}
        return false;
    }

    public bool CheckIfQuestHandedIn(QuestSO questToCheck)
    {
        if (questToCheck.questHandedIn)
        {
            return true;
        }
        //if (handinQuestList.Contains(questToCheck))
        //{
        //    return true;
        //}
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