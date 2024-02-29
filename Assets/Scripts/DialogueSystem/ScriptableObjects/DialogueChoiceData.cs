using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoiceData
{
    [field: SerializeField] public string text { get; set; }
    [field: SerializeField] public DialogueSO nextDialogue { get; set; }
    [field: SerializeField] public DialogueActions action { get; set; }
    [field: SerializeField] public QuestSO questStartingPoint { get; set; }
    [field: SerializeField] public QuestSO questCompletePoint { get; set; }
    [field: SerializeField] public QuestSO questHandinPoint { get; set; }
    [field: SerializeField] public DialogueContainerSO dialogueAfterCompletion { get; set; }

}
