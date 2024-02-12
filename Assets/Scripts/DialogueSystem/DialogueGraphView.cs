using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System;
using UnityEngine.UIElements;
using UnityEditor;
using System.Linq;
using System.Security.Cryptography;

//Class for defining visual elements in the graph editor window
public class DialogueGraphView : GraphView
{
    DialogueGraphEditorWindow editorWindow;
    DialogueSearchWindow searchWindow;
    SerializableDictionary<string, DialogueNodeErrorData> ungroupedNodes;
    SerializableDictionary<Group, SerializableDictionary<string, DialogueNodeErrorData>> groupedNodes;

    int repeatedNamesAmount;
    int RepeatedNamesAmount
    {
        get 
        { 
            return repeatedNamesAmount;
        }
        set 
        { 
            repeatedNamesAmount = value;

            if (repeatedNamesAmount == 0)
            {
                editorWindow.EnableSaving();
            }

            if (repeatedNamesAmount == 1)
            {
                editorWindow.DisableSaving();
            }
        }
    }

    public DialogueGraphView(DialogueGraphEditorWindow dialogueGraphEditorWindow) {
        editorWindow = dialogueGraphEditorWindow;

        ungroupedNodes = new SerializableDictionary<string, DialogueNodeErrorData>();
        groupedNodes = new SerializableDictionary<Group, SerializableDictionary<string, DialogueNodeErrorData>>();

        AddManipulators();
        AddSearchWindow();
        AddGridBackground();

        OnElementsDeleted();
        OnGroupElementsAdded();
        OnGroupElementsRemoved();

        AddStyles();
    }

    private void AddSearchWindow()
    {
        if (searchWindow == null)
        {
            searchWindow = ScriptableObject.CreateInstance<DialogueSearchWindow>();

            searchWindow.Initialize(this);
        }

        nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
    }

