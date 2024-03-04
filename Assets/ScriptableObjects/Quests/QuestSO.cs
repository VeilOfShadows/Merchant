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

[System.Serializable]
public struct QuestActions {
    public QuestAction action;
    public ItemObject item;
    public int itemAmount;
    public string fuctionID;
}

[System.Serializable]
public struct QuestItemRequirements {
    public ItemObject requiredItem;
    public int requiredAmount;
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
    public QuestActions[] startActions;

    [Header("Quest Items")]
    public QuestItemRequirements[] itemRequirement;

    [Header("Quest Complete Action")]
    public QuestActions[] completeActions;
}
