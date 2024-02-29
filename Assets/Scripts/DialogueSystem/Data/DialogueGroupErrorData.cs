#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class DialogueGroupErrorData
{    public DialogueErrorData errorData { get; set; }
    public List<DialogueGroup> groups { get; set; }

    public DialogueGroupErrorData()
    {
        errorData = new DialogueErrorData();
        groups = new List<DialogueGroup>();
    }
}
#endif