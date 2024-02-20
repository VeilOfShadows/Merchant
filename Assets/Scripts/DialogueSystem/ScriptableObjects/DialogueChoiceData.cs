using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoiceData
{
    [field: SerializeField] public string text { get; set; }
    [field: SerializeField] public DialogueSO nextDialogue { get; set; }
    [field: SerializeField] public DialogueActions action { get; set; }

    //[field: SerializeField] public ScriptableObject functionObject { get; set; }
    //[field: SerializeField] public string methodName { get; set; }
    [field: SerializeField] public QuestSO questStartingPoint { get; set; }
    [field: SerializeField] public QuestSO questHandinPoint { get; set; }

}
