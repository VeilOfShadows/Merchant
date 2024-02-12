using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;

public class OldGraphSaveUtility
{
    private OldDialogueGraphView _targetGraphView;
    private OldDialogueContainer _containerCache;

    private List<UnityEditor.Experimental.GraphView.Edge> edges => _targetGraphView.edges.ToList();
    private List<OldDialogueNode> nodes => _targetGraphView.nodes.ToList().Cast<OldDialogueNode>().ToList();

    public static OldGraphSaveUtility GetInstance(OldDialogueGraphView targetGraphView)
    {
        return new OldGraphSaveUtility
        {
            _targetGraphView = targetGraphView
        };
    }

    public void SaveGraph(string fileName)
    {
        if (!edges.Any())
        {
            Debug.Log("NO EDGES");
            return;
        }

        var dialogueContainer = ScriptableObject.CreateInstance<OldDialogueContainer>();

        var connectedPorts = edges.Where(x=>x.input.node != null).ToArray();
        for (var i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as OldDialogueNode;
            var inputNode = connectedPorts[i].input.node as OldDialogueNode;

            dialogueContainer.nodeLinks.Add(new OldNodeLinkData
            {
                baseNodeGUID = outputNode.GUID,
                portName = connectedPorts[i].output.portName,
                targetNodeGUID = inputNode.GUID,
            });
        }

        foreach (var dialogueNode in nodes.Where(node=>!node.entryPoint))
        {
            dialogueContainer.dialogueNodeData.Add(new OldDialogueNodeData
            {
                nodeGUID = dialogueNode.GUID,
                dialogueText = dialogueNode.dialogueText,
                position = dialogueNode.GetPosition().position
            });
        }

        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
        {
            AssetDatabase.CreateFolder("Assets", "Resources");
        }

        AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    public void LoadGraph(string fileName)
    {
        _containerCache = Resources.Load<OldDialogueContainer>(fileName);

        if (_containerCache == null)
        {
            EditorUtility.DisplayDialog("File not found", "Target dialogue graph file does not exist", "Ok");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }

    private void ConnectNodes()
    {
        for (var i = 0; i < nodes.Count; i++)
        {
            var connections = _containerCache.nodeLinks.Where(x => x.baseNodeGUID == nodes[i].GUID).ToList();
            for (var j = 0; j < connections.Count(); j++)
            {
                var targetNodeGUID = connections[j].targetNodeGUID;
                var targetNode = nodes.First(x => x.GUID == targetNodeGUID);
                LinkNodes(nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);

                targetNode.SetPosition(new Rect(
                    _containerCache.dialogueNodeData.First(x => x.nodeGUID == targetNodeGUID).position,
                    _targetGraphView.defaultNodeSize));
            }
        }

        //for (var i = 0; i < nodes.Count; i++)
        //{
        //    var connections = _containerCache.nodeLinks.Where(x => x.baseNodeGUID == nodes[i].GUID).ToList();

        //    for (var j = 0; j < connections.Count; j++)
        //    {
        //        var targetedNodeGUID = connections[j].targetNodeGUID;
        //        var targetNode = nodes.First(x=>x.GUID == targetedNodeGUID);
        //        LinkNodes(nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);

        //        targetNode.SetPosition(new Rect(
        //            _containerCache.dialogueNodeData.First(x => x.nodeGUID == targetedNodeGUID).position,
        //            _targetGraphView.defaultNodeSize));
        //    }
        //}
    }

    private void LinkNodes(Port output, Port input)
    {
        var tempEdge = new UnityEditor.Experimental.GraphView.Edge
        {
            output = output,
            input = input
        };

        tempEdge?.input.Connect(tempEdge);
        tempEdge?.output.Connect(tempEdge);
        _targetGraphView.Add(tempEdge);
    }

    public void CreateNodes()
    {
        foreach (var nodeData in _containerCache.dialogueNodeData) 
        {
            var tempNode = _targetGraphView.CreateDialogueNode(nodeData.dialogueText);
            tempNode.GUID = nodeData.nodeGUID;
            _targetGraphView.AddElement(tempNode);

            var nodePorts = _containerCache.nodeLinks.Where(x=>x.baseNodeGUID == nodeData.nodeGUID).ToList();
            nodePorts.ForEach(x=>_targetGraphView.AddChoicePort(tempNode, x.portName));
        }
    }

    private void ClearGraph()
    {
        //set entry point guid from save. Discard existing guid
        nodes.Find(x => x.entryPoint).GUID = _containerCache.nodeLinks[0].baseNodeGUID;

        foreach (var node in nodes)
        {
            if (node.entryPoint)
            {
                continue;
            }

            //remove edges that connect to this node
            edges.Where(x=>x.input.node == node).ToList().ForEach(edge=>_targetGraphView.RemoveElement(edge));

            //then remove the node
            _targetGraphView.RemoveElement(node);
        }
    }
}
