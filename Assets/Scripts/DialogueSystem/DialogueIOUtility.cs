using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering.Universal;
using UnityEngine;

//This class is used for saving and loading the graph
public static class DialogueIOUtility
{
    static DialogueGraphView graphView;
    static string graphFileName;
    static string containerFolderPath;

    static List<DialogueGroup> groups;
    static List<DialogueNode> nodes;
    private static Dictionary<string, DialogueGroupSO> createdDialogueGroups;
    private static Dictionary<string, DialogueSO> createdDialogues;
    private static Dictionary<string, DialogueGroup> loadedGroups;
    private static Dictionary<string, DialogueNode> loadedNodes;

    public static void Initialize(DialogueGraphView _graphView, string graphName)
    { 
        graphView = _graphView;
        graphFileName = graphName;
        containerFolderPath = $"Assets/DialogueSystem/Dialogues/{graphFileName}";
    
        groups = new List<DialogueGroup>();
        nodes = new List<DialogueNode>();

        createdDialogueGroups = new Dictionary<string, DialogueGroupSO>();
        createdDialogues = new Dictionary<string, DialogueSO>();
        loadedGroups = new Dictionary<string, DialogueGroup>();
        loadedNodes = new Dictionary<string, DialogueNode>();
    }

    public static void Save()
    {
        CreateStaticFolders();

        GetElementsFromGraphView();

        DialogueGraphSaveDataSO graphData= CreateAsset<DialogueGraphSaveDataSO>("Assets/Editor/DialogueSystem/Graphs", $"{graphFileName}Graph");

        graphData.Initialize(graphFileName);

        DialogueContainerSO dialogueContainer = CreateAsset<DialogueContainerSO>(containerFolderPath, graphFileName);

        dialogueContainer.Initialize(graphFileName);

        SaveGroups(graphData, dialogueContainer);
        SaveNodes(graphData, dialogueContainer);

        SaveAsset(graphData);
        SaveAsset(dialogueContainer);
    }

    public static void Load() 
    {
        DialogueGraphSaveDataSO graphData = LoadAsset<DialogueGraphSaveDataSO>("Assets/Editor/DialogueSystem/Graphs", graphFileName);

        if (graphData == null)
        {
            EditorUtility.DisplayDialog(
                "Couldn't load that file.", 
                "The file at the following path could not be found:\n\n" + 
                $"Assets/Efitor/DialogueSystem/Graphs/{graphFileName}\n\n" +
                "Make sure you have chosen the right file and it is placed at the folder path above",
                "Thanks");

            return;
        }

        DialogueGraphEditorWindow.UpdateFileName(graphData.fileName);

        LoadGroups(graphData.groups);
        LoadNodes(graphData.nodes);
        LoadNodesConnections();
    }

    #region Load Methods
    private static void LoadNodes(List<DialogueNodeSaveData> nodes)
    {
        foreach (DialogueNodeSaveData nodeData in nodes)
        {
            List<DialogueChoiceSaveData> choices = CloneNodeChoices(nodeData.choices);
            DialogueNode node = (DialogueNode)graphView.CreateNode(nodeData.name, nodeData.dialogueType, nodeData.position, false);

            node.ID = nodeData.ID;
            node.choices = choices;
            node.dialogueText = nodeData.text;

            node.Draw();

            graphView.AddElement(node);

            loadedNodes.Add(node.ID, node);

            if (string.IsNullOrEmpty(nodeData.groupID))
            {
                continue;
            }

            DialogueGroup group = loadedGroups[nodeData.groupID];

            node.group = group;

            group.AddElement(node);

            
        }
    }

    private static void LoadGroups(List<DialogueGroupSaveData> groups)
    {
        foreach (DialogueGroupSaveData groupData in groups)
        {
            DialogueGroup group = (DialogueGroup)graphView.CreateGroup(groupData.name, groupData.position);

            group.ID = groupData.ID;

            loadedGroups.Add(group.ID, group);
        }
    }

