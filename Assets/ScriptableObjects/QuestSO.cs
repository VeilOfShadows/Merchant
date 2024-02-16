using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Create/Quests/New Quest")]
public class QuestSO : ScriptableObject
{
    public int questID;
    public string questName;
    [TextArea(10,25)]
    public string questDescription;
    public bool questAccepted = false;
    public bool questComplete = false;
}
