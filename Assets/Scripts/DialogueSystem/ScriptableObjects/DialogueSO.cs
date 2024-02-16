using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSO : ScriptableObject
{
    [field: SerializeField] public string dialogueName { get; set; }
    [field: SerializeField][field: TextArea()]public string text { get; set; }
    [field: SerializeField]public List<DialogueChoiceData> choices { get; set; }
    [field: SerializeField]public SerializableDictionary<List<DialogueChoiceData>, DialogueSO> choiceResults { get; set; }
    [field: SerializeField]public DialogueType dialogueType { get; set; }
    [field: SerializeField]public bool isStartingDialogue { get; set; }

    public void Initialize(string _dialogueName, string _text, List<DialogueChoiceData> _choices, SerializableDictionary<List<DialogueChoiceData>, DialogueSO> _choiceResults, DialogueType _dialogueType, bool _isStartingDialogue) 
    {
        dialogueName = _dialogueName;
        text = _text;
        choices = _choices;
        choiceResults = new SerializableDictionary<List<DialogueChoiceData>, DialogueSO>();
        dialogueType = _dialogueType;
        isStartingDialogue = _isStartingDialogue;
    }
}
