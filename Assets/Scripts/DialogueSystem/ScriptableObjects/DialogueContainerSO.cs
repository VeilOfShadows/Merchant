using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainerSO : ScriptableObject
{
    [field: SerializeField]public string fileName { get; set; }
    [field: SerializeField]public SerializableDictionary<DialogueGroupSO, List<DialogueSO>> dialogueGroups { get; set; }
    [field: SerializeField]public List<DialogueSO> ungroupedDialogues { get; set; }

    public void Initialize(string _fileName) 
    {
        fileName = _fileName;
        dialogueGroups = new SerializableDictionary<DialogueGroupSO, List<DialogueSO>>();
        ungroupedDialogues = new List<DialogueSO>();
    }
}
