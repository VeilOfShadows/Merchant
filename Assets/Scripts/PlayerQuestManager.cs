using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct QuestStatusTracker {
    public QuestSO quest;
    public QuestStatus questStatus;
}

public class PlayerQuestManager : MonoBehaviour
{
    public static PlayerQuestManager instance;

    public QuestDatabase questDatabase;
    public QuestSO temp;

    public QuestStatusTracker[] quests;

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
            questDatabase.SetQuestStatus(temp.questID, QuestStatus.Completed);
        }
    }

    public bool FindAvailableDialogues(List<QuestPrerequisites> questPrerequisites) {
        for (int i = 0; i < questPrerequisites.Count; i++)
        {
            if (questPrerequisites[i].prerequisiteQuest.questStatus != questPrerequisites[i].prerequisiteQuestProgressionRequirement)
            {
                Debug.Log("Quest: " + questPrerequisites[i].prerequisiteQuest.questName + " does not have progression: " + questPrerequisites[i].prerequisiteQuestProgressionRequirement);
                return false;
            }
        }

        //for (int i = 0; i < questsToCheck.Count; i++)
        //{
        //    if (questsToCheck[i].questStatus != _progression)
        //    {
        //        return false;
        //    }
        //}

        return true;
    }
}