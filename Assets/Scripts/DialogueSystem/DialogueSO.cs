using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSO : ScriptableObject
{
    public string dialogueName { get; set; }
    [field: TextArea()]public string text { get; set; }
    public List<DialogueChoiceData> choices { get; set; }
    public DialogueType dialogueType { get; set; }
    public bool isStartingDialogue { get; set; }

    public void Initialize(string _dialogueName, string _text, List<DialogueChoiceData> _choices, DialogueType _dialogueType, bool _isStartingDialogue) 
    {
        dialogueName = _dialogueName;
        text = _text;
        choices = _choices;
        dialogueType = _dialogueType;
        isStartingDialogue = _isStartingDialogue;
    }
}
