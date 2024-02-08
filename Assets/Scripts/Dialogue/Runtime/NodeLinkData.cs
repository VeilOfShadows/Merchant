using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Will hold data about connections between two nodes
[Serializable]
public class NodeLinkData
{
    public string baseNodeGUID;
    public string portName;
    public string targetNodeGUID;
}
