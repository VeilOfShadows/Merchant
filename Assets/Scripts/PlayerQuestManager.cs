using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestManager : MonoBehaviour
{
    public static PlayerQuestManager instance;

    public List<QuestSO> activeQuestList = new List<QuestSO>();
    public List<QuestSO> completedQuestList = new List<QuestSO>();

    private void Awake()
    {
        if (instance == null)
        { 
            instance = this;
        }
    }

    public void AcceptQuest(QuestSO questToAccept) {
        if (!activeQuestList.Contains(questToAccept))
        {
            activeQuestList.Add(questToAccept);
            questToAccept.questAccepted = true;
        }
    }
}