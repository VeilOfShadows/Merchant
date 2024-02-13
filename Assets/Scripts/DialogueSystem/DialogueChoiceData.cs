using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoiceData
{
    public string text { get; set; }
    public DialogueSO nextDialogue { get; set; }
}