    private static void LoadNodesConnections()
    {
        foreach (KeyValuePair<string, DialogueNode> loadedNode in loadedNodes)
        {
            //Debug.Log(loadedNode.Value.outputContainer.childCount);

            //Loop through children of the output container, if the element is a Port, loade choice data
            foreach (var element in loadedNode.Value.outputContainer.Children())
            {
                if (element is Port)
                {
                    Port choicePort = (Port)element;
                    DialogueChoiceSaveData choiceData = (DialogueChoiceSaveData)choicePort.userData;

                    if (string.IsNullOrEmpty(choiceData.nodeID))
                    {
                        continue;
                    }

                    DialogueNode nextNode = loadedNodes[choiceData.nodeID];

                    Port nextNodeInputPort = (Port)nextNode.inputContainer.Children().First();

                    UnityEditor.Experimental.GraphView.Edge edge = choicePort.ConnectTo(nextNodeInputPort);

                    graphView.AddElement(edge);

                    loadedNode.Value.RefreshPorts();
                }
            }

            //foreach (Port choicePort in loadedNode.Value.outputContainer.Children())
            //{
            //    Debug.Log("yes");
            //    DialogueChoiceSaveData choiceData = (DialogueChoiceSaveData)choicePort.userData;

            //    if (string.IsNullOrEmpty(choiceData.nodeID))
            //    {
            //        continue;
            //    }

            //    DialogueNode nextNode = loadedNodes[choiceData.nodeID];

            //    Port nextNodeInputPort = (Port)nextNode.inputContainer.Children().First();

            //    Edge edge = choicePort.ConnectTo(nextNodeInputPort);

            //    graphView.AddElement(edge);

            //    loadedNode.Value.RefreshPorts();
            //}
        }
    }
    #endregion

    #region Save Methods
    public static void SaveNodeToScriptableObject(DialogueNode node, DialogueContainerSO dialogueContainer)
    {

        DialogueSO dialogue;

        if (node.group != null)
        {
            dialogue = CreateAsset<DialogueSO>($"{containerFolderPath}/Groups/{node.group.title}/Dialogues", node.dialogueName);
            dialogueContainer.dialogueGroups.AddItem(createdDialogueGroups[node.group.ID], dialogue);
        }
        else
        {
            dialogue = CreateAsset<DialogueSO>($"{containerFolderPath}/Global/Dialogues", node.dialogueName);

            dialogueContainer.ungroupedDialogues.Add(dialogue);
        }
        SerializableDictionary<List<DialogueChoiceData>, DialogueSO> nodeChoices = new SerializableDictionary<List<DialogueChoiceData>, DialogueSO> ();

        nodeChoices.Add(ConvertNodeChoicesToDialogueChoices(node.choices), dialogue);

        //dialogue.Initialize(node.dialogueName, node.dialogueText, ConvertNodeChoicesToDialogueChoices(node.choices), nodeChoices, node.IsStartingNode());
        dialogue.Initialize(node.dialogueName, node.dialogueText, ConvertNodeChoicesToDialogueChoices(node.choices), nodeChoices, dialogue.dialogueType, node.IsStartingNode());

        createdDialogues.Add(node.ID, dialogue);

        SaveAsset(dialogue);
    }
    public static void SaveNodes(DialogueGraphSaveDataSO graphData, DialogueContainerSO dialogueContainer)
    {
        SerializableDictionary<string, List<string>> groupedNodeNames = new SerializableDictionary<string, List<string>>();
        List<string> ungroupedNodeNames = new List<string>();

        foreach (DialogueNode node in nodes)
        {
            SaveNodeToGraph(node, graphData);
            SaveNodeToScriptableObject(node, dialogueContainer);

            if (node.group != null)
            {
                groupedNodeNames.AddItem(node.group.title, node.dialogueName);

                continue;
            }

            ungroupedNodeNames.Add(node.dialogueName);
        }

        UpdateDialoguesChoicesConnections();

        UpdateOldGroupedNodes(groupedNodeNames, graphData);
        UpdateOldUngroupedNodes(ungroupedNodeNames, graphData);
    }

    public static void UpdateOldGroupedNodes(SerializableDictionary<string, List<string>> currentGroupedNodeNames, DialogueGraphSaveDataSO graphData)
    {
        if (graphData.oldGroupedNodeNames != null && graphData.oldGroupedNodeNames.Count != 0)
        {
            foreach (KeyValuePair<string, List<string>> oldGroupedNode in graphData.oldGroupedNodeNames)
            {
                List<string> nodesToRemove = new List<string>();

                if (currentGroupedNodeNames.ContainsKey(oldGroupedNode.Key))
                {
                    nodesToRemove = oldGroupedNode.Value.Except(currentGroupedNodeNames[oldGroupedNode.Key]).ToList();
                }

                foreach (string nodeToRemove in nodesToRemove)
                {
                    RemoveAsset($"{containerFolderPath}/Groups/{oldGroupedNode.Key}/Dialogues", nodeToRemove);
                }
            }
        }

        graphData.oldGroupedNodeNames = new SerializableDictionary<string, List<string>>();
    }

