using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class DialogueNodeSingleChoice : DialogueNode
{
    public override void Initialise(DialogueGraphView dialogueGraphView, Vector2 position)
    {
        base.Initialise(dialogueGraphView, position);

        dialogueType = DialogueType.SingleChoice;

        choices.Add("Next dialogue");
    }

    public override void Draw()
    {
        base.Draw();

        foreach (string choice in choices) 
        {
            Port choicePort = this.CreatePort(choice);

            choicePort.portName = choice;

            outputContainer.Add(choicePort);

        }
        RefreshExpandedState();
    }
}
