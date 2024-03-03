using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestStatus { 
    NotStarted,
    Accepted,
    Completed,
    //HandedIn,
    DEBUGFORCE
}

public enum QuestAction { 
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

    [Header("Quest Start Action")]
    public QuestAction questStartAction;
    public ItemObject questStartItem;
    public int questStartItemAmount;
    public string questStartFunctionName;

    [Header("Quest Items")]
    public ItemObject questRequiredItem;
    public int questRequiredItemAmount;

    [Header("Quest Complete Action")]
    public QuestAction questCompleteAction;
    public ItemObject questCompleteItem;
    public int questCompleteItemAmount;
    public string questCompleteFunctionName;
}
