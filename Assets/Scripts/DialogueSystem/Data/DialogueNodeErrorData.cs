using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNodeErrorData
{
    public DialogueErrorData errorData { get; set; }
    public List<DialogueNode> nodes { get; set; }

    public DialogueNodeErrorData()
    { 
        errorData = new DialogueErrorData();
        nodes = new List<DialogueNode>();
    }
}
