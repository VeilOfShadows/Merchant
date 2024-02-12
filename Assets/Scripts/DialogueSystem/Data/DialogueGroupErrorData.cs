using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogueGroupErrorData
{    public DialogueErrorData errorData { get; set; }
    public List<Group> groups { get; set; }

    public DialogueGroupErrorData()
    {
        errorData = new DialogueErrorData();
        groups = new List<Group>();
    }
}
