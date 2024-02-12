using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueNodeMultipleChoice : DialogueNode
{
    public override void Initialise(DialogueGraphView dialogueGraphView, Vector2 position)
    {
        base.Initialise(dialogueGraphView, position);

        dialogueType = DialogueType.MultipleChoice;

        choices.Add("New Choice");
    }

    public override void Draw()
    {
        base.Draw();

        //Create a button which allows you to add a choice to the dialogue
        Button addChoiceButton = DialogueElementUtility.CreateButton("Add Choice",() =>
        {
            Port choicePort = CreateChoicePort("New Choice");

            choices.Add("New Choice");

            outputContainer.Add(choicePort);
        });

        addChoiceButton.AddToClassList("ds-node__button");
        mainContainer.Insert(1, addChoiceButton);

        //Create a set of choices and a button to delete them
        foreach (string choice in choices)
        {
            Port choicePort = CreateChoicePort(choice);

            outputContainer.Add(choicePort);

        }
        RefreshExpandedState();
    }

    private Port CreateChoicePort(string choice)
    {        
        Port choicePort = this.CreatePort();

        //choicePort.portName = "";

        Button deleteChoiceButton = DialogueElementUtility.CreateButton("X");

        deleteChoiceButton.AddToClassList("ds-node__button");

        TextField choiceTextField = DialogueElementUtility.CreateTextField(choice);

        choiceTextField.AddToClassList("ds-node__textfield");
        choiceTextField.AddToClassList("ds-node__choice-textfield");
        //choiceTextField.AddToClassList("ds-node__choice-test-textfield");
        choiceTextField.AddToClassList("ds-node__textfield__hidden");

        choicePort.Add(choiceTextField);
        choicePort.Add(deleteChoiceButton);
        return choicePort;
    }
}
