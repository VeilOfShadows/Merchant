using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNodeSaveData
{
    public string ID { get; set; }
    public string name { get; set; }
    public string text { get; set; }
    public List<DialogueChoiceSaveData> choices { get; set; }
    public string groupID { get; set; }
    public DialogueType dialogueType { get; set; }
    public Vector2 position { get; set; }


}
