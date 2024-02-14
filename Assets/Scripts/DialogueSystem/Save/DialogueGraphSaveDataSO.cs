using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGraphSaveDataSO : ScriptableObject
{
    [field: SerializeField] public string fileName { get; set; }
    [field: SerializeField]public List<DialogueGroupSaveData> groups { get; set; }
    [field: SerializeField]public List<DialogueNodeSaveData> nodes { get; set; }
    [field: SerializeField]public List<string> oldGroupNames { get; set; }
    [field: SerializeField]public List<string> oldUngroupedNodeNames { get; set; }
    [field: SerializeField]public SerializableDictionary<string, List<string>> oldGroupedNodeNames { get; set; }

    public void Initialize(string _fileName)
    {
        fileName = _fileName;

        groups = new List<DialogueGroupSaveData>();
        nodes = new List<DialogueNodeSaveData>();
    }
}
