using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestStatus { 
    NotStarted,
    Accepted,
    Completed,
    HandedIn,
    DEBUGFORCE
}

public enum QuestRewardType { 
    None,
    Item,
    Function
}

[CreateAssetMenu(fileName = "Quest", menuName = "Create/Quests/New Quest")]
public class QuestSO : ScriptableObject
{
    public int questID;
    public string questName;
    [TextArea(10,25)]
    public string questDescription;
    public QuestStatus questStatus;

    public QuestRewardType questRewardType;
    public ItemObject questRewardItem;
    public int questRewardItemAmount;
    public string questRewardFunctionName;
}
