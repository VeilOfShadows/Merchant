using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OldDialogueContainer : ScriptableObject
{
    public List<OldNodeLinkData> nodeLinks = new List<OldNodeLinkData>();
    public List<OldDialogueNodeData> dialogueNodeData = new List<OldDialogueNodeData>();
}
