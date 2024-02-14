using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueGroupSaveData
{
    [field: SerializeField] public string ID { get; set; }  
    [field: SerializeField]public string name { get; set; }  
    [field: SerializeField]public Vector2 position { get; set; }  
}