    public static void UpdateOldUngroupedNodes(List<string> currentUngroupedNodeNames, DialogueGraphSaveDataSO graphData)
    {
        if (graphData.oldUngroupedNodeNames != null && graphData.oldUngroupedNodeNames.Count != 0)
        {
            List<string> nodesToRemove = graphData.oldUngroupedNodeNames.Except(currentUngroupedNodeNames).ToList();

            foreach (string nodeToRemove in nodesToRemove)
            {
                RemoveAsset($"{containerFolderPath}/Global/Dialogues", nodeToRemove);
            }
        }

        graphData.oldUngroupedNodeNames = new List<string>(currentUngroupedNodeNames);
    }

    public static void RemoveAsset(string path, string assetName)
    {
        AssetDatabase.DeleteAsset($"{path}/{assetName}.asset");
    }

    public static void UpdateDialoguesChoicesConnections()
    {
        foreach (DialogueNode node in nodes)
        {
            DialogueSO dialogue = createdDialogues[node.ID];

            for (int choiceIndex = 0; choiceIndex < node.choices.Count; choiceIndex++)
            {
                DialogueChoiceSaveData nodeChoice = node.choices[choiceIndex];

                if (string.IsNullOrEmpty(nodeChoice.nodeID)) 
                {
                    continue;
                }

                dialogue.choices[choiceIndex].nextDialogue = createdDialogues[nodeChoice.nodeID];

                SaveAsset(dialogue);
            }
        }
    }
    public static List<DialogueChoiceData> ConvertNodeChoicesToDialogueChoices(List<DialogueChoiceSaveData> nodeChoices)
    {
        List<DialogueChoiceData> dialogueChoices = new List<DialogueChoiceData>();

        foreach (DialogueChoiceSaveData nodeChoice in nodeChoices)
        {
            //NEW CHOICE DATA
            DialogueChoiceData choiceData = new DialogueChoiceData()
            {
                text = nodeChoice.text,
                action = nodeChoice.action,
                questStartingPoint = nodeChoice.questStartingPoint,
                questCompletePoint = nodeChoice.questCompletePoint,
                questHandinPoint = nodeChoice.questHandinPoint,
                dialogueAfterCompletion = nodeChoice.dialogueAfterCompletion,
            };

            dialogueChoices.Add(choiceData);
        }
        return dialogueChoices;
    }

    public static void SaveNodeToGraph(DialogueNode node, DialogueGraphSaveDataSO graphData)
    {
        List<DialogueChoiceSaveData> choices = CloneNodeChoices(node.choices);

        DialogueNodeSaveData nodeData = new DialogueNodeSaveData()
        {
            ID = node.ID,
            name = node.dialogueName,
            choices = choices,
            text = node.dialogueText,
            groupID = node.group?.ID,
            dialogueType = node.dialogueType,
            position = node.GetPosition().position,            
        };

        graphData.nodes.Add(nodeData);
    }

    private static List<DialogueChoiceSaveData> CloneNodeChoices(List<DialogueChoiceSaveData> nodeChoices)
    {
        List<DialogueChoiceSaveData> choices = new List<DialogueChoiceSaveData>();

        foreach (DialogueChoiceSaveData choice in nodeChoices)
        {
            //NEW CHOICE DATA
            DialogueChoiceSaveData choiceData = new DialogueChoiceSaveData()
            {
                text = choice.text,
                nodeID = choice.nodeID,
                action = choice.action,
                questStartingPoint = choice.questStartingPoint,
                questCompletePoint = choice.questCompletePoint,
                questHandinPoint = choice.questHandinPoint,
                dialogueAfterCompletion = choice.dialogueAfterCompletion,
            };

            choices.Add(choiceData);
        }

        return choices;
    }

    public static void SaveGroups(DialogueGraphSaveDataSO graphData, DialogueContainerSO dialogueContainer)
    {
        List<string> groupNames = new List<string>();
        foreach (DialogueGroup group in groups)
        {
            SaveGroupToGraph(group, graphData);
            SaveGroupToScriptableObject(group, dialogueContainer);

            groupNames.Add(group.title);
        }

        UpdateOldGroups(groupNames, graphData);
    }

