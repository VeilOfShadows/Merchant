using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainerSO : ScriptableObject
{
    public string fileName { get; set; }
    public List<DialogueSO> ungroupedDialogues { get; set; }

    public void Initialize(string _fileName) 
    {
        fileName = _fileName;
        ungroupedDialogues = new List<DialogueSO>();
    }
}
