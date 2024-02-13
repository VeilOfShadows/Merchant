using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGraphSaveDataSO : ScriptableObject
{
    public string fileName { get; set; }
    public List<DialogueNodeSaveData> nodes { get; set; }
    public List<string> oldUngroupedNodeNames { get; set; }
    //public SerializableDictionary<string, List<string>> old

    public void Initialize(string _fileName)
    {
        fileName = _fileName;

        nodes = new List<DialogueNodeSaveData>();
    }
}
