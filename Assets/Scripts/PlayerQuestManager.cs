using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestProgression { 
    Default,
    NotAccepted,
    Accepted,
    Completed,
    HandedIn,
    DEBUGFORCE
}

public class PlayerQuestManager : MonoBehaviour
{
    public static PlayerQuestManager instance;

    public QuestSO temp;
    public QuestProgression progression;

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

    public bool FindAvailableDialogues(QuestSO questToCheck, QuestProgression _progression) {

        progression = _progression;

        switch (progression)
        {
            case QuestProgression.Default:
                break;
            case QuestProgression.NotAccepted:
                if (CheckQuestAvailable(questToCheck))
                {
                    Debug.Log("Quest not accepted. Returning True");
                    progression = QuestProgression.Default;
                    return true;
                }
                break;
            case QuestProgression.Accepted:
                if (CheckIfQuestActive(questToCheck))
                {
                    Debug.Log("Quest accepted. Returning True");
                    progression = QuestProgression.Default;
                    return true;
                }
                break;

            case QuestProgression.Completed:
                if (CheckIfQuestCompleted(questToCheck))
                {
                    Debug.Log("Quest completed. Returning True");
                    progression = QuestProgression.Default;
                    return true;
                }
                break;

            case QuestProgression.HandedIn:
                if (CheckIfQuestHandedIn(questToCheck))
                {
                    Debug.Log("Quest handed in. Returning True");
                    progression = QuestProgression.Default;
                    return true;
                }
                break;

            case QuestProgression.DEBUGFORCE:
                return true;

            default:
                break;
        }

        Debug.Log("Quest not accepted. Returning false");

        progression = QuestProgression.Default;

        return false;
    }
}