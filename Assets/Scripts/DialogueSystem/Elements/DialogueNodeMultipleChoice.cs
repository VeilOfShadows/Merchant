using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueNodeMultipleChoice : DialogueNode
{
    public override void Initialise(string nodeName, DialogueGraphView dialogueGraphView, Vector2 position)
    {
        base.Initialise(nodeName, dialogueGraphView, position);

        dialogueType = DialogueType.MultipleChoice;

        DialogueChoiceSaveData choiceData = new DialogueChoiceSaveData()
        {
            text = "Next Dialogue"
        };

        choices.Add(choiceData);
    }

    public override void Draw()
    {
        base.Draw();

        //Create a button which allows you to add a choice to the dialogue
        Button addChoiceButton = DialogueElementUtility.CreateButton("Add Choice",() =>
        {
            DialogueChoiceSaveData choiceData = new DialogueChoiceSaveData()
            {
                text = "Next Dialogue"
            };

            choices.Add(choiceData);

            Port choicePort = CreateChoicePort(choiceData);

            outputContainer.Add(choicePort);
        });

        addChoiceButton.AddToClassList("ds-node__button");
        mainContainer.Insert(1, addChoiceButton);

        //Create a set of choices and a button to delete them
        foreach (DialogueChoiceSaveData choice in choices)
        {
            Port choicePort = CreateChoicePort(choice);

            outputContainer.Add(choicePort);

        }
        RefreshExpandedState();
    }

    private Port CreateChoicePort(object userData)
    {        
        Port choicePort = this.CreatePort();

        choicePort.userData = userData;

        DialogueChoiceSaveData choiceData = (DialogueChoiceSaveData)userData;
        //choicePort.portName = "";

        Button deleteChoiceButton = DialogueElementUtility.CreateButton("X", () =>
        {
            if (choices.Count == 1)
            {
                return;
            }

            if (choicePort.connected)
            {
                graphView.DeleteElements(choicePort.connections);
            }

            choices.Remove(choiceData);
            graphView.RemoveElement(choicePort);
        });

        deleteChoiceButton.AddToClassList("ds-node__button");

        TextField choiceTextField = DialogueElementUtility.CreateTextField(choiceData.text, null, callback => 
        {
            choiceData.text = callback.newValue;
        });

        choiceTextField.AddToClassList("ds-node__textfield");
        choiceTextField.AddToClassList("ds-node__choice-textfield");
        //choiceTextField.AddToClassList("ds-node__choice-test-textfield");
        choiceTextField.AddToClassList("ds-node__textfield__hidden");

        choicePort.Add(choiceTextField);
        choicePort.Add(deleteChoiceButton);
        return choicePort;
    }
}
