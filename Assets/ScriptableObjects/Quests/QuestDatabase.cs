using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct QuestDetails {
    public QuestSO quest;
    public QuestStatus questStatus;
}

[CreateAssetMenu(fileName = "Quest Database", menuName = "Create/Quests/New Quest Database")]
public class QuestDatabase : ScriptableObject
{
    [SerializeField]
    public QuestDetails[] quests;

    [ContextMenu("Set Quest IDs")]
    public void SetIDs()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].quest.questID != i)
            {
                quests[i].quest.questID = i;                
            }
        }
        ClearCompletion();
    }

    [ContextMenu("Clear Completion")]
    public void ClearCompletion()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            quests[i].questStatus = QuestStatus.NotStarted;
            quests[i].quest.questStatus = QuestStatus.NotStarted;
        }
    }

    [ContextMenu("Sync Completion")]
    public void SyncCompletion()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            //quests[i].questStatus = QuestStatus.NotStarted;
            quests[i].quest.questStatus = quests[i].questStatus;
        }
    }

    public QuestSO FindItem(int _questID)
    {
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].quest.questID == _questID)
            {
                return quests[i].quest;
            }
        }
        return null;
    }

    public bool GetQuestStatus(int _questID, QuestStatus _status)
    {
        if (FindItem(_questID).questStatus == _status)
        { return true; }
        return false;
    }

    public void SetQuestStatus(int _questID, QuestStatus status) {
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].quest.questID == _questID)
            {
                quests[i].quest.questStatus = status;
                quests[i].questStatus = status;
                switch (status)
                {
                    case QuestStatus.NotStarted:
                        break;
                    case QuestStatus.Accepted:
                        NotificationManager.instance.DisplayNotification(" - Quest Accepted: " + quests[i].quest.questName);
                        break;
                    case QuestStatus.Completed:
                        NotificationManager.instance.DisplayNotification(" - Quest Completed: " + quests[i].quest.questName);
                        break;
                    //case QuestStatus.HandedIn:
                    //    NotificationManager.instance.DisplayNotification(" - Quest Handed In: " + quests[i].quest.questName);
                        //break;
                    case QuestStatus.DEBUGFORCE:
                        break;
                    default:
                        break;
                }

            }
        }     
    }
}

