using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Database", menuName = "Create/Quests/New Quest Database")]
public class QuestDatabase : ScriptableObject
{
    public QuestSO[] quests;

    [ContextMenu("Set Quest IDs")]
    public void SetIDs()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].questID != i)
            {
                quests[i].questID = i;
            }
        }
    }

    [ContextMenu("Clear Completion")]
    public void ClearCompletion()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            quests[i].questAccepted = false;
            quests[i].questCompleted = false;
            quests[i].questHandedIn = false;
        }
    }

    //public Item FindItemObject(QuestSO _item)
    //{
    //    for (int i = 0; i < items.Length; i++)
    //    {
    //        if (items[i].data.itemID == _item.data.itemID)
    //        {
    //            return items[i].data;
    //        }
    //    }
    //    return null;
    //}

    public QuestSO FindItem(int _questID)
    {
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].questID == _questID)
            {
                return quests[i];
            }
        }
        return null;
    }
}

