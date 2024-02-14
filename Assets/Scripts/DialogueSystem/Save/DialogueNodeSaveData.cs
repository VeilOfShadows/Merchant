using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNodeSaveData
{
    [field: SerializeField]public string ID { get; set; }
    [field: SerializeField]public string name { get; set; }
    [field: SerializeField]public string text { get; set; }
    [field: SerializeField]public List<DialogueChoiceSaveData> choices { get; set; }
    [field: SerializeField]public string groupID { get; set; }
    [field: SerializeField]public DialogueType dialogueType { get; set; }
    [field: SerializeField]public Vector2 position { get; set; }


}
