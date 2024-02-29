using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.Rendering;
using UnityEngine;

public class DialogueGroupSO : ScriptableObject
{
    [field: SerializeField] public string groupName { get; set; }

    public void Initialize(string _groupName)
    {
        groupName = _groupName;
    }
}