    public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false) 
    {
        Vector2 worldMousePosition = mousePosition;

        if (isSearchWindow)
        {
            worldMousePosition -= editorWindow.position.position;
        }

        Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);

        return localMousePosition;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) 
    {
        List<Port> compatiblePorts = new List<Port>();

        ports.ForEach(port => 
        {
            if (startPort == port)
            {
                return;
            }

            if(startPort.node == port.node) 
            {
                return;
            }

            if(startPort.direction == port.direction) 
            {
                return;
            }

            compatiblePorts.Add(port);
        } );

        return compatiblePorts;
    }

    //Adds a set of manipulators for you to interact with the editor window
    private void AddManipulators()
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(CreateNodeContextualMenu("Add Node (Single Choice)", DialogueType.SingleChoice));
        this.AddManipulator(CreateNodeContextualMenu("Add Node (Multiple Choice)", DialogueType.MultipleChoice));

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        this.AddManipulator(CreateGroupContextualMenu());
    }

    //Creates right click menu to add node at mouse position
    IManipulator CreateNodeContextualMenu(string actionTitle, DialogueType dialogueType) 
    {
        ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
            menuEvent => menuEvent.menu.AppendAction(actionTitle, actionEvent => AddElement(CreateNode(dialogueType, GetLocalMousePosition(actionEvent.eventInfo.localMousePosition)))));

        return contextualMenuManipulator;
    }

    IManipulator CreateGroupContextualMenu()
    {
        ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
            menuEvent => menuEvent.menu.AppendAction("Add Group", actionEvent => AddElement(CreateGroup("DialogueGroup", GetLocalMousePosition(actionEvent.eventInfo.localMousePosition)))));

        return contextualMenuManipulator;
    }

    //Adds a custom grid background to the window
    private void AddGridBackground()
    {
        GridBackground gridBackground = new GridBackground();
        gridBackground.StretchToParentSize();

        //Insert(position, element) places the element at that position on the canvas. Used for re-ordering overlapping visuals
        Insert(0, gridBackground);
    }
    
    public DialogueNode CreateNode(DialogueType dialogueType, Vector2 position)
    {
        //if (nodeT)
        //{

        //}
        Type nodeType = Type.GetType($"DialogueNode{dialogueType}");
        DialogueNode node = (DialogueNode)Activator.CreateInstance(nodeType);

        //DialogueNode node = new DialogueNode();

        node.Initialise(this, position);
        node.Draw();

        AddUngroupedNode(node);

        return node;
       //AddElement(node);
    }

    void OnElementsDeleted() {
        deleteSelection = (operationName, askUser) =>
        {
            Type edgeType = typeof(Edge);

            List<DialogueNode> nodesToDelete = new List<DialogueNode>();
            List<Edge> edgesToDelete = new List<Edge>();

            foreach (GraphElement element in selection)
            {
                if (element is DialogueNode node)
                {
                    nodesToDelete.Add((node));

                    continue;
                }

                if (element.GetType() == edgeType)
                {
                    Edge edge = (Edge)element;

                    edgesToDelete.Add(edge);

                    continue;
                }
            }

            DeleteElements(edgesToDelete);

            foreach (DialogueNode node in nodesToDelete)
            {
                if (node.group != null)
                {
                    node.group.RemoveElement(node);
                }
                RemoveUngroupedNode(node);

                node.DisconnectAllPorts();

                RemoveElement(node);
            }
        };
    }

    void OnGroupElementsAdded()
    {
        elementsAddedToGroup = (group, elements) =>
        {
            foreach (GraphElement element in elements)
            {
                if (!(element is DialogueNode))
                {
                    continue;
                }

                DialogueNode node = (DialogueNode)element;
                RemoveUngroupedNode(node);
                AddGroupedNode(node, group);
            }

        };
    }

    void OnGroupElementsRemoved()
    {
        elementsRemovedFromGroup = (group, elements) =>
        {
            foreach (GraphElement element in elements)
            {
                if (!(element is DialogueNode))
                {
                    continue;
                }

                DialogueNode node = (DialogueNode)element;

                RemoveGroupedNode(node, group);
                AddUngroupedNode(node);
            }
        };
    }

    public void AddGroupedNode(DialogueNode node, Group group)
    {
        string nodeName = node.dialogueName;

        node.group = group;

        if (!groupedNodes.ContainsKey(group))
        {
            groupedNodes.Add(group, new SerializableDictionary<string, DialogueNodeErrorData>());
        }

        if (!groupedNodes[group].ContainsKey(nodeName))
        {
            DialogueNodeErrorData nodeErrorData = new DialogueNodeErrorData();

            nodeErrorData.nodes.Add(node);

            groupedNodes[group].Add(nodeName, nodeErrorData);

            return;
        }

        groupedNodes[group][nodeName].nodes.Add(node);

        Color errorColor = groupedNodes[group][nodeName].errorData.color;

        node.SetErrorStyle(errorColor);

        if (groupedNodes[group][nodeName].nodes.Count == 2)
        {
            ++RepeatedNamesAmount;
            groupedNodes[group][nodeName].nodes[0].SetErrorStyle(errorColor);
        }
    }

    public void RemoveGroupedNode(DialogueNode node, Group group)
    {
        string nodeName = node.dialogueName;

        List<DialogueNode> groupedNodeList = groupedNodes[group][nodeName].nodes;

        groupedNodes[group][nodeName].nodes.Remove(node);

        node.ResetStyle();

        if (groupedNodeList.Count == 1)
        {
            --RepeatedNamesAmount;

            groupedNodeList[0].ResetStyle();

            return;
        }
        
        if (groupedNodeList.Count == 0)
        {
            groupedNodes[group].Remove(nodeName);

            if (groupedNodes[group].Count == 0)
            {
                groupedNodes.Remove(group);
            }
        }
    }

    public void AddUngroupedNode(DialogueNode node)
    {
        string nodeName = node.dialogueName;
        if (!ungroupedNodes.ContainsKey(nodeName))
        {
            DialogueNodeErrorData nodeErrorData = new DialogueNodeErrorData();

            nodeErrorData.nodes.Add(node);

            ungroupedNodes.Add(nodeName, nodeErrorData);

            return;
        }

        ungroupedNodes[nodeName].nodes.Add(node);

        Color errorColour = ungroupedNodes[nodeName].errorData.color;

        node.SetErrorStyle(errorColour);

        if (ungroupedNodes[nodeName].nodes.Count == 2)
        {
            ++RepeatedNamesAmount;
            ungroupedNodes[nodeName].nodes[0].SetErrorStyle(errorColour);
        }
    }

    public void RemoveUngroupedNode(DialogueNode node)
    {
        string nodeName = node.dialogueName;

        node.group = null;

        ungroupedNodes[nodeName].nodes.Remove(node);

        node.ResetStyle();

        if (ungroupedNodes[nodeName].nodes.Count == 1)
        {
            --RepeatedNamesAmount;
            ungroupedNodes[nodeName].nodes[0].ResetStyle();
            return;
        }
        
        if (ungroupedNodes[nodeName].nodes.Count == 0)
        {
            ungroupedNodes.Remove(nodeName);
        }
    }

    public GraphElement CreateGroup(string title, Vector2 localMousePosition)
    {
        Group group = new Group()
        {
            title = title
        };

        group.SetPosition(new Rect(localMousePosition, Vector2.zero));

        return group;
    }
    
    //Adds specified style sheet which is used for changing the appearance of the grid
    private void AddStyles()
    {
        StyleSheet styleSheet = Resources.Load<StyleSheet>("DialogueGraph");
        styleSheets.Add(styleSheet);

        styleSheet = Resources.Load<StyleSheet>("DialogueNodeStyles");
        styleSheets.Add(styleSheet);
    }
}