    public static void SaveGroupToScriptableObject(DialogueGroup group, DialogueContainerSO dialogueContainer)
    {
        string groupName = group.title;

        CreateFolder($"{containerFolderPath}/Groups", groupName);
        CreateFolder($"{containerFolderPath}/Groups/{groupName}", "Dialogues");

        DialogueGroupSO dialogueGroup = CreateAsset<DialogueGroupSO>($"{containerFolderPath}/Groups/{groupName}", groupName);

        dialogueGroup.Initialize(groupName);

        dialogueContainer.dialogueGroups.Add(dialogueGroup, new List<DialogueSO>());

        SaveAsset(dialogueGroup);

        createdDialogueGroups.Add(group.ID, dialogueGroup);
    }

    public static void SaveGroupToGraph(DialogueGroup group, DialogueGraphSaveDataSO graphData)
    {
        DialogueGroupSaveData groupData = new DialogueGroupSaveData()
        {
            ID = group.ID,
            name = group.title,
            position = group.GetPosition().position
        };

        graphData.groups.Add(groupData);
    }

    public static void SaveAsset(UnityEngine.Object asset)
    {
        EditorUtility.SetDirty(asset);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static void UpdateOldGroups(List<string> currentGroupNames, DialogueGraphSaveDataSO graphData)
    {
        if (graphData.oldGroupNames != null && graphData.oldGroupNames.Count != 0)
        {
            List<string> groupsToRemove = graphData.oldGroupNames.Except(currentGroupNames).ToList();

            foreach (string groupToRemove in groupsToRemove)
            {
                RemoveFolder($"{containerFolderPath}/Groups/{groupsToRemove}");
            }
        }

        graphData.oldGroupNames = new List<string>(currentGroupNames);
    }

    public static void RemoveFolder(string fullPath)
    {
        FileUtil.DeleteFileOrDirectory($"{fullPath}.meta");
        FileUtil.DeleteFileOrDirectory($"{fullPath}/");
    }

    public static void GetElementsFromGraphView()
    {
        Type groupType = typeof(DialogueGroup);
        graphView.graphElements.ForEach(graphElement =>
        {
            if (graphElement is DialogueNode node)
            {
                nodes.Add(node);

                return;
            }

            if (graphElement.GetType() == groupType)
            {
                DialogueGroup group = (DialogueGroup) graphElement;

                groups.Add(group);

                return;
            }
        });
    }

    public static void CreateStaticFolders()
    {
        CreateFolder("Assets/Editor/DialogueSystem", "Graphs");

        CreateFolder("Assets", "DialogueSystem");
        CreateFolder("Assets/DialogueSystem", "Dialogues");

        CreateFolder("Assets/DialogueSystem/Dialogues", graphFileName);

        CreateFolder(containerFolderPath, "Global");
        CreateFolder(containerFolderPath, "Groups");
        CreateFolder($"{containerFolderPath}/Global", "Dialogues");
    }

    public static void CreateFolder(string path, string folderName)
    {
        if (AssetDatabase.IsValidFolder($"{path}/{folderName}"))
        {
            return;
        }

        AssetDatabase.CreateFolder(path, folderName);
    }

    public static T CreateAsset<T>(string path, string assetName) where T:ScriptableObject
    {
        //string fullPath = $"{path}/{assetName}.asset";

        ////T asset = LoadAsset<T>(path, assetName);
        //T asset = AssetDatabase.LoadAssetAtPath<T>(path);

        //if (asset == null)
        //{
        //    asset = ScriptableObject.CreateInstance<T>();
        //    Debug.Log(path);

        //    AssetDatabase.CreateAsset(asset, fullPath);
        //}

        //return asset;        
        string fullPath = $"{path}/{assetName}.asset";
        T asset = LoadAsset<T>(path, assetName);

        if (asset == null)
        {
            asset = ScriptableObject.CreateInstance<T>();
            Debug.Log(path);
            AssetDatabase.CreateAsset(asset, fullPath);
        }

        return asset;
    }

    public static T LoadAsset<T>(string path, string assetName) where T : ScriptableObject
    {
        string fullPath = $"{path}/{assetName}.asset";

        return AssetDatabase.LoadAssetAtPath<T>(fullPath);
    }
    #endregion

}
