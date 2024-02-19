using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Drawing;
using System;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine.Networking.PlayerConnection;

public class DialogueNode : Node
{
    public string ID { get; set; }
    public string dialogueName { get; set; }
    public List<DialogueChoiceSaveData> choices { get; set; }
    public string dialogueText { get; set; }
    public DialogueType dialogueType { get; set; }
    public DialogueGroup group { get; set; }
    public ScriptableObject connector { get; set; }
    public string methodName { get; set; }
    public QuestSO quest { get; set; }

    public DialogueGraphView graphView;

    public bool isStartingNode = false;


    UnityEngine.Color defaultBackgroundColour;

    public virtual void Initialise(string nodeName, DialogueGraphView dialogueGraphView, Vector2 position) {
        ID = Guid.NewGuid().ToString();
        dialogueName = nodeName;
        choices = new List<DialogueChoiceSaveData>();
        dialogueText = "Type here.....";

        graphView = dialogueGraphView;
        defaultBackgroundColour = new UnityEngine.Color(29f / 255f, 29f / 255f, 30f / 255f);

        SetPosition(new Rect(position, Vector2.zero));

        mainContainer.AddToClassList("ds-node__main-container");
        outputContainer.AddToClassList("ds-node__output-container");
        extensionContainer.AddToClassList("ds-node__extension-container");
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        evt.menu.AppendAction("Disconnect Input Ports", actionEvent => DisconnectInputPorts());
        evt.menu.AppendAction("Disconnect Output Ports", actionEvent => DisconnectOutputPorts());

        base.BuildContextualMenu(evt);
    }

    public virtual void Draw() {
        //Set the title of the dialogue node
        TextField dialogueNameTextField = DialogueElementUtility.CreateTextField(dialogueName, null, callback =>
        {
            if (string.IsNullOrEmpty(callback.newValue))
            {
                if (!string.IsNullOrEmpty(dialogueName))
                {
                    ++graphView.RepeatedNamesAmount;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(dialogueName))
                {
                    --graphView.RepeatedNamesAmount;
                }
            }

            if (group == null)
            {
                graphView.RemoveUngroupedNode(this);

                dialogueName = callback.newValue;

                graphView.AddUngroupedNode(this);

                return;
            }

            DialogueGroup currentGroup = (DialogueGroup) group;

            graphView.RemoveGroupedNode(this, group);

            dialogueName = callback.newValue;

            graphView.AddGroupedNode(this, currentGroup);
        });

        dialogueNameTextField.AddToClassList("ds-node__textfield");
        dialogueNameTextField.AddToClassList("ds-node__filename-textfield");
        dialogueNameTextField.AddToClassList("ds-node__textfield__hidden");

        titleContainer.Insert(0, dialogueNameTextField);

        //Create an input port for the node and add it to the input container(top portion of node)
        Port inputPort = this.CreatePort("Dialogue Connection", Orientation.Horizontal, Direction.Input, Port.Capacity.Multi);

        inputPort.portName = "Dialogue Connection";

        inputContainer.AddToClassList("ds-node__input-container");
        inputContainer.Add(inputPort);

        //Create a custom foldout element full of dialogue text
        VisualElement customDataContainer = new VisualElement();

        customDataContainer.AddToClassList("ds-node__custom-data-container");

        Foldout textFoldout = DialogueElementUtility.CreateFoldout("Dialogue Text");

        PopupField<string> methodField = DialogueElementUtility.CreatePopupField(callback =>
        {
            methodName = callback.newValue;
        });

        ObjectField connectorSOObject = DialogueElementUtility.CreateObjectField("Method Connector", callback =>
        {
            //if (callback.newValue is ScriptableObject so)
            //{
            connector = (ScriptableObject)callback.newValue;
            var methods = connector.GetType().GetMethods().Select(m => m.Name).ToList();

            methodField.label = "Method Field";
            methodField.choices = methods;
            //}
        });

        if (connector != null)
        {
            connectorSOObject.value = connector;
            var methods = connector.GetType().GetMethods().Select(m => m.Name).ToList();

            methodField.label = "Method Field";
            methodField.choices = methods;

            methodField.value = methodName;
        }

        ObjectField questObjectField = DialogueElementUtility.CreateQuestObjectField("Quest", callback =>
        {
            //if (callback.newValue is ScriptableObject so)
            //{
            quest = (QuestSO)callback.newValue;
            
            //}
        });

        if (quest != null)
        {
            questObjectField.value = quest;
        }

        //test.RegisterValueChangedCallback(evt =>
        //{
        //    if (evt.newValue is ScriptableObject so)
        //    {
        //        var methods = so.GetType().GetMethods().Select(m => m.Name).ToList();

        //        methodField.label = "Method Field";
        //        methodField.choices = methods;
        //        connector = (ScriptableObject)test.value;
        //        //monoBehaviour = (MonoBehaviour) test.value;
        //        //Debug.Log(monoBehaviour);
        //    }
        //});

        //methodField.RegisterValueChangedCallback(evt =>
        //{
        //    methodName = methodField.value;
        //    //Debug.Log(classAction);
        //    //if (evt.newValue is GetType())
        //    //{
        //    //    var methods = monoBehaviour.GetType().GetMethods().Select(m => m.Name).ToList();

        //    //    methodField.label = "Method Field";
        //    //    methodField.choices = methods;
        //    //}
        //});

        TextField textTextField = DialogueElementUtility.CreateTextArea(dialogueText, null, callback =>
        {
            dialogueText = callback.newValue;
        });

        textTextField.AddToClassList("ds-node__textfield");
        textTextField.AddToClassList("ds-node__quote-textfield");

        textFoldout.Add(textTextField);
        customDataContainer.Add(textFoldout);
        extensionContainer.Add(connectorSOObject);
        extensionContainer.Add(methodField);
        extensionContainer.Add(questObjectField);

        extensionContainer.Add(customDataContainer);
    }

    public void DisconnectAllPorts()
    {
        DisconnectInputPorts();
        DisconnectOutputPorts();
    }

    void DisconnectInputPorts() {
        DisconnectPorts(inputContainer);
    }
    void DisconnectOutputPorts() {
        DisconnectPorts(outputContainer);
    }

    void DisconnectPorts(VisualElement container) 
    {
        foreach (Port port in container.Children())
        {
            if (!port.connected)
            {
                continue;
            }

            graphView.DeleteElements(port.connections);
        }
    }


    public bool IsStartingNode() {
        Port inputPort = (Port) inputContainer.Children().First();

        return !inputPort.connected;
    }
    public void SetErrorStyle(UnityEngine.Color color)
    {
        mainContainer.style.backgroundColor = color;
    }

    public void ResetStyle()
    {
        mainContainer.style.backgroundColor = defaultBackgroundColour;

    }
}
