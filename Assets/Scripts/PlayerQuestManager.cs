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

    public bool FindAvailableDialogues(QuestSO questToCheck, QuestStatus _progression) {
        if (questToCheck.questStatus == _progression)
        {
            return true;
        }

        return false;
    }
}