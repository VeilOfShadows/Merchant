using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoiceSaveData
{
    [field: SerializeField] public string text { get; set; }
    [field: SerializeField]public string nodeID { get; set; }
}
